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
            ModConsole.Msg("Initializing RPC", 1);
            Discord = new global::Discord.Discord(Preferences.discordAppId.Value, (ulong)CreateFlags.Default);
            ModConsole.Msg($"Discord is {Discord}", 1);
            ModConsole.Msg($"Application ID is {Preferences.discordAppId.Value}", 1);
            _activityManager = Discord.GetActivityManager();
            ModConsole.Msg($"Activity manager is {_activityManager}", 1);
            SetRpc(null, "Loading Game", "bonelab", "BONELAB", null, null);
        }

        public static void Dispose()
        {
            Discord.Dispose();
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
                if (result == global::Discord.Result.Ok)
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
}