using System.IO;
using BLRPC.Internal;
using BLRPC.Presence.Handlers.Helpers;
using BoneLib;

namespace BLRPC.Presence.Handlers;

internal static class LevelHandler
{
    public static void OnLevelLoaded(LevelInfo levelInfo)
    {

        levelInfo.title = Main.RemoveRichText(levelInfo.title);
        RpcManager.SetActivity(RpcManager.ActivityField.State, FusionHandler.InServer ? $"In {levelInfo.title} | Fusion" : $"In {levelInfo.title}");
        RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, CheckBarcode.CheckMap(levelInfo.barcode));
        RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, levelInfo.title);
        
        switch (Preferences.DetailsMode.Value)
        {
            case DetailsMode.GunShots:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Gun Shots Fired: {GunshotHandler.Counter}");
                break;
            case DetailsMode.NPCDeaths:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, $"NPC Deaths: {DeathHandler.NPC.Counter}");
                break;
            case DetailsMode.SpawnablesPlaced:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Objects Spawned: {SpawnGunHandler.Counter}");
                break;
            case DetailsMode.SDKMods:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, $"SDK Mods Loaded: {CheckPallets.GetPalletCount()}");
                break;
            case DetailsMode.Extraes:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, ExtraesMode.RandomScreamingAboutNonsense());
                break;
            case DetailsMode.Entries:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, GetEntry());
                break;
            case DetailsMode.PlayerDeaths:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Player Deaths: {DeathHandler.Player.Counter}");
                break;
            default:
                ModConsole.Error("You don't have a proper mode set!");
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "Somehow, this broke!");
                break;
        }
    }
    
    private static string GetEntry()
    {
        var rnd = new Random();
        var lines = File.ReadAllLines(UserData.UserEntriesPath);
        var r = rnd.Next(lines.Length);
        return lines[r];
    }
}