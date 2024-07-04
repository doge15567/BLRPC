using System.IO;
using BLRPC.Internal;
using MelonLoader.Utils;

namespace BLRPC.Melon;

internal static class UserData
{
    private static readonly string UserDataDirectory = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric/BLRPC");
    private static readonly string LegacyDirectory = Path.Combine(MelonEnvironment.UserDataDirectory, "BLRPC");
    private static readonly string DLLPath = Path.Combine(UserDataDirectory, "discord_game_sdk.dll");
    public static readonly string UserEntriesPath = Path.Combine(UserDataDirectory, "UserEntries.txt");
    
    private static bool _hasLoadedLib;
    private static IntPtr _rpcLib;
    
    public static void Setup()
    {
        if (!Directory.Exists(UserDataDirectory))
        {
            ModConsole.Msg($"User data directory not found, creating at {UserDataDirectory}", 1);
            Directory.CreateDirectory(UserDataDirectory);
        }
        if (!File.Exists(DLLPath))
        {
            ModConsole.Msg($"Discord SDK not unpacked, checking legacy path", 1);
            if (Directory.Exists(LegacyDirectory) && File.Exists(Path.Combine(LegacyDirectory, "discord_game_sdk.dll")))
            {
                File.Move(Path.Combine(LegacyDirectory, "discord_game_sdk.dll"), DLLPath);
            }
            else
            {
                ModConsole.Msg($"Legacy path not found, creating at {DLLPath}", 1);
                File.WriteAllBytes(DLLPath, EmbeddedResource.GetResourceBytes("discord_game_sdk.dll"));
            }
        }
        if (!File.Exists(UserEntriesPath))
        {
            ModConsole.Msg($"User entries file not unpacked, checking legacy path", 1);
            if (Directory.Exists(LegacyDirectory) && File.Exists(Path.Combine(LegacyDirectory, "UserEntries.txt")))
            {
                var entries = Path.Combine(LegacyDirectory, "UserEntries.txt");
                File.Move(entries, UserEntriesPath);
            }
            else
            {
                ModConsole.Msg($"Legacy path not found, creating at {UserEntriesPath}", 1);
                File.WriteAllBytes(UserEntriesPath, EmbeddedResource.GetResourceBytes("UserEntries.txt"));
            }
        }
        if (!_hasLoadedLib)
        {
            ModConsole.Msg($"Loading Discord SDK from {DLLPath}", 1);
            _rpcLib = DllTools.LoadLibrary(DLLPath);
            _hasLoadedLib = true;
        }
    }
    
    public static void Dispose()
    {
        if (_hasLoadedLib)
        {
            DllTools.FreeLibrary(_rpcLib);
            _hasLoadedLib = false;
        }
    }
}