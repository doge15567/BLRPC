using System.IO;
using MelonLoader;
using System;

namespace BLRPC
{
    public static class UserEntries
    {
        private static readonly string FolderPath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC");
        private static readonly string FilePath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC", "UserEntries.txt");
        public static void Setup()
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
                MelonLogger.Msg("Created BLRPC folder");
            }
            if (!File.Exists(FilePath))
            {
                File.WriteAllBytes(FilePath, EmbeddedResource.GetResourceBytes("UserEntries.txt"));
            }
        }
        public static string GetEntry()
        {
            var rnd = new Random();
            string[] lines = File.ReadAllLines(FilePath);
            int r = rnd.Next(lines.Length);
            return lines[r];
        }
    }
}