using BLRPC.Handlers.Helpers;
using BLRPC.Internal;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;
using SLZ.VRMK;

namespace BLRPC.Handlers;

public static class AvatarHandler
{
    [HarmonyPatch(typeof(ArtRig), "SetAvatar")]
    public class PlayerDeath
    {
        public static void Postfix(ArtRig __instance, Avatar avatar)
        {
            var aviBarcode = __instance.manager.AvatarCrate.Crate.Barcode;
            var aviTitle = __instance.manager.AvatarCrate.Crate.Title;
            if (aviTitle == null || aviBarcode == null) return;
            GlobalVariables.smallImageKey = CheckBarcode.CheckAvatar(aviBarcode);
            GlobalVariables.smallImageText = aviTitle;
            Rpc.UpdateRpc();
        }
    }
}