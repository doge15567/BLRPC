using System;
using System.Reflection;
using BLRPC.Internal;
using Discord;
using LabFusion.Network;
using LabFusion.Preferences;
using LabFusion.Representation;
using LabFusion.Utilities;

namespace BLRPC.Patching;

internal static class FusionHandler
{
    public static ActivityParty Party;

    private static int GetMaxPlayers()
    {
        Type fusionPreferences = typeof(FusionPreferences);
        FieldInfo activeServerSettings = fusionPreferences.GetField("ActiveServerSettings", BindingFlags.Static | BindingFlags.NonPublic);
        if (activeServerSettings != null)
        {
            int maxPlayers = (int)activeServerSettings.FieldType.GetField("MaxPlayers").GetValue(null);
            return maxPlayers;
        }
        return default;
    }
        
    private static ServerPrivacy GetServerPrivacy()
    {
        Type fusionPreferences = typeof(FusionPreferences);
        FieldInfo activeServerSettings = fusionPreferences.GetField("ActiveServerSettings", BindingFlags.Static | BindingFlags.NonPublic);
        if (activeServerSettings != null)
        {
            ServerPrivacy serverPrivacy = (ServerPrivacy)activeServerSettings.FieldType.GetField("ServerPrivacy").GetValue(null);
            return serverPrivacy;
        }

        return default;
    }

    private static ActivityPartyPrivacy ConvertPartyPrivacy(ServerPrivacy serverPrivacy)
    {
        switch (serverPrivacy)
        {
            case ServerPrivacy.PUBLIC:
                return ActivityPartyPrivacy.Public;
            case ServerPrivacy.FRIENDS_ONLY:
                return ActivityPartyPrivacy.Private;
            case ServerPrivacy.PRIVATE:
                return ActivityPartyPrivacy.Private;
            case ServerPrivacy.LOCKED:
                return ActivityPartyPrivacy.Private;
            default:
                return ActivityPartyPrivacy.Private;
        }
    }
        
    public static void Init()
    {
        MultiplayerHooking.OnJoinServer += OnJoinLobby;
        MultiplayerHooking.OnDisconnect += OnLeaveLobby;
        MultiplayerHooking.OnStartServer += OnStartLobby;
        MultiplayerHooking.OnPlayerJoin += OnPlayerJoin;
        MultiplayerHooking.OnPlayerLeave += OnPlayerLeave;
        MultiplayerHooking.OnServerSettingsChanged += OnServerSettingsChanged;
        Party = new ActivityParty
        {
            Id = "",
            Size =
            {
                CurrentSize = 0,
                MaxSize = 0
            },
            Privacy = ActivityPartyPrivacy.Private
        };
    }

    private static void OnJoinLobby()
    {
        Party.Id = $"{PlayerIdManager.GetPlayerId(0).LongId}";
        Party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        Party.Size.MaxSize = GetMaxPlayers();
        Party.Privacy = ConvertPartyPrivacy(GetServerPrivacy());
        Rpc.UpdateRpc();
    }
        
    private static void OnServerSettingsChanged()
    {
        Party.Size.MaxSize = GetMaxPlayers();
        Party.Privacy = ConvertPartyPrivacy(GetServerPrivacy());
        Rpc.UpdateRpc();
    }
        
    private static void OnStartLobby()
    {
        Party.Id = $"{PlayerIdManager.GetPlayerId(0).LongId}";
        Party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        Party.Size.MaxSize = GetMaxPlayers();
        Party.Privacy = ConvertPartyPrivacy(GetServerPrivacy());
        Rpc.UpdateRpc();
    }

    private static void OnLeaveLobby()
    {
        Party.Id = "";
        Party.Size.CurrentSize = 0;
        Party.Size.MaxSize = 0;
        Party.Privacy = ActivityPartyPrivacy.Private;
        Rpc.UpdateRpc();
    }

    private static void OnPlayerJoin(PlayerId playerId)
    {
        Party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        Rpc.UpdateRpc();
    }

    private static void OnPlayerLeave(PlayerId playerId)
    {
        Party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        Rpc.UpdateRpc();
    }
}