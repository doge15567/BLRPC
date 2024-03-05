using BLRPC.Presence.Handlers.Helpers;
using SLZ.Rig;
using SLZ.VRMK;

namespace BLRPC.Presence.Handlers;

internal static class AvatarHandler
{
    [HarmonyPatch(typeof(ArtRig), "SetAvatar")]
    public class PlayerDeath
    {
        public static void Postfix(ArtRig __instance, Avatar avatar)
        {
            var aviBarcode = __instance.manager.AvatarCrate.Crate.Barcode;
            var aviTitle = __instance.manager.AvatarCrate.Crate.Title;
            if (aviTitle == null || aviBarcode == null) return;
            GlobalVariables.SmallImageKey = CheckBarcode.CheckAvatar(aviBarcode);
            GlobalVariables.SmallImageText = aviTitle;
            RpcManager.UpdateRpc();
        }
    }
}