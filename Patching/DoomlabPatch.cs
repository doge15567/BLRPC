using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using NEP.DOOMLAB.Entities;

namespace BLRPC.Patching
{
    public static class DoomlabPatch
    {
        [HarmonyPatch(typeof(Mobj), "Kill")]
        public class Mobj_OnDeath
        {
            public static void Postfix(Mobj __instance)
            {
                if (Main.IsQuest || Main.DiscordClosed) return;
                if (Preferences.detailsMode.Value == DetailsMode.NPCDeaths)
                {
                    if (!__instance.flags.HasFlag(MobjFlags.MF_COUNTKILL)) return;
                    UpdateCounter();
                }
            }
        }
        public static int Counter = 0;
        private static void UpdateCounter()
        {
            Counter += 1;
            ModConsole.Msg($"DoomNPC died, new death count is {Counter}", 1);
            GlobalVariables.details = $"NPC Deaths: {Counter}";
            Rpc.SetRpc(GlobalVariables.details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText, GlobalVariables.smallImageKey, GlobalVariables.smallImageText);
        }
    }
}