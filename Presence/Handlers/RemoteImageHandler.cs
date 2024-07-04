using System.Collections.Generic;
using Il2CppSLZ.Marrow.Warehouse;
using Il2CppSLZ.Marrow.Forklift.Model;
using System.Text.Json;
using System.IO;
using Il2CppSLZ.MLAgents;
//using Newtonsoft.Json;
using Il2CppSLZ.Serialize;
using Il2CppSLZ.Marrow;
using UnityEngine;


namespace BLRPC.Internal;

internal class RemoteImageHandler
{
    private static readonly Dictionary<string, string> ImageCache = new();


    public static string CheckforExternalImage(string imageKey, string fallBackKey)
    {

        ModConsole.Msg("Check External Image Called with value " + imageKey, 1);

        if (ImageCache.ContainsKey(imageKey))
        {
            ModConsole.Msg(imageKey + " was already processed, and was cached with value " + ImageCache[imageKey], 1);
            return ImageCache[imageKey];
        }

        if (GetModioImage(imageKey, fallBackKey) is { } modioNetworkUri) { ImageCache.Add(imageKey, modioNetworkUri); }
        else { ImageCache.Add(imageKey, fallBackKey); }

        ModConsole.Msg("Image Key Cache of " + imageKey + " was set with " + ImageCache[imageKey], 1);

        ModConsole.Msg("Check for external image"+imageKey+" finished, with value " + ImageCache[imageKey], 1);
        return ImageCache[imageKey];
    }

    /*
    private static ModInfo GetModInfoForAvatarBarcode(string barcode) // Unimplemented in ModioModNetworker for some reason?????
    {
        AvatarCrate avatarCrate =
            AssetWarehouse.Instance.GetCrate<AvatarCrate>(barcode);
        if (avatarCrate == null)
        {
            return null;
        }

        string palletBarcode = avatarCrate._pallet._barcode;
        return ModInfoUtilities.GetModInfoForPalletBarcode(palletBarcode);
    }
    */


    private static string GetModioImage(string imageKey, string fallbackKey)
    {
        ModInfo info;
        switch (fallbackKey)
        {
            case "moddedmap":
                info = ModInfoUtilities.GetModInfoForLevelBarcode(imageKey);
                break;
            case "moddedavatar":
                info = ModInfoUtilities.GetModInfoForAvatarBarcode(imageKey);
                break;
            default:
                return null;
        }
        if (info != null) { 
            if (info.isTracked)
            {
                ModConsole.Msg("Mod was Mod.io tracked and we returned " + info.thumbnailLink , 1);
                return info.thumbnailLink;
            } 
        }
        return null;
            
    }

}

public class ModInfo
{
    public string thumbnailLink { get; set; }
    public bool isTracked { get; set; }


}
internal class ModInfoUtilities
{
    public static ModInfo GetModInfoForLevelBarcode(string input)
    {
        LevelCrate crate = AssetWarehouse.Instance.GetCrate<LevelCrate>(input);
        Pallet pallet = crate.Pallet;

        return GetModInfoForPallet(pallet);

    }
    public static ModInfo GetModInfoForAvatarBarcode(string input)
    {
        AvatarCrate crate = AssetWarehouse.Instance.GetCrate<AvatarCrate>(input);
        Pallet pallet = crate.Pallet;

        return GetModInfoForPallet(pallet);
    }

    public static ModInfo GetModInfoForPallet(Pallet pallet)
    {
        ModConsole.Msg("Extracting ModInfo for pallet", 1);
        string manifestDir = PalletManifest.GetManifestPath(pallet);
        ModConsole.Msg("Manifest dir is " + manifestDir, 1);


        PalletPacker.TryUnpackManifestJsonFromFile(manifestDir, out var manifest, out var outjson);
        //BORN TO DIE
        //IL2CPP IS A FUCK
        //调试 Inline Em All 1093848
        //I am ripper
        //4781 SECONDS WASTED RECREATING A FUNCION ALREADY IN THE ASSEMBLY 
        //Weather Electric Experience


        ModConsole.Msg("Deserialized correctly", 1);


        string url = manifest.ModListing.ThumbnailUrl;

        ModConsole.Msg("URL is " + url, 1);

        ModConsole.Msg("Pallet: " + pallet.Title + "  Url:" + url,1);

        ModInfo returnInfo = new ModInfo { thumbnailLink = url, isTracked = (url != null) };

        return returnInfo;
    }

}