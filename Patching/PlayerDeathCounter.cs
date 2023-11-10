using BLRPC.Internal;
using BLRPC.Melon;
using HarmonyLib;

namespace BLRPC.Patching
{
    public static class PlayerDeathCounter
    {
        [HarmonyPatch(typeof(Player_Health), "Death")]
        public class PlayerDeath
        {
            public static void Postfix(Player_Health __instance)
            {
                if (Main.IsQuest || Main.DiscordClosed) return;
                if (Preferences.detailsMode == DetailsMode.PlayerDeaths)
                {
                    UpdateCounter();
                }
            }
        }
        
        public static int Counter = 0;
        
        private static void UpdateCounter()
        {
            Counter += 1;
            ModConsole.Msg($"Playerdied, new death count is {Counter}", LoggingMode.DEBUG);
            GlobalVariables.details = $"Player Deaths: {Counter}";
            Rpc.SetRpc(GlobalVariables.details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText, GlobalVariables.smallImageKey, GlobalVariables.smallImageText);
        }
        
    }
}