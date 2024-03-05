using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using SLZ.AI;
using SLZ.Props;
using SLZ.Props.Weapons;

namespace BLRPC.Handlers;

public static class SpawnGunHandler
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

    public static int Counter = 0;
    private static void UpdateCounter()
    {
        Counter += 1;
        ModConsole.Msg($"Spawnable placed, new spawn count is {Counter}", 1);
        GlobalVariables.details = $"Objects Spawned: {Counter}";
        Rpc.UpdateRpc();
    }
}