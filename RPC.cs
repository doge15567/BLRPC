using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Timers;
using DiscordRPC;
using DiscordRPC.Events;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using MelonLoader;

namespace BLRPC
{
    public static class Rpc
    {
        private static DateTime _startTime;

        public static readonly DiscordRpcClient Client = new DiscordRpcClient("1162864836418490388");
        public static void Initialize()
        {
            Client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            MelonLogger.Msg($"Client initialized with ID {Client.ApplicationID}");
            Client.OnReady += OnReady;
            Client.OnPresenceUpdate += OnPresenceUpdate;
            Client.Initialize();
            MelonLogger.Msg("Client initialized! Probably.");
            _startTime = DateTime.UtcNow;
            Client.SetPresence(new RichPresence()
            {
                Details = UserEntries.GetEntry(),
                State = "Loading",
                Timestamps = new Timestamps()
                {
                    Start = Now
                },
                Assets = new Assets()
                {
                    LargeImageKey = "bonelab",
                    LargeImageText = "BONELAB",
                    SmallImageKey = "bonelabsmall",
                    SmallImageText = "BONELAB"
                }
            });
            MelonLogger.Msg("Presence set! Maybe.");
            MelonLogger.Msg($"Current presence: {Client.CurrentPresence}");
        }

        private static void OnReady(object sender, ReadyMessage e)
        {
            MelonLogger.Msg($"Received Ready from user {e.User.Username}");
        }

        private static void OnPresenceUpdate(object sender, PresenceMessage e)
        {
            MelonLogger.Msg($"Received Update! {e.Presence}");
        }
        public static DateTime? Now { get; }
        public static void SetRpc(string details, string state, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
        {
            Client.UpdateDetails(details);
            Client.UpdateState(state);
            Client.UpdateLargeAsset(largeImageKey, largeImageText);
            Client.UpdateSmallAsset(smallImageKey, smallImageText);
        }
    }
}