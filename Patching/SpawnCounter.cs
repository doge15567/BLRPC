using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using SLZ.AI;
using SLZ.Props;
using SLZ.Props.Weapons;

namespace BLRPC.Patching
{
    public static class SpawnCounter
    {
        [HarmonyPatch(typeof(SpawnGun), "OnFire")]
        public class SpawnGun_Spawn
        {
            public static void Postfix(Gun __instance)
            {
                if (Main.IsQuest || Main.DiscordClosed) return;
                if (Preferences.detailsMode.Value == DetailsMode.SpawnablesPlaced)
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
            Rpc.SetRpc(GlobalVariables.details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText, GlobalVariables.smallImageKey, GlobalVariables.smallImageText);
        }
    }
}