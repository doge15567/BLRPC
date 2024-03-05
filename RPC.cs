using System;
using BLRPC.Internal;
using BLRPC.Melon;
using BLRPC.Patching;
using Discord;

namespace BLRPC;

internal static class Rpc
{
    public static Discord.Discord Discord;
    private static ActivityManager _activityManager;
    private static readonly long Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public static void Initialize()
    {
        ModConsole.Msg("Initializing RPC", 1);
        Discord = new global::Discord.Discord(Preferences.DiscordAppId.Value, (ulong)CreateFlags.Default);
        ModConsole.Msg($"Discord is {Discord}", 1);
        ModConsole.Msg($"Application ID is {Preferences.DiscordAppId.Value}", 1);
        _activityManager = Discord.GetActivityManager();
        ModConsole.Msg($"Activity manager is {_activityManager}", 1);
        SetRpc(null, "Loading Game", "bonelab", "BONELAB", null, null);
    }

    public static void Dispose()
    {
        Discord.Dispose();
    }

    public static void UpdateRpc()
    {
        ModConsole.Msg($"Setting activity with details {GlobalVariables.details}, state {GlobalVariables.status}, large image key {GlobalVariables.largeImageKey}, and large image text {GlobalVariables.largeImageText}", 1);
        var activity = new Activity
        {
            State = GlobalVariables.status,
            Details = GlobalVariables.details,
            Timestamps =
            {
                Start = Start
            },
            Assets =
            {
                LargeImage = GlobalVariables.largeImageKey,
                LargeText = GlobalVariables.largeImageText,
                SmallImage = GlobalVariables.smallImageKey,
                SmallText = GlobalVariables.smallImageText
            },
            Instance = false,
            Party = FusionHandler.Party
        };
        _activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Result.Ok)
            {
                ModConsole.Msg("Successfully set activity!", 1);
            }
            else
            {
                ModConsole.Error("Failed to set activity!");
            }
        });
    }

    public static void SetRpc(string details, string state, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
    {
        ModConsole.Msg($"Setting activity with details {details}, state {state}, large image key {largeImageKey}, and large image text {largeImageText}", 1);
        var activity = new Activity
        {
            State = state,
            Details = details,
            Timestamps =
            {
                Start = Start
            },
            Assets =
            {
                LargeImage = largeImageKey,
                LargeText = largeImageText,
                SmallImage = smallImageKey,
                SmallText = smallImageText
            },
            Instance = false
        };
        _activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Result.Ok)
            {
                ModConsole.Msg("Successfully set activity!", 1);
            }
            else
            {
                ModConsole.Error("Failed to set activity!");
            }
        });
    }
}