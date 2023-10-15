using BLRPC.Melon;
using SLZ.Marrow.Warehouse;

namespace BLRPC.Internal
{
    public static class CheckPallets
    {
        public static int GetPalletCount()
        {
            ModConsole.Msg("Getting pallet count", LoggingMode.DEBUG);
            var count = AssetWarehouse.Instance.GetPallets().Count;
            ModConsole.Msg($"Pallet count is {count}", LoggingMode.DEBUG);
            return count;
        }
    }
}