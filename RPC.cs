using System;
using System.IO;
using System.Reflection;
using DiscordRPC;
using MelonLoader;

namespace BLRPC
{
    public static class Rpc
    {
        public static bool HasLoadedLib;
        public static IntPtr RPCLibrary;

        public static void LoadAssembly()
        {
            if (HasLoadedLib) return;
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

            MelonLogger.Msg("Loading RPC from " + rpcDllPath);
            RPCLibrary = DllTools.LoadLibrary(rpcDllPath);
            HasLoadedLib = true;
        }
        
        public static DiscordRpcClient Client;
        
        public static void Initialize()
        {
            Client.OnReady += (sender, e) =>
            {
                MelonLogger.Msg($"Received Ready from user {e.User.Username}");
            };
            Client.OnPresenceUpdate += (sender, e) =>
            {
                MelonLogger.Msg($"Received Update! {e.Presence}");
            };
            Client.Initialize();
            Client.SetPresence(new RichPresence()
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
            Client.SetPresence(new RichPresence()
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