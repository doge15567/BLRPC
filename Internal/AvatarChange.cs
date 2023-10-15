using BLRPC.Internal;
using BLRPC.Melon;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;

namespace BLRPC.Internal
{
    public static class AvatarChange
    {
        public static void UpdateRpc()
        {
            if (Preferences.detailsMode.entry.Value != DetailsMode.CurrentAvatar) return;
            var avatar = Player.GetCurrentAvatar();
            var avatarclean = HelperMethods.GetCleanObjectName(avatar.name);
            ModConsole.Msg($"Current avatar is {avatarclean}", LoggingMode.DEBUG);
            Rpc.SetRpc($"Current Avatar: {avatarclean}", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
        }
    }
}