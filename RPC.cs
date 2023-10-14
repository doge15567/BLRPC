using System;
using System.IO;
using System.Reflection;
using DiscordRPC;
using MelonLoader;

namespace BLRPC
{
    public static class Rpc
    {
        private static DiscordRpcClient _client;
        private static bool _hasLoadedLib;
        private static IntPtr _rpcLibrary;

        public static void LoadAssembly()
        {
            if (_hasLoadedLib) return;
            var appDataPath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC");
            var rpcDllPath = Path.Combine(appDataPath, "DiscordRPC.dll");
            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            if (!File.Exists(rpcDllPath))
            {
                using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("BLRPC.DiscordRPC.dll"))
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    str?.CopyTo(memoryStream);
                    File.WriteAllBytes(rpcDllPath, memoryStream.ToArray());
                }
            }

            MelonLogger.Msg("Loading BASS from " + rpcDllPath);
            _rpcLibrary = DllTools.LoadLibrary(rpcDllPath);
            _hasLoadedLib = true;
        }
        
        public static void Initialize()
        {
            _client = new DiscordRpcClient("1162864836418490388");	
            _client.OnReady += (sender, e) =>
            {
                MelonLogger.Msg($"Received Ready from user {e.User.Username}");
            };
            _client.OnPresenceUpdate += (sender, e) =>
            {
                MelonLogger.Msg($"Received Update! {e.Presence}");
            };
            _client.Initialize();
            _client.SetPresence(new RichPresence()
            {
                State = "Loading",
                Assets = new Assets()
                {
                    LargeImageKey = "bonelab",
                    LargeImageText = "BONELAB",
                    SmallImageKey = "bonelabsmall",
                    SmallImageText = "BONELAB"
                }
            });
        }

        public static void SetRpc(string state, string largeImageKey, string largeImageText, string smallImageKey, string smallImageText)
        {
            _client.SetPresence(new RichPresence()
            {
                State = state,
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