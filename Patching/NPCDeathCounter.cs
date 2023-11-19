using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using NEP.DOOMLAB.Entities;
using SLZ.AI;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Global

namespace BLRPC.Patching
{
    public static class NPCDeathCounter
    {
        [HarmonyPatch(typeof(AIBrain), "OnDeath")]
        public class AIBrain_OnDeath
        {
            public static void Postfix(AIBrain __instance)
            {
                if (Main.IsQuest || Main.DiscordClosed) return;
                if (Preferences.DetailsMode.Value == DetailsMode.NPCDeaths)
                {
                    UpdateCounter();
                }
            }
        }
        
        [HarmonyPatch(typeof(Mobj), "Kill")]
        public class Mobj_OnDeath
        {
            public static void Postfix(Mobj __instance)
            {
                if (Main.IsQuest || Main.DiscordClosed) return;
                if (Preferences.DetailsMode.Value == DetailsMode.NPCDeaths && Preferences.CountDoomlabDeaths.Value)
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
            ModConsole.Msg($"NPC died, new death count is {Counter}", 1);
            GlobalVariables.details = $"NPC Deaths: {Counter}";
            Rpc.SetRpc(GlobalVariables.details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText, GlobalVariables.smallImageKey, GlobalVariables.smallImageText);
        }
    }
}