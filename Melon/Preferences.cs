using MelonLoader;

namespace BLRPC.Melon
{
    internal static class Preferences
    {
        public static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
        private static MelonPreferences_Category category = MelonPreferences.CreateCategory("BLRPC");

        public static MelonPreferences_Entry<int> loggingMode;
        
        public static MelonPreferences_Entry<long> discordAppId;
        
        public static MelonPreferences_Entry<DetailsMode> detailsMode;
        

        public static void Setup()
        {
            loggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0, "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
            discordAppId = category.CreateEntry("DiscordAppId", 1162864836418490388, "Discord Application ID", "The application ID for the Discord application that will be used for Rich Presence.");
            detailsMode = category.CreateEntry("DetailsMode", DetailsMode.Entries, "Details Mode", "The mode for the details section. Possible Values: Entries, NPCDeaths, GunShots, Extraes, SDKMods, SpawnablesPlaced");
            GlobalCategory.SetFilePath(MelonUtils.UserDataDirectory+"/WeatherElectric.cfg");
            GlobalCategory.SaveToFile(false);
            category.SetFilePath(MelonUtils.UserDataDirectory+"/WeatherElectric.cfg");
            category.SaveToFile(false);
            ModConsole.Msg("Finished preferences setup", 1);
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