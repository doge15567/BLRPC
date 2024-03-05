using System.Diagnostics;
using BoneLib;
using System.IO;
using BLRPC.Presence.Handlers.Helpers;
using BLRPC.Internal;
using BLRPC.Presence;
using Random = System.Random;

namespace BLRPC;

public class Main : MelonMod
{
    internal const string Name = "BLRPC";
    internal const string Description = "Discord Rich Presence for BONELAB";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "2.0.0";
    internal const string DownloadLink = "https://bonelab.thunderstore.io/package/SoulWithMae/BonelabRichPresence/";
    
    // Quest users.
    public static bool IsQuest;
    private static bool _checkedQuest;
    
    // Prevents stuff from running if Discord isn't open
    public static bool DiscordClosed;
        
    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        // Quest is a cunt can we bomb facebook already
        if (IsQuest) return;
        Preferences.Setup();
        if (!DiscordOpen()) return;
        UserData.Setup();
        ModConsole.Msg("Initializing RPC", 1);
        RpcManager.Init();
        if (HelperMethods.CheckIfAssemblyLoaded("labfusion")) FusionHandler.Init();
        BoneMenu.Setup();
        Hooking.OnLevelInitialized += OnLevelLoad;
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
        GlobalVariables.Status = $"In {levelInfo.title}";
        ModConsole.Msg($"Status is {GlobalVariables.Status}", 1);
        GlobalVariables.LargeImageKey = CheckBarcode.CheckMap(levelInfo.barcode);
        ModConsole.Msg($"Large image key is {GlobalVariables.LargeImageKey}", 1);
        GlobalVariables.LargeImageText = levelInfo.title;
        ModConsole.Msg($"Large image text is {GlobalVariables.LargeImageText}", 1);
        switch (Preferences.DetailsMode.Value)
        {
            case DetailsMode.GunShots:
                GlobalVariables.Details = "Gun Shots Fired: 0";
                break;
            case DetailsMode.NPCDeaths:
                GlobalVariables.Details = "NPC Deaths: 0";
                break;
            case DetailsMode.SpawnablesPlaced:
                GlobalVariables.Details = "Objects Spawned: 0";
                break;
            case DetailsMode.SDKMods:
                GlobalVariables.Details = $"SDK Mods Loaded: {CheckPallets.GetPalletCount()}";
                break;
            case DetailsMode.Extraes:
                GlobalVariables.Details = ExtraesMode.RandomScreamingAboutNonsense();
                break;
            case DetailsMode.Entries:
                GlobalVariables.Details = GetEntry();
                ModConsole.Msg($"Details are {GlobalVariables.Details}", 1);
                break;
            case DetailsMode.PlayerDeaths:
                GlobalVariables.Details = $"Player Deaths: {DeathHandler.Player.Counter}";
                break;
            default:
                ModConsole.Error("You don't have a proper mode set!");
                GlobalVariables.Details = "Somehow, this broke!";
                break;
        }
        RpcManager.UpdateRpc();
    }

    private static string GetEntry()
    {
        var rnd = new Random();
        var lines = File.ReadAllLines(UserData.UserEntriesPath);
        var r = rnd.Next(lines.Length);
        return lines[r];
    }
}