namespace BLRPC.Presence;

internal static class RpcManager
{
    public static Discord.Discord Discord;
    public static ActivityManager ActivityManager;
    private static readonly long Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    
    private static Activity _activity;
    
    public static void Init()
    {
        ModConsole.Msg("Initializing RPC", 1);
        Discord = new global::Discord.Discord(Preferences.DiscordAppId.Value, (ulong)CreateFlags.Default);
        ModConsole.Msg($"Discord is {Discord}", 1);
        ModConsole.Msg($"Application ID is {Preferences.DiscordAppId.Value}", 1);
        ActivityManager = Discord.GetActivityManager();
        ModConsole.Msg($"Activity manager is {ActivityManager}", 1);
        _activity = new Activity
        {
            State = "Loading Game",
#if DEBUG
            Details = "boobs.",
#endif
            Timestamps =
            {
                Start = Start
            },
            Assets =
            {
                LargeImage = "bonelab",
                LargeText = "BONELAB"
            },
            Instance = false
        };
        UpdateRpc();
    }

    public static void Dispose()
    {
        Discord.Dispose();
    }

    public static void SetActivity(ActivityField activityField, string value)
    {
        switch (activityField)
        {
            case ActivityField.State:
                _activity.State = value;
                break;
            case ActivityField.Details:
                _activity.Details = value;
                break;
            case ActivityField.LargeImageKey:
                _activity.Assets.LargeImage = value;
                break;
            case ActivityField.LargeImageText:
                _activity.Assets.LargeText = value;
                break;
            case ActivityField.SmallImageKey:
                _activity.Assets.SmallImage = value;
                break;
            case ActivityField.SmallImageText:
                _activity.Assets.SmallText = value;
                break;
            case ActivityField.JoinSecret:
                _activity.Secrets.Join = value;
                break;
            case ActivityField.Party:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            default:
                ModConsole.Error("Invalid activity field!");
                break;
        }
        UpdateRpc();
    }
    public static void SetActivity(ActivityField activityField, ActivityParty party)
    {
        switch (activityField)
        {
            case ActivityField.State:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            case ActivityField.Details:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            case ActivityField.LargeImageKey:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            case ActivityField.LargeImageText:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            case ActivityField.SmallImageKey:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            case ActivityField.SmallImageText:
                ModConsole.Error("This error is my fault, called a method wrong.");
                break;
            case ActivityField.Party:
                _activity.Party = party;
                break;
            default:
                ModConsole.Error("Invalid activity field!");
                break;
        }
        UpdateRpc();
    }

    public enum ActivityField
    {
        State,
        Details,
        LargeImageKey,
        LargeImageText,
        SmallImageKey,
        SmallImageText,
        Party,
        JoinSecret
    }

    public static void UpdateRpc()
    {
        ModConsole.Msg($"Setting activity | Details: {_activity.Details} | State: {_activity.State} | LargeImageKey: {_activity.Assets.LargeImage} | LargeImageText: {_activity.Assets.LargeText} | SmallImageKey: {_activity.Assets.SmallImage} | SmallImageText: {_activity.Assets.SmallText}", 1);
        ActivityManager.UpdateActivity(_activity, (result) =>
        {
            if (result == Result.Ok)
            {
                ModConsole.Msg("Successfully set activity!", 1);
            }
            else
            {
                ModConsole.Error($"Failed to set activity: {result.ToString()}");
            }
        });
    }
}