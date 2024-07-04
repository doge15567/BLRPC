namespace BLRPC.Melon;
using MelonLoader.Utils;

internal static class Preferences
{
    private static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
    public static readonly MelonPreferences_Category Category = MelonPreferences.CreateCategory("BLRPC");

    public static MelonPreferences_Entry<int> LoggingMode;
    public static MelonPreferences_Entry<long> DiscordAppId;
    public static MelonPreferences_Entry<DetailsMode> DetailsMode;
        
    // NPC Deaths Settings
    public static MelonPreferences_Entry<bool> CountDoomlabDeaths;
    public static MelonPreferences_Entry<bool> ResetKillsOnLevelLoad;
        
    // Player Deaths Settings
    public static MelonPreferences_Entry<bool> ResetDeathsOnLevelLoad;
        
    // Gun Shots Settings
    public static MelonPreferences_Entry<bool> ResetGunShotsOnLevelLoad;

    public static void Setup()
    {
            // Basic
            LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0, "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
            DiscordAppId = Category.CreateEntry("DiscordAppId", 1162864836418490388, "Discord Application ID", "The application ID for the Discord application that will be used for Rich Presence.");
            DetailsMode = Category.CreateEntry("DetailsMode", Melon.DetailsMode.Entries, "Details Mode", "The mode for the details section. Possible Values: Entries, NPCDeaths, GunShots, Extraes, SDKMods, SpawnablesPlaced");
            // NPC Deaths
            CountDoomlabDeaths = Category.CreateEntry("CountDOOMLABDeaths", true, "Count DOOMLAB Deaths", "Whether or not to count DOOMLAB deaths in the NPC Deaths counter");
            ResetKillsOnLevelLoad = Category.CreateEntry("ResetKillsOnLevelLoad", true, "Reset Kills On Level Load", "Whether or not to reset the NPC kill counter on level load");
            // Player deaths
            ResetDeathsOnLevelLoad = Category.CreateEntry("ResetDeathsOnLevelLoad", true, "Reset Deaths On Level Load", "Whether or not to reset the player death counter on level load");
            // Gun Shots
            ResetGunShotsOnLevelLoad = Category.CreateEntry("ResetGunShotsOnLevelLoad", true, "Reset Gun Shots On Level Load", "Whether or not to reset the gun shot counter on level load");
            // Save to file
            GlobalCategory.SetFilePath(MelonEnvironment.UserDataDirectory+"/WeatherElectric.cfg");
            GlobalCategory.SaveToFile(false);
            Category.SetFilePath(MelonEnvironment.UserDataDirectory+"/WeatherElectric.cfg");
            Category.SaveToFile(false);
            ModConsole.Msg("Finished preferences setup", 1);
        }
}
public enum DetailsMode
{
    Entries,
    NPCDeaths,
    GunShots,
    Extraes,
    SDKMods,
    SpawnablesPlaced,
    PlayerDeaths
}