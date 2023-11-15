using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
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
                if (Preferences.detailsMode.Value == DetailsMode.NPCDeaths)
                {
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