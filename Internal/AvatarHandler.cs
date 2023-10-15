using System;
using BLRPC.Internal;
using BLRPC.Melon;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;

namespace BLRPC.Internal
{
    public static class AvatarHandler
    {
        public static void UpdateRpc()
        {
            var avatar = Player.GetCurrentAvatar();
            var index = avatar.name.LastIndexOf("(Clone)", StringComparison.Ordinal);
            if (index != -1)
            {
                var cleanName = avatar.name.Substring(0, index);
                GlobalVariables.smallImageText = CheckBarcode.CheckAvatarName(cleanName);
                GlobalVariables.smallImageKey = CheckBarcode.CheckAvatar(cleanName);
            }
            else
            {
                GlobalVariables.smallImageText = avatar.name;
                GlobalVariables.smallImageKey = CheckBarcode.CheckAvatar(avatar.name);
            }
            Rpc.SetRpc(GlobalVariables.details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText, GlobalVariables.smallImageKey, GlobalVariables.smallImageText);
        }
    }
}