using Comfort.Common;
using EFT;
using EFT.InventoryLogic;
/*using Silencer = GClass2124;
using FlashHider = GClass2121;
using MuzzleCombo = GClass2116;
using Barrel = GClass2138;
using Mount = GClass2134;
using Receiver = GClass2141;
using Stock = GClass2136;
using Charge = GClass2129;
using CompactCollimator = GClass2122;
using Collimator = GClass2121;
using AssaultScope = GClass2120;
using Scope = GClass2124;
using IronSight = GClass2123;
using SpecialScope = GClass2125;
using AuxiliaryMod = GClass2104;
using Foregrip = GClass2108;
using PistolGrip = GClass2140;
using Gasblock = GClass2109;
using Handguard = GClass2139;
using Bipod = GClass2106;
using Flashlight = GClass2113;
using TacticalCombo = GClass2112;*/


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
