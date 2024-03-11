using LabFusion.Network;
using LabFusion.Preferences;
using LabFusion.Representation;
using LabFusion.Utilities;
using SLZ.Marrow.SceneStreaming;
using Steamworks;
using Result = Discord.Result;

namespace BLRPC.Presence.Handlers;

internal static class FusionHandler
{
    public static bool InServer;
    private static ActivityParty _party;
    private static Lobby _lobby;
    
    private static LobbyManager _lobbyManager;
    
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
    
    private static LobbyType ConvertLobbyType(ServerPrivacy serverPrivacy)
    {
        return serverPrivacy switch
        {
            ServerPrivacy.PUBLIC => LobbyType.Public,
            ServerPrivacy.FRIENDS_ONLY => LobbyType.Private,
            ServerPrivacy.PRIVATE => LobbyType.Private,
            ServerPrivacy.LOCKED => LobbyType.Private,
            _ => LobbyType.Private
        };
    }
        
    public static void Init()
    {
#if DEBUG
        ModConsole.Msg("THIS IS GETTING CALLED!");
#endif
        _lobbyManager = RpcManager.Discord.GetLobbyManager();
        
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

        RpcManager.ActivityManager.OnActivityJoin += secret => _lobbyManager.ConnectLobbyWithActivitySecret(secret, DiscordJoinLobby);
    }

    private static void DiscordJoinLobby(Result result, ref Lobby lobby)
    {
        if (InServer) return;
        if (result != Result.Ok)
        {
            ModConsole.Error($"Failed to join lobby: {result.ToString()}");
            return;
        }
        SteamId steamId = ulong.Parse(_lobbyManager.GetLobbyMetadataValue(lobby.Id, "steamLobbyId"));
        if (NetworkInfo.CurrentNetworkLayer is SteamNetworkLayer steamLayer)
        {
            steamLayer.JoinServer(steamId);
        }
        _lobby = lobby;
        RpcManager.SetActivity(RpcManager.ActivityField.JoinSecret, _lobbyManager.GetLobbyActivitySecret(lobby.Id));
    }

    private static void OnJoinLobby()
    {
        InServer = true;
        ModConsole.Msg("Joined lobby, setting party activity.", 1);
        _party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        _party.Size.MaxSize = FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue();
        _party.Privacy = ConvertPartyPrivacy(FusionPreferences.ActiveServerSettings.Privacy.GetValue());
        _party.Id = _lobby.Id.ToString();
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }
        
    private static void OnServerSettingsChanged()
    {
        if (!InServer) return;
        ModConsole.Msg("Server settings changed, updating party activity.", 1);
        _party.Size.MaxSize = FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue();
        _party.Privacy = ConvertPartyPrivacy(FusionPreferences.ActiveServerSettings.Privacy.GetValue());
        var lobbyTransaction = _lobbyManager.GetLobbyUpdateTransaction(_lobby.Id);
        lobbyTransaction.SetCapacity(FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue());
        lobbyTransaction.SetType(ConvertLobbyType(FusionPreferences.ActiveServerSettings.Privacy.GetValue()));
        lobbyTransaction.SetLocked(FusionPreferences.ActiveServerSettings.Privacy.GetValue() == ServerPrivacy.LOCKED);
        _lobbyManager.UpdateLobby(_lobby.Id, lobbyTransaction, res =>
        {
            if (res != Result.Ok)
            {
                ModConsole.Error($"Failed to update lobby: {res.ToString()}");
            }
        });
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
    }
        
    private static void OnStartLobby()
    {
        InServer = true;

        var lobbyTransaction = _lobbyManager.GetLobbyCreateTransaction();
        lobbyTransaction.SetCapacity(FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue());
        lobbyTransaction.SetType(ConvertLobbyType(FusionPreferences.ActiveServerSettings.Privacy.GetValue()));
        lobbyTransaction.SetLocked(FusionPreferences.ActiveServerSettings.Privacy.GetValue() == ServerPrivacy.LOCKED);
        _lobbyManager.CreateLobby(lobbyTransaction, OnDiscordLobbyCreate);
    }

    private static void OnDiscordLobbyCreate(Result result, ref Lobby lobby)
    {
        if (result != Result.Ok)
        {
            ModConsole.Error($"Failed to create lobby: {result.ToString()}");
            return;
        }
        
        LobbyTransaction transaction = _lobbyManager.GetLobbyUpdateTransaction(lobby.Id);
        transaction.SetMetadata("steamLobbyId", SteamClient.SteamId.ToString());
        _lobbyManager.UpdateLobby(lobby.Id, transaction, res =>
        {
            if (res != Result.Ok)
            {
                ModConsole.Error($"Failed to update lobby: {res.ToString()}");
            }
        });
        
        _lobby = lobby;
        
        ModConsole.Msg("Started lobby, setting party activity.", 1);
        _party.Size.CurrentSize = PlayerIdManager.PlayerCount;
        _party.Size.MaxSize = FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue();
        _party.Id = SteamClient.SteamId.ToString();
#if DEBUG
        ModConsole.Msg($"Max players is {FusionPreferences.ActiveServerSettings.MaxPlayers.GetValue()}");
        ModConsole.Msg($"SteamID is {SteamClient.SteamId.ToString()}");
#endif
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
        RpcManager.SetActivity(RpcManager.ActivityField.JoinSecret, _lobbyManager.GetLobbyActivitySecret(lobby.Id));
    }

    private static void OnLeaveLobby()
    {
        InServer = false;
        ModConsole.Msg("Left lobby, clearing party activity.", 1);
        _party.Id = "";
        _party.Size.CurrentSize = 0;
        _party.Size.MaxSize = 0;
        RpcManager.SetActivity(RpcManager.ActivityField.Party, _party);
        RpcManager.SetActivity(RpcManager.ActivityField.State, $"In {SceneStreamer.Session.Level.Title}");
        RpcManager.SetActivity(RpcManager.ActivityField.JoinSecret, "");
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