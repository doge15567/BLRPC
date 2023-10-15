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
                File.Create(FilePath);
                MelonLogger.Msg("Created UserEntries.txt");
                File.WriteAllText(FilePath, "Got fenced in :(" + Environment.NewLine);
                File.AppendAllText(FilePath, "Plugging a USB device in.." + Environment.NewLine);
                File.AppendAllText(FilePath, "Stuck on Long Run's puzzle" + Environment.NewLine);
                File.AppendAllText(FilePath, "DAMN YOU PILLAR CLIMB." + Environment.NewLine);
                File.AppendAllText(FilePath, "Making another generic bodycam video" + Environment.NewLine);
                File.AppendAllText(FilePath, "Trying to get a good time on Monogon Motorway" + Environment.NewLine);
                File.AppendAllText(FilePath, "Suffering with My Pal Apollo" + Environment.NewLine);
                File.AppendAllText(FilePath, "Getting NullReference'd" + Environment.NewLine);
                File.AppendAllText(FilePath, "Getting stuck in the void" + Environment.NewLine);
                File.AppendAllText(FilePath, "Getting stuck in the void again" + Environment.NewLine);
                File.AppendAllText(FilePath, "I'm not even playing the game, I'm just testing my mods" + Environment.NewLine);
                File.AppendAllText(FilePath, "Stuck with a bunch of Quest kids in a Fusion lobby" + Environment.NewLine);
                File.AppendAllText(FilePath, "System.NullReferenceException: Object reference not set to an instance of an object" + Environment.NewLine);
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