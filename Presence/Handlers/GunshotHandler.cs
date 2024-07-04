using Il2CppSLZ.Bonelab;

namespace BLRPC.Presence.Handlers;

internal static class GunshotHandler
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
    public static int Counter;
    private static void UpdateCounter()
    {
        Counter += 1;
        ModConsole.Msg($"Gun fired, new shot count is {Counter}", 1);
        RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Gun Shots Fired: {Counter}");
    }
}