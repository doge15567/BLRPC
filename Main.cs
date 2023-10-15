using System;
using BoneLib;
using System.IO;
using MelonLoader;

namespace BLRPC
{
    public class Main : MelonMod
    {
        internal const string Name = "BLRPC";
        internal const string Description = "Discord Rich Presence for BONELAB";
        internal const string Author = "SoulWithMae";
        internal const string Company = "Weather Electric";
        internal const string Version = "1.0.0";
        internal const string DownloadLink = "null";
        
        // Stuff for userdata folder
        private static readonly string UserDataDirectory = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC");
        private static readonly string DLLPath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC", "discord_game_sdk.dll");
        private static readonly string UserEntriesPath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC", "UserEntries.txt");
        // Stuff for loading the discord game SDK assembly
        private static bool _hasLoadedLib;
        private static IntPtr _rpcLib;
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Mod loaded, loading discord SDK");
            if (!Directory.Exists(UserDataDirectory))
            {
                Directory.CreateDirectory(UserDataDirectory);
            }
            if (!File.Exists(DLLPath))
            {
                File.WriteAllBytes(DLLPath, EmbeddedResource.GetResourceBytes("discord_game_sdk.dll"));
            }
            if (!File.Exists(UserEntriesPath))
            {
                File.WriteAllBytes(UserEntriesPath, EmbeddedResource.GetResourceBytes("UserEntries.txt"));
            }
            if (!_hasLoadedLib)
            {
                _rpcLib = DllTools.LoadLibrary(DLLPath);
                _hasLoadedLib = true;
            }
            MelonLogger.Msg($"Discord SDK loaded at {DLLPath}");
            Rpc.Initialize();
            MelonLogger.Msg("Hooking LevelLoad");
            Hooking.OnLevelInitialized += OnLevelLoad;
        }

        public override void OnApplicationQuit()
        {
            MelonLogger.Msg("Application quit, disposing RPC");
            if (_hasLoadedLib)
            {
                DllTools.FreeLibrary(_rpcLib);
            }
        }

        public override void OnUpdate()
        {
            Rpc.Discord.RunCallbacks();
        }

        private static void OnLevelLoad(LevelInfo levelInfo)
        {
            var title = levelInfo.title;
            var map = CheckMap(levelInfo.barcode);
            MelonLogger.Msg($"Level loaded: {title} ({map})");
            MelonLogger.Msg($"Barcode is {levelInfo.barcode}");
            var details = GetEntry();
            Rpc.SetRpc(details,"In " + title, map, title, "bonelabsmall", "BONELAB");
        }

        private static string GetEntry()
        {
            var rnd = new Random();
            var lines = File.ReadAllLines(UserEntriesPath);
            var r = rnd.Next(lines.Length);
            return lines[r];
        }

        private static string CheckMap(string barcode)
        {
            switch (barcode)
            {
                case BonelabMaps.MainMenu:
                    return "mainmenu";
                case BonelabMaps.Descent:
                    return "descent";
                case BonelabMaps.BLHub:
                    return "blhub";
                case BonelabMaps.LongRun:
                    return "longrun";
                case BonelabMaps.MineDive:
                    return "minedive";
                case BonelabMaps.BigAnomaly:
                    return "biganomaly";
                case BonelabMaps.StreetPuncher:
                    return "streetpuncher";
                case BonelabMaps.SprintBridge:
                    return "sprintbridge";
                case BonelabMaps.MagmaGate:
                    return "magmagate";
                case BonelabMaps.Moonbase:
                    return "moonbase";
                case BonelabMaps.MonogonMotorway:
                    return "monogonmotorway";
                case BonelabMaps.PillarClimb:
                    return "pillarclimb";
                case BonelabMaps.BigAnomaly2:
                    return "biganomaly2";
                case BonelabMaps.Ascent:
                    return "ascent";
                case BonelabMaps.Home:
                    return "home";
                case BonelabMaps.VoidG114:
                    return "voidg114";
                case BonelabMaps.Baseline:
                    return "baseline";
                case BonelabMaps.BigBoneBowling:
                    return "bigbonebowling";
                case BonelabMaps.DropPit:
                    return "droppit";
                case BonelabMaps.DungeonWarrior:
                    return "dungeonwarrior";
                case BonelabMaps.FantasyArena:
                    return "fantasyarena";
                case BonelabMaps.GunRange:
                    return "gunrange";
                case BonelabMaps.HalfwayPark:
                    return "halfwaypark";
                case BonelabMaps.Holochamber:
                    return "holochamber";
                case BonelabMaps.MuseumBasement:
                    return "museumbasement";
                case BonelabMaps.NeonParkour:
                    return "neonparkour";
                case BonelabMaps.NeonTrial:
                    return "neontrial";
                case BonelabMaps.Rooftops:
                    return "rooftops";
                case BonelabMaps.TunnelTipper:
                    return "tunneltipper";
                case BonelabMaps.Tuscany:
                    return "tuscany";
                case BonelabMaps.ContainerYard:
                    return "containeryard";
                case BonelabMaps.Mirror:
                    return "mirror";
                default:
                    return "moddedmap";
            }
        }
    }
}