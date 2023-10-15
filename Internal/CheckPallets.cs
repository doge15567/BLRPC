using SLZ.Marrow.Warehouse;

namespace BLRPC.Internal
{
    public static class CheckPallets
    {
        public static int GetPalletCount()
        {
            var count = AssetWarehouse.Instance.GetPallets().Count;
            return count;
        }
    }
}