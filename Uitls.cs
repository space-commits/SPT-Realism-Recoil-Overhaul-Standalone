using Comfort.Common;
using EFT;
using EFT.InventoryLogic;

namespace RecoilStandalone
{
    public static class Uitls
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

            Player player = gameWorld.MainPlayer;
            if (player != null && player?.HandsController != null)
            {
                if (player?.HandsController?.Item != null && player?.HandsController?.Item is Weapon)
                {
                    Uitls.WeaponReady = true;
                }
                else
                {
                    Uitls.WeaponReady = false;
                }
            }

            if (gameWorld == null || gameWorld.AllAlivePlayersList == null || gameWorld.MainPlayer == null || sessionResultPanel != null)
            {
                Uitls.IsReady = false;
                return false;
            }
            Uitls.IsReady = true;

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
