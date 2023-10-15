using BLRPC.Internal;
using BLRPC.Melon;
using BoneLib;
using HarmonyLib;
using SLZ.Rig;

namespace BLRPC.Patching
{
    public class AvatarChange
    {
        [HarmonyPatch(typeof(RigManager), "SwapAvatar")]
        public class RigManager_SwapAvatar
        {
            public static void Postfix(RigManager __instance)
            {
                if (Preferences.detailsMode == DetailsMode.CurrentAvatar)
                {
                    UpdateRpc();
                }
            }
        }

        private static void UpdateRpc()
        {
            var avatar = Player.GetCurrentAvatar();
            Rpc.SetRpc($"Current Avatar: {avatar}", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
        }
    }
}