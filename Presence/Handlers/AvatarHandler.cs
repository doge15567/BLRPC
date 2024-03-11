using BLRPC.Presence.Handlers.Helpers;
using LabFusion.Utilities;
using SLZ.Rig;
using SLZ.VRMK;

namespace BLRPC.Presence.Handlers;

internal static class AvatarHandler
{
    [HarmonyPatch(typeof(ArtRig), "SetAvatar")]
    public class ArtRigSetAvatar
    {
        // ReSharper disable once InconsistentNaming
        public static void Postfix(ArtRig __instance, Avatar avatar)
        {
            DelayUtilities.Delay(() =>
            {
                var aviBarcode = __instance.manager.AvatarCrate.Crate.Barcode;
                var aviTitle = __instance.manager.AvatarCrate.Crate.Title;
                if (aviTitle == null || aviBarcode == null) return;
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, CheckBarcode.CheckAvatar(aviBarcode));
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, aviTitle);
            }, 2);
        }
    }
}