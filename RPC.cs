using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Timers;
using DiscordRPC;
using DiscordRPC.Events;
using DiscordRPC.Message;
using MelonLoader;

namespace BLRPC
{
    public static class Rpc
    {
        private static DateTime _startTime;
        
        public static DiscordRpcClient Client;
        public static void Initialize()
        {
            Client = new DiscordRpcClient("1162864836418490388");	
            MelonLogger.Msg($"Client initialized with ID {Client.ApplicationID}");
            Client.OnReady += OnReady;
            Client.OnPresenceUpdate += OnPresenceUpdate;
            Client.Initialize();
            MelonLogger.Msg("Client initialized! Probably.");
            Client.SetPresence(new RichPresence()
            {
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
            _startTime = DateTime.UtcNow;
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
        public static void SetRpc(string state, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
        {
            Client.SetPresence(new RichPresence()
            {
                State = state,
                Timestamps = new Timestamps()
                {
                    Start = Now
                },
                Assets = new Assets()
                {
                    LargeImageKey = largeImageKey,
                    LargeImageText = largeImageText,
                    SmallImageKey = smallImageKey,
                    SmallImageText = smallImageText
                }
            });
        }
    }
}