using MelonLoader;

namespace BLRPC.Melon
{
    internal static class Preferences
    {
        private static MelonPreferences_Category category = MelonPreferences.CreateCategory("BLRPC");

        public static ModPref<LoggingMode> loggingMode;
        
        public static ModPref<long> discordAppId;
        
        public static ModPref<DetailsMode> detailsMode;
        

        public static void Setup()
        {
            loggingMode = new ModPref<LoggingMode>(category, "LoggingMode", LoggingMode.NORMAL, "Logging Mode", "The logging mode for the mod. DEBUG will show all messages, NORMAL will show all messages except DEBUG messages.");
            discordAppId = new ModPref<long>(category, "DiscordAppId", 1162864836418490388, "Discord Application ID", "The application ID for the Discord application that will be used for Rich Presence.");
            detailsMode = new ModPref<DetailsMode>(category,"DetailsMode", DetailsMode.Entries, "Details Mode", "The mode for the details section. Possible Values: Entries, NPCDeaths, GunShots, Extraes, SDKMods, SpawnablesPlaced");

            category.SaveToFile(false);
            ModConsole.Msg("Finished preferences setup", LoggingMode.DEBUG);
        }
    }
    internal enum DetailsMode
    {
        Entries,
        NPCDeaths,
        GunShots,
        Extraes,
        SDKMods,
        SpawnablesPlaced,
        PlayerDeaths
    }
}