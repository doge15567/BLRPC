namespace BLRPC.Internal
{
    public static class CheckBarcode
    {
        public static string CheckAvatar(string name)
        {
            switch (name)
            {
                case BonelabAvatars.Heavy:
                    return "heavy";
                case BonelabAvatars.Fast:
                    return "fast";
                case BonelabAvatars.Short:
                    return "short";
                case BonelabAvatars.Tall:
                    return "tall";
                case BonelabAvatars.Strong:
                    return "strong";
                case BonelabAvatars.Light:
                    return "light";
                case BonelabAvatars.Jimmy:
                    return "jimmy";
                case BonelabAvatars.Ford:
                    return "ford";
                case BonelabAvatars.FordBL:
                    return "ford";
                case BonelabAvatars.PeasantFemaleA:
                    return "peasantfemalea";
                case BonelabAvatars.PeasantFemaleB:
                    return "peasantfemaleb";
                case BonelabAvatars.PeasantFemaleC:
                    return "peasantfemalec";
                case BonelabAvatars.PeasantMaleA:
                    return "peasantmalea";
                case BonelabAvatars.PeasantMaleB:
                    return "peasantmaleb";
                case BonelabAvatars.PeasantMaleC:
                    return "peasantmalec";
                case BonelabAvatars.Nullbody:
                    return "nullbody";
                case BonelabAvatars.Skeleton:
                    return "skeleton";
                case BonelabAvatars.SecurityGuard:
                    return "securityguard";
                case BonelabAvatars.DuckSeasonDog:
                    return "duckseasondog";
                case BonelabAvatars.PolyBlank:
                    return "polyblank";
                case BonelabAvatars.PolyDebugger:
                    return "polyblank";
                default:
                    return "moddedavatar";
            }
        }
        public static string CheckMap(string barcode)
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