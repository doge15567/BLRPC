using MelonLoader;

namespace BLRPC.Melon
{
    internal static class Preferences
    {
        public static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
        private static MelonPreferences_Category category = MelonPreferences.CreateCategory("BLRPC");

        public static ModPref<LoggingMode> loggingMode;
        
        public static ModPref<long> discordAppId;
        
        public static ModPref<DetailsMode> detailsMode;
        

        public static void Setup()
        {
            loggingMode = new ModPref<LoggingMode>(GlobalCategory, "LoggingMode", LoggingMode.NORMAL, "Logging Mode", "The level of logging to use. DEBUG = Everything, NORMAL = Important Only");
            discordAppId = new ModPref<long>(category, "DiscordAppId", 1162864836418490388, "Discord Application ID", "The application ID for the Discord application that will be used for Rich Presence.");
            detailsMode = new ModPref<DetailsMode>(category,"DetailsMode", DetailsMode.Entries, "Details Mode", "The mode for the details section. Possible Values: Entries, NPCDeaths, GunShots, Extraes, SDKMods, SpawnablesPlaced");
            GlobalCategory.SetFilePath(MelonUtils.UserDataDirectory+"/WeatherElectric.cfg");
            GlobalCategory.SaveToFile(false);
            category.SetFilePath(MelonUtils.UserDataDirectory+"/WeatherElectric.cfg");
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