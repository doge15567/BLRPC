using LabFusion.Network;
using LabFusion.Preferences;
using LabFusion.Representation;
using LabFusion.Utilities;

namespace BLRPC.Presence.Handlers;

internal static class FusionHandler
{
    public static bool InServer;
    private static ActivityParty _party;
    
    private static ActivityPartyPrivacy ConvertPartyPrivacy(ServerPrivacy serverPrivacy)
    {
        return serverPrivacy switch
        {
            ServerPrivacy.PUBLIC => ActivityPartyPrivacy.Public,
            ServerPrivacy.FRIENDS_ONLY => ActivityPartyPrivacy.Private,
            ServerPrivacy.PRIVATE => ActivityPartyPrivacy.Private,
            ServerPrivacy.LOCKED => ActivityPartyPrivacy.Private,
            _ => ActivityPartyPrivacy.Private
        };
    }
        
    public static void Init()
    {
#if DEBUG
        ModConsole.Msg("THIS IS GETTING CALLED!");
#endif
        MultiplayerHooking.OnJoinServer += OnJoinLobby;
        MultiplayerHooking.OnDisconnect += OnLeaveLobby;
        MultiplayerHooking.OnStartServer += OnStartLobby;
        MultiplayerHooking.OnPlayerJoin += OnPlayerJoin;
        MultiplayerHooking.OnPlayerLeave += OnPlayerLeave;
        MultiplayerHooking.OnServerSettingsChanged += OnServerSettingsChanged;
        _party = new ActivityParty
        {
            Id = "disconnected",
            Size =
            {
                CurrentSize = 0,
                MaxSize = 0
            }
        };
        
        RpcManager.ActivityManager.OnActivityJoin += OnDiscordJoin;
    }

    private static void OnDiscordJoin(string secret)
    {
        
    }

    private static void OnJoinLobby()
    {
        InServer = true;
        ModConsole.Msg("Joined lobby, setting party activity.", 1);
        _party.Id = $"{PlayerIdManager.GetPlayerId(0).LongId}";
        _party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        _party.Size.MaxSize = FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue();
        _party.Privacy = ConvertPartyPrivacy(FusionPreferences.ActiveServerSettings.Privacy.GetValue());
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }
        
    private static void OnServerSettingsChanged()
    {
        if (_party.Id == "disconnected") return;
        ModConsole.Msg("Server settings changed, updating party activity.", 1);
        _party.Size.MaxSize = FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue();
        _party.Privacy = ConvertPartyPrivacy(FusionPreferences.ActiveServerSettings.Privacy.GetValue());
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }
        
    private static void OnStartLobby()
    {
        InServer = true;
        ModConsole.Msg("Started lobby, setting party activity.", 1);
        _party.Id = $"{PlayerIdManager.GetPlayerId(0).LongId}";
        _party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        _party.Size.MaxSize = FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue();
#if DEBUG
        ModConsole.Msg($"Max players is {FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue()}", 1);
#endif
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }

    private static void OnLeaveLobby()
    {
        InServer = false;
        ModConsole.Msg("Left lobby, clearing party activity.", 1);
        _party.Id = "disconnected";
        _party.Size.CurrentSize = 0;
        _party.Size.MaxSize = 0;
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }

    private static void OnPlayerJoin(PlayerId playerId)
    {
        ModConsole.Msg("Player joined, updating party activity.", 1);
        _party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }

    private static void OnPlayerLeave(PlayerId playerId)
    {
        ModConsole.Msg("Player left, updating party activity.", 1);
        _party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }
}