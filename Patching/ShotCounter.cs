using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using SLZ.AI;
using SLZ.Props;
using SLZ.Props.Weapons;

namespace BLRPC.Patching
{
    public static class ShotCounter
    {
        [HarmonyPatch(typeof(Gun), "OnFire")]
        public class Gun_Fire
        {
            public static void Postfix(Gun __instance)
            {
                if (Preferences.detailsMode == DetailsMode.GunShots)
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
            ModConsole.Msg($"Gun fired, new shot count is {Counter}", LoggingMode.DEBUG);
            Rpc.SetRpc($"Gun Shots Fired: {Counter}", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
        }
    }
}