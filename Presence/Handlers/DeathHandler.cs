using NEP.DOOMLAB.Entities;
using SLZ.AI;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Global

namespace BLRPC.Presence.Handlers;

internal static class DeathHandler
{
    public static class NPC
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
        
        public static int Counter;
        private static void UpdateCounter()
        {
            Counter += 1;
            ModConsole.Msg($"NPC died, new death count is {Counter}", 1);
            RpcManager.SetActivity(RpcManager.ActivityField.Details, $"NPC Deaths: {Counter}");
        }
    }

    public static class Player
    {
        [HarmonyPatch(typeof(Player_Health), "Death")]
        public class PlayerDeath
        {
            public static void Postfix(Player_Health __instance)
            {
                if (Main.IsQuest || Main.DiscordClosed) return;
                if (Preferences.DetailsMode.Value == DetailsMode.PlayerDeaths)
                {
                    UpdateCounter();
                }
            }
        }
        
        public static int Counter;
        
        private static void UpdateCounter()
        {
            Counter += 1;
            ModConsole.Msg($"Player died, new death count is {Counter}", 1);
            RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Player Deaths: {Counter}");
        }
    }
}