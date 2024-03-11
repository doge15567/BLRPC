using BLRPC.Melon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using ModioModNetworker;
using ModioModNetworker.Utilities;
using ModioModNetworker.Data;
using SLZ.Marrow.Warehouse;

namespace BLRPC.Internal
{
    internal class RemoteImageHandler
    {

        public static Dictionary<string, string> ImageCache = new Dictionary<string, string>();


        public static string CheckforExternalImage(string ImageKey, string FallBackKey)
        {

            ModConsole.Msg("Check External Image Called with value " + ImageKey, 1);

            if (ImageCache.ContainsKey(ImageKey))
            {
                ModConsole.Msg(ImageKey + " was already processed, and was cached with value " + ImageCache[ImageKey], 1);
                return ImageCache[ImageKey];
            }
            
            string ModioNetworkURI = GetModioImage(ImageKey, FallBackKey);
            if (ModioNetworkURI != null) { ImageCache.Add(ImageKey, ModioNetworkURI); }
            else { ImageCache.Add(ImageKey, FallBackKey); }

            ModConsole.Msg("Image Key Cache of " + ImageKey + " was set with " + ImageCache[ImageKey], 1);

            ModConsole.Msg("Check for external image"+ImageKey+" finished, with value " + ImageCache[ImageKey], 1);
            return ImageCache[ImageKey];
        }

        public static ModInfo GetModInfoForAvatarBarcode(string barcode) // Unimplemented in ModioModNetworker for some reason?????
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




        public static string GetModioImage(string imageKey, string FallbackKey)
        {
            ModInfo info;
            if (FallbackKey == "moddedmap") { info = ModInfoUtilities.GetModInfoForLevelBarcode(imageKey); }
            else if (FallbackKey == "moddedavatar") { info = GetModInfoForAvatarBarcode(imageKey); }
            else return null;
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

}


