﻿using System;
using BoneLib;
using System.IO;
using BLRPC.Internal;
using BLRPC.Melon;
using MelonLoader;
using UnityEngine;
using Random = System.Random;

namespace BLRPC
{
    public class Main : MelonMod
    {
        internal const string Name = "BLRPC";
        internal const string Description = "Discord Rich Presence for BONELAB";
        internal const string Author = "SoulWithMae";
        internal const string Company = "Weather Electric";
        internal const string Version = "1.1.0";
        internal const string DownloadLink = "null";
        
        // Stuff for userdata folder
        private static readonly string UserDataDirectory = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC");
        private static readonly string DLLPath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC", "discord_game_sdk.dll");
        private static readonly string UserEntriesPath = Path.Combine(MelonUtils.UserDataDirectory, "BLRPC", "UserEntries.txt");
        // Stuff for loading the discord game SDK assembly
        private static bool _hasLoadedLib;
        private static IntPtr _rpcLib;
        // Quest users.
        private bool _isQuest;
        public override void OnInitializeMelon()
        {
            ModConsole.Setup(LoggerInstance);
            if (Application.platform == RuntimePlatform.Android)
            {
                ModConsole.Error("You are on Quest! This mod won't work! Please use the PC version of BONELAB!");
                _isQuest = true;
            }
            if (_isQuest) return;
            Preferences.Setup();
            if (!Directory.Exists(UserDataDirectory))
            {
                ModConsole.Msg($"User data directory not found, creating at {UserDataDirectory}", LoggingMode.DEBUG);
                Directory.CreateDirectory(UserDataDirectory);
            }
            if (!File.Exists(DLLPath))
            {
                ModConsole.Msg($"Discord SDK not unpacked, unpacking at {DLLPath}", LoggingMode.DEBUG);
                File.WriteAllBytes(DLLPath, EmbeddedResource.GetResourceBytes("discord_game_sdk.dll"));
            }
            if (!File.Exists(UserEntriesPath))
            {
                ModConsole.Msg($"User entries file not unpacked, unpacking at {UserEntriesPath}", LoggingMode.DEBUG);
                File.WriteAllBytes(UserEntriesPath, EmbeddedResource.GetResourceBytes("UserEntries.txt"));
            }
            if (!_hasLoadedLib)
            {
                ModConsole.Msg($"Loading Discord SDK from {DLLPath}", LoggingMode.DEBUG);
                _rpcLib = DllTools.LoadLibrary(DLLPath);
                _hasLoadedLib = true;
            }
            ModConsole.Msg("Initializing RPC", LoggingMode.DEBUG);
            Rpc.Initialize();
            Hooking.OnLevelInitialized += OnLevelLoad;
        }

        public override void OnApplicationQuit()
        {
            if (_hasLoadedLib)
            {
                DllTools.FreeLibrary(_rpcLib);
            }
        }

        public override void OnUpdate()
        {
            if (_isQuest) return;
            Rpc.Discord.RunCallbacks();
        }

        private static void OnLevelLoad(LevelInfo levelInfo)
        {
            MelonLogger.Msg($"Level loaded: {levelInfo.title}", LoggingMode.DEBUG);
            DeathCounter.Counter = 0;
            ShotCounter.Counter = 0;
            GlobalVariables.status = $"In {levelInfo.title}";
            ModConsole.Msg($"Status is {GlobalVariables.status}", LoggingMode.DEBUG);
            GlobalVariables.largeImageKey = CheckMap(levelInfo.barcode);
            ModConsole.Msg($"Large image key is {GlobalVariables.largeImageKey}", LoggingMode.DEBUG);
            GlobalVariables.largeImageText = levelInfo.title;
            ModConsole.Msg($"Large image text is {GlobalVariables.largeImageText}", LoggingMode.DEBUG);
            if (Preferences.detailsMode != DetailsMode.NPCDeaths && Preferences.detailsMode != DetailsMode.GunShots && Preferences.detailsMode != DetailsMode.Extraes)
            {
                var details = GetEntry();
                ModConsole.Msg($"Details are {details}", LoggingMode.DEBUG);
                Rpc.SetRpc(details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
            }
            if (Preferences.detailsMode == DetailsMode.NPCDeaths)
            {
                Rpc.SetRpc("NPC Deaths: 0", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
            }
            if (Preferences.detailsMode == DetailsMode.GunShots)
            {
                Rpc.SetRpc("Gun Shots Fired: 0", GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
            }
            if (Preferences.detailsMode == DetailsMode.Extraes)
            {
                var details = ExtraesMode.RandomScreamingAboutNonsense();
                Rpc.SetRpc(details, GlobalVariables.status, GlobalVariables.largeImageKey, GlobalVariables.largeImageText);
            }
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