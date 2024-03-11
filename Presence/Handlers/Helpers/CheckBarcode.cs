using BLRPC.Internal;
using BoneLib;

namespace BLRPC.Presence.Handlers.Helpers;

internal static class CheckBarcode
{
    public static string CheckAvatar(string barcode)
    {
        return barcode switch
        {
            CommonBarcodes.Avatars.Heavy => "heavy",
            CommonBarcodes.Avatars.Fast => "fast",
            CommonBarcodes.Avatars.Short => "short",
            CommonBarcodes.Avatars.Tall => "tall",
            CommonBarcodes.Avatars.Strong => "strong",
            CommonBarcodes.Avatars.Light => "light",
            CommonBarcodes.Avatars.Jimmy => "jimmy",
            CommonBarcodes.Avatars.FordBW => "ford",
            CommonBarcodes.Avatars.FordBL => "ford",
            CommonBarcodes.Avatars.PeasantFemaleA => "peasantfemalea",
            CommonBarcodes.Avatars.PeasantFemaleB => "peasantfemaleb",
            CommonBarcodes.Avatars.PeasantFemaleC => "peasantfemalec",
            CommonBarcodes.Avatars.PeasantMaleA => "peasantmalea",
            CommonBarcodes.Avatars.PeasantMaleB => "peasantmaleb",
            CommonBarcodes.Avatars.PeasantMaleC => "peasantmalec",
            CommonBarcodes.Avatars.Nullbody => "nullbody",
            CommonBarcodes.Avatars.Skeleton => "skeleton",
            CommonBarcodes.Avatars.SecurityGuard => "securityguard",
            CommonBarcodes.Avatars.DuckSeasonDog => "duckseasondog",
            CommonBarcodes.Avatars.PolyBlank => "polyblank",
            CommonBarcodes.Avatars.PolyDebugger => "polyblank",
            _ => RemoteImageHandler.CheckforExternalImage(barcode, "moddedavatar")
        };
    }
    public static string CheckMap(string barcode)
    {
        return barcode switch
        {
            CommonBarcodes.Maps.MainMenu => "mainmenu",
            CommonBarcodes.Maps.Descent => "descent",
            CommonBarcodes.Maps.BLHub => "blhub",
            CommonBarcodes.Maps.LongRun => "longrun",
            CommonBarcodes.Maps.MineDive => "minedive",
            CommonBarcodes.Maps.BigAnomaly => "biganomaly",
            CommonBarcodes.Maps.StreetPuncher => "streetpuncher",
            CommonBarcodes.Maps.SprintBridge => "sprintbridge",
            CommonBarcodes.Maps.MagmaGate => "magmagate",
            CommonBarcodes.Maps.Moonbase => "moonbase",
            CommonBarcodes.Maps.MonogonMotorway => "monogonmotorway",
            CommonBarcodes.Maps.PillarClimb => "pillarclimb",
            CommonBarcodes.Maps.BigAnomaly2 => "biganomaly2",
            CommonBarcodes.Maps.Ascent => "ascent",
            CommonBarcodes.Maps.Home => "home",
            CommonBarcodes.Maps.VoidG114 => "voidg114",
            CommonBarcodes.Maps.Baseline => "baseline",
            CommonBarcodes.Maps.BigBoneBowling => "bigbonebowling",
            CommonBarcodes.Maps.DropPit => "droppit",
            CommonBarcodes.Maps.DungeonWarrior => "dungeonwarrior",
            CommonBarcodes.Maps.FantasyArena => "fantasyarena",
            CommonBarcodes.Maps.GunRange => "gunrange",
            CommonBarcodes.Maps.HalfwayPark => "halfwaypark",
            CommonBarcodes.Maps.Holochamber => "holochamber",
            CommonBarcodes.Maps.MuseumBasement => "museumbasement",
            CommonBarcodes.Maps.NeonParkour => "neonparkour",
            CommonBarcodes.Maps.NeonTrial => "neontrial",
            CommonBarcodes.Maps.Rooftops => "rooftops",
            CommonBarcodes.Maps.TunnelTipper => "tunneltipper",
            CommonBarcodes.Maps.Tuscany => "tuscany",
            CommonBarcodes.Maps.ContainerYard => "containeryard",
            CommonBarcodes.Maps.Mirror => "mirror",
            _ => RemoteImageHandler.CheckforExternalImage(barcode,"moddedmap")
        };
    }
}