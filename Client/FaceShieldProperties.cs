using EFT.InventoryLogic;

namespace RecoilStandalone
{

    public static class FaceShieldProperties
    {
        public static bool AllowsADS(Item fs)
        {

            if (Helper.NullCheck(fs.ConflictingItems))
            {
                return true;
            }

            return bool.Parse(fs.ConflictingItems[1]);
        }
    }
}
