using System.Diagnostics;
using BoneLib;
using BLRPC.Presence;

namespace BLRPC;

public class Main : MelonMod
{
    internal const string Name = "BLRPC";
    internal const string Description = "Discord Rich Presence for BONELAB";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "2.1.0";
    internal const string DownloadLink = "https://bonelab.thunderstore.io/package/SoulWithMae/BonelabRichPresence/";
    
    // Quest users.
    public static bool IsQuest;
    private static bool _checkedQuest;

    public static bool NetworkerInstalled;
    
    // Prevents stuff from running if Discord isn't open
    public static bool DiscordClosed;
        
    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        // Quest is a cunt can we bomb facebook already
        if (IsQuest) return;
        Preferences.Setup();
#if DEBUG
        ModConsole.Warning("This is a debug build! Things may be unstable!");
#endif
        if (!DiscordOpen()) return;
        UserData.Setup();
        ModConsole.Msg("Initializing RPC", 1);
        RpcManager.Init();
        BoneMenu.Setup();
        Hooking.OnLevelInitialized += OnLevelLoad;
    }

    public override void OnLateInitializeMelon()
    {
        // BLRPC's regular OnInitializeMelon is called before Fusion is loaded, not good
        if (HelperMethods.CheckIfAssemblyLoaded("labfusion")) FusionHandler.Init();
        NetworkerInstalled = HelperMethods.CheckIfAssemblyLoaded("modiomodnetworker");
    }

    private static bool DiscordOpen()
    {
        var discord = Process.GetProcessesByName("discord");
        var discordcanary = Process.GetProcessesByName("discordcanary");
        var discordptb = Process.GetProcessesByName("discordptb");
        if (discordcanary.Length <= 0 && discord.Length <= 0 && discordptb.Length <= 0)
        {
            ModConsole.Error("Neither Discord, Discord Canary, or Discord PTB are running!");
            DiscordClosed = true;
            return false;
        }
        if ((discord.Length > 0 && discordcanary.Length > 0) || (discord.Length > 0 && discordptb.Length > 0) || (discordcanary.Length > 0 && discordptb.Length > 0))
        {
            ModConsole.Error("You have 2 Discords open! Discord may struggle to pick one, and it may not work! Please close one and restart!");
        }
        return true;
    }

    public override void OnApplicationQuit()
    {
        if (IsQuest || DiscordClosed) return;
        RpcManager.Dispose();
        UserData.Dispose();
    }
        
    public override void OnUpdate()
    {
        if (_checkedQuest)
        {
            if (HelperMethods.IsAndroid())
            {
                ModConsole.Error("You are on Quest! This mod won't work! Please use the PC version of BONELAB!");
                // kys
                IsQuest = true;
            }
            _checkedQuest = true;
        }
        if (IsQuest || DiscordClosed) return;
        RpcManager.Discord.RunCallbacks();
    }
    
    private static void OnLevelLoad(LevelInfo levelInfo)
    {
        if (IsQuest || DiscordClosed) return;
        ModConsole.Msg($"Level loaded: {levelInfo.title}", 1);
        if (Preferences.ResetKillsOnLevelLoad.Value) DeathHandler.NPC.Counter = 0;
        if (Preferences.ResetGunShotsOnLevelLoad.Value) GunshotHandler.Counter = 0;
        if (Preferences.ResetDeathsOnLevelLoad.Value) DeathHandler.Player.Counter = 0;
        SpawnGunHandler.Counter = 0;
        LevelHandler.OnLevelLoaded(levelInfo);
    }
}