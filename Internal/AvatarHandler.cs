using BoneLib;

namespace BLRPC.Internal
{
    public static class AvatarHandler
    {
        public static void UpdateRpc()
        {
            var rigManager = Player.rigManager;
            var avatar = rigManager.AvatarCrate.Crate;
            GlobalVariables.smallImageKey = CheckBarcode.CheckAvatar(avatar.Barcode);
            GlobalVariables.smallImageText = avatar.Title;
            Rpc.SetRpc(GlobalVariables.details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText, GlobalVariables.smallImageKey, GlobalVariables.smallImageText);
        }
    }
}