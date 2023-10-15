using System;
using Discord;
using MelonLoader;

namespace BLRPC
{
    public static class Rpc
    {
        public static Discord.Discord Discord;
        private static ActivityManager _activityManager;
        private static readonly long Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public static void Initialize()
        {
            MelonLogger.Msg("We got to the activity stuff");
            Discord = new global::Discord.Discord(1162864836418490388, (ulong)CreateFlags.Default);
            _activityManager = Discord.GetActivityManager();
            SetRpc(null, "Loading MelonLoader", "bonelab", "BONELAB", "bonelabsmall", "BONELAB");
            MelonLogger.Msg("We got past the activity stuff");
        }
        
        public static void SetRpc(string details, string state, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
        {
            MelonLogger.Msg("Setting activity");
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
            MelonLogger.Msg("Activity set");
            _activityManager.UpdateActivity(activity, (result) =>
            {
                if (result == global::Discord.Result.Ok)
                {
                    MelonLogger.Msg("Successfully set activity");
                }
                else
                {
                    MelonLogger.Msg("Failed setting activity");
                }
            });
        }
    }
}