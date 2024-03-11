using System.Collections.Generic;
using ModioModNetworker.Utilities;
using ModioModNetworker.Data;
using SLZ.Marrow.Warehouse;

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


    private static string GetModioImage(string imageKey, string fallbackKey)
    {
        ModInfo info;
        switch (fallbackKey)
        {
            case "moddedmap":
                info = ModInfoUtilities.GetModInfoForLevelBarcode(imageKey);
                break;
            case "moddedavatar":
                info = GetModInfoForAvatarBarcode(imageKey);
                break;
            default:
                return null;
        }
        if (info != null) { 
            if (info.IsTracked())
            {
                ModConsole.Msg("Mod was Mod.io tracked and we returned " + info.thumbnailLink , 1);
                return info.thumbnailLink;
            } 
        }
        return null;
            
    }

}