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
                if (Preferences.detailsMode == DetailsMode.SpawnablesPlaced)
                {
                    UpdateCounter();
                }
            }
        }
        public static int Counter = 0;
        private static void UpdateCounter()
        {
            Counter += 1;
            Rpc.SetRpc($"Objects Spawned: {Counter}", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
        }
    }
}