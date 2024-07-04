using BLRPC.Presence.Handlers.Helpers;
using LabFusion.Utilities;
using Il2CppSLZ.Rig;
using Il2CppSLZ.VRMK;
using static MelonLoader.MelonLogger;
using UnityEngine.Playables;

namespace BLRPC.Presence.Handlers;

internal static class AvatarHandler
{
    //[HarmonyPatch(typeof(RigManager), "SwapAvatar")]
    public class RigManagerSetAvatar
    {
        // ReSharper disable once InconsistentNaming
        /*
        public static void Postfix(RigManager __instance, Avatar avatar)
        {
            if (Main.FusionInstalled)
            {
                if (!__instance.IsSelf()) return;
            }
            
            DelayUtilities.Delay(() =>
            {
                var aviBarcode = __instance.AvatarCrate.Crate.Barcode;
                var aviTitle = __instance.AvatarCrate.Crate.Title;
                if (aviTitle == null || aviBarcode == null) return;
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, CheckBarcode.CheckAvatar(aviBarcode));
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, aviTitle);
            }, 2);
        }
        */
        
    }

}


