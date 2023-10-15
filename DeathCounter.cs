using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;
using SLZ.AI;

namespace BLRPC
{
    public static class DeathCounter
    {
        [HarmonyPatch(typeof(AIBrain), "OnDeath")]
        public class AIBrain_OnDeath
        {
            public static void Postfix(AIBrain __instance)
            {
                if (Preferences.detailsMode == DetailsMode.NPCDeaths)
                {
                    UpdateCounter();
                }
            }
        }
        public static int Counter = 0;
        private static void UpdateCounter()
        {
            Counter += 1;
            Rpc.SetRpc($"NPC Deaths: {Counter}", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
        }
    }
}