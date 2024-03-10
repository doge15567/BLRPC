using SLZ.Props;

namespace BLRPC.Presence.Handlers;

internal static class SpawnGunHandler
{
    [HarmonyPatch(typeof(SpawnGun), "OnFire")]
    public class SpawnGunSpawn
    {
        public static void Postfix(SpawnGun __instance)
        {
            if (Main.IsQuest || Main.DiscordClosed) return;
            if (Preferences.DetailsMode.Value == DetailsMode.SpawnablesPlaced)
            {
                UpdateCounter();
            }
        }
    }

    public static int Counter;
    private static void UpdateCounter()
    {
        Counter += 1;
        ModConsole.Msg($"Spawnable placed, new spawn count is {Counter}", 1);
        RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Objects Spawned: {Counter}");
    }
}