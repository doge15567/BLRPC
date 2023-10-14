using BoneLib;
using MelonLoader;

namespace BLRPC
{
    public class Main : MelonMod
    {
        internal const string Name = "BLRPC"; // Required
        internal const string Description = "Discord Rich Presence for BONELAB"; // Required
        internal const string Author = "SoulWithMae"; // Required
        internal const string Company = "Weather Electric"; // Set as null if blank
        internal const string Version = "1.0.0"; // Required
        internal const string DownloadLink = "null"; // Set as null if blank
        
        public override void OnInitializeMelon()
        {
            Rpc.LoadAssembly();
            Rpc.Initialize();
            Hooking.OnLevelInitialized += OnLevelLoad;
        }

        public override void OnApplicationQuit()
        {
            if (Rpc.HasLoadedLib) DllTools.FreeLibrary(Rpc.RPCLibrary);
            Rpc.Client.Dispose();
        }

        private static void OnLevelLoad(LevelInfo levelInfo)
        {
            var title = levelInfo.title;
            var map = CheckMap(levelInfo.barcode);
            Rpc.SetRpc("In " + title, map, title, "bonelabsmall", "BONELAB");
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
                case BonelabMaps.LoadDefault:
                    return "loaddefault";
                case BonelabMaps.LoadMod:
                    return "loadmod";
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
                default:
                    return "moddedmap";
            }
        }
    }
}