using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using SLZ.Props;
using SLZ.Props.Weapons;

namespace BLRPC.Handlers;

public static class GunshotHandler
{
    [HarmonyPatch(typeof(Gun), "OnFire")]
    public class GunFire
    {
        public static void Postfix(Gun __instance)
        {
            if (Main.IsQuest || Main.DiscordClosed) return;
            if (Preferences.DetailsMode.Value == DetailsMode.GunShots)
            {
                if (__instance.GetComponent<SpawnGun>()) return;
                UpdateCounter();
            }
        }
    }
    public static int Counter = 0;
    private static void UpdateCounter()
    {
        Counter += 1;
        ModConsole.Msg($"Gun fired, new shot count is {Counter}", 1);
        GlobalVariables.details = $"Gun Shots Fired: {Counter}";
        Rpc.UpdateRpc();
    }
}