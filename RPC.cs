using System;
using BLRPC.Melon;
using Discord;

namespace BLRPC
{
    public static class Rpc
    {
        public static Discord.Discord Discord;
        private static ActivityManager _activityManager;
        private static readonly long Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public static void Initialize()
        {
            ModConsole.Msg("Initializing RPC", LoggingMode.DEBUG);
            Discord = new global::Discord.Discord(Preferences.discordAppId, (ulong)CreateFlags.Default);
            ModConsole.Msg($"Discord is {Discord}", LoggingMode.DEBUG);
            ModConsole.Msg($"Application ID is {Preferences.discordAppId}", LoggingMode.DEBUG);
            _activityManager = Discord.GetActivityManager();
            ModConsole.Msg($"Activity manager is {_activityManager}", LoggingMode.DEBUG);
            SetRpc(null, "Loading Game", "bonelab", "BONELAB", null, null);
        }

        public static void Dispose()
        {
            Discord.Dispose();
        }
        
        public static void SetRpc(string details, string state, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
        {
            ModConsole.Msg($"Setting activity with details {details}, state {state}, large image key {largeImageKey}, and large image text {largeImageText}", LoggingMode.DEBUG);
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
                if (result == global::Discord.Result.Ok)
                {
                    ModConsole.Msg("Successfully set activity!", LoggingMode.DEBUG);
                }
                else
                {
                    ModConsole.Error("Failed to set activity!");
                }
            });
        }
    }
}