using Comfort.Common;
using EFT;
using EFT.InventoryLogic;

namespace RecoilStandalone
{
    public static class Helper
    {

        public static bool ProgramKEnabled = false;

        public static bool IsAllowedAim = true;

        public static bool IsAttemptingToReloadInternalMag = false;

        public static bool IsMagReloading = false;

        public static bool IsInReloadOpertation = false;

        public static bool NoMagazineReload = false;

        public static bool IsAttemptingRevolverReload = false;

        public static bool IsReady = false;

        public static bool WeaponReady = false;


        public static bool CheckIsReady()
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;
            SessionResultPanel sessionResultPanel = Singleton<SessionResultPanel>.Instance;

            if (gameWorld?.AllPlayers.Count > 0)
            {
                Player player = gameWorld.AllPlayers[0];
                if (player != null && player?.HandsController != null)
                {
                    if (player?.HandsController?.Item != null && player?.HandsController?.Item is Weapon)
                    {
                        Helper.WeaponReady = true;
                    }
                }
            }

            if (gameWorld == null || gameWorld.AllPlayers == null || gameWorld.AllPlayers.Count <= 0 || sessionResultPanel != null)
            {
                Helper.IsReady = false;
                return false;
            }
            Helper.IsReady = true;

            return true;
        }


        public static void SafelyAddAttributeToList(ItemAttributeClass itemAttribute, Mod __instance)
        {
            if (itemAttribute.Base() != 0f)
            {
                __instance.Attributes.Add(itemAttribute);
            }
        }
    }
}
