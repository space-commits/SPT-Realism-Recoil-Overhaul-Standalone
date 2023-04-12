using Aki.Common.Http;
using Aki.Common.Utils;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using EFT.InventoryLogic;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static RecoilStandalone.Attributes;

namespace RecoilStandalone
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> resetTime { get; set; }
        public static ConfigEntry<float> vRecoilLimit { get; set; }
        public static ConfigEntry<float> hRecoilLimit { get; set; }
        public static ConfigEntry<float> convergenceLimit { get; set; }
        public static ConfigEntry<float> convergenceResetRate { get; set; }
        public static ConfigEntry<float> vRecoilChangeMulti { get; set; }
        public static ConfigEntry<float> vRecoilResetRate { get; set; }
        public static ConfigEntry<float> hRecoilChangeMulti { get; set; }
        public static ConfigEntry<float> hRecoilResetRate { get; set; }
        public static ConfigEntry<float> SensChangeRate { get; set; }
        public static ConfigEntry<float> SensResetRate { get; set; }
        public static ConfigEntry<float> SensLimit { get; set; }
        public static ConfigEntry<bool> showCamRecoil { get; set; }
        public static ConfigEntry<bool> showDispersion { get; set; }
        public static ConfigEntry<bool> ReduceCamRecoil { get; set; }
        public static ConfigEntry<float> ConvergenceSpeedCurve { get; set; }
        public static ConfigEntry<bool> EnableRecoilClimb { get; set; }
        public static ConfigEntry<float> SwayIntensity { get; set; }
        public static ConfigEntry<float> RecoilIntensity { get; set; }
        public static ConfigEntry<bool> EnableStatsDelta { get; set; }
        public static ConfigEntry<bool> EnableHipfire { get; set; }
        public static ConfigEntry<float> VertMulti { get; set; }
        public static ConfigEntry<float> HorzMulti { get; set; }
        public static ConfigEntry<float> DispMulti { get; set; }
        public static ConfigEntry<float> CamMulti { get; set; }
        public static ConfigEntry<float> ConSemiMulti { get; set; }
        public static ConfigEntry<float> ConAutoMulti { get; set; }

        public static bool IsFiring = false;
        public static int ShotCount = 0;
        public static int PrevShotCount = ShotCount;
        public static bool StatsAreReset;

        public static float StartingAimSens;
        public static float CurrentAimSens = StartingAimSens;
        public static float StartingHipSens;
        public static float CurrentHipSens = StartingHipSens;
        public static bool CheckedForSens = false;

        public static float StartingDispersion;
        public static float CurrentDispersion;
        public static float DispersionProportionK;

        public static float StartingDamping;
        public static float CurrentDamping;

        public static float StartingHandDamping;
        public static float CurrentHandDamping;

        public static float StartingConvergence;
        public static float CurrentConvergence;
        public static float ConvergenceProporitonK;

        public static float StartingCamRecoilX;
        public static float StartingCamRecoilY;
        public static float CurrentCamRecoilX;
        public static float CurrentCamRecoilY;

        public static float StartingVRecoilX;
        public static float StartingVRecoilY;
        public static float CurrentVRecoilX;
        public static float CurrentVRecoilY;

        public static float StartingHRecoilX;
        public static float StartingHRecoilY;
        public static float CurrentHRecoilX;
        public static float CurrentHRecoilY;

        public static float Timer = 0.0f;
        public static bool IsAiming;

        public static bool LauncherIsActive = false;

        public static Weapon CurrentlyEquipedWeapon;

        public static Dictionary<Enum, Sprite> IconCache = new Dictionary<Enum, Sprite>();

        private void CacheIcons()
        {
            IconCache.Add(ENewItemAttributeId.ShotDispersion, Resources.Load<Sprite>("characteristics/icons/Velocity"));
            IconCache.Add(ENewItemAttributeId.BluntThroughput, Resources.Load<Sprite>("characteristics/icons/armorMaterial"));
            IconCache.Add(ENewItemAttributeId.VerticalRecoil, Resources.Load<Sprite>("characteristics/icons/Ergonomics"));
            IconCache.Add(ENewItemAttributeId.HorizontalRecoil, Resources.Load<Sprite>("characteristics/icons/Recoil Back"));
            IconCache.Add(ENewItemAttributeId.Dispersion, Resources.Load<Sprite>("characteristics/icons/Velocity"));
            IconCache.Add(ENewItemAttributeId.CameraRecoil, Resources.Load<Sprite>("characteristics/icons/SightingRange"));
            IconCache.Add(ENewItemAttributeId.AutoROF, Resources.Load<Sprite>("characteristics/icons/bFirerate"));
            IconCache.Add(ENewItemAttributeId.SemiROF, Resources.Load<Sprite>("characteristics/icons/bFirerate"));
            IconCache.Add(ENewItemAttributeId.ReloadSpeed, Resources.Load<Sprite>("characteristics/icons/weapFireType"));
            IconCache.Add(ENewItemAttributeId.FixSpeed, Resources.Load<Sprite>("characteristics/icons/icon_info_raidmoddable"));
            IconCache.Add(ENewItemAttributeId.ChamberSpeed, Resources.Load<Sprite>("characteristics/icons/icon_info_raidmoddable"));
            IconCache.Add(ENewItemAttributeId.AimSpeed, Resources.Load<Sprite>("characteristics/icons/SightingRange"));
            IconCache.Add(ENewItemAttributeId.Firerate, Resources.Load<Sprite>("characteristics/icons/bFirerate"));
            IconCache.Add(ENewItemAttributeId.Damage, Resources.Load<Sprite>("characteristics/icons/icon_info_bulletspeed"));
            IconCache.Add(ENewItemAttributeId.Penetration, Resources.Load<Sprite>("characteristics/icons/armorClass"));
            IconCache.Add(ENewItemAttributeId.ArmorDamage, Resources.Load<Sprite>("characteristics/icons/armorMaterial"));
            IconCache.Add(ENewItemAttributeId.FragmentationChance, Resources.Load<Sprite>("characteristics/icons/icon_info_bloodloss"));
            IconCache.Add(ENewItemAttributeId.MalfunctionChance, Resources.Load<Sprite>("characteristics/icons/icon_info_raidmoddable"));
            IconCache.Add(ENewItemAttributeId.CanSpall, Resources.Load<Sprite>("characteristics/icons/icon_info_bulletspeed"));
            IconCache.Add(ENewItemAttributeId.SpallReduction, Resources.Load<Sprite>("characteristics/icons/Velocity"));
            IconCache.Add(ENewItemAttributeId.GearReloadSpeed, Resources.Load<Sprite>("characteristics/icons/weapFireType"));
            IconCache.Add(ENewItemAttributeId.CanAds, Resources.Load<Sprite>("characteristics/icons/SightingRange"));
        }

        void Awake()
        {

            try
            {
                CacheIcons();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
            }
            string RecoilSettings = "2. Recoil Settings";
            string WeapStatSettings = "3. Weapon Stat Display Settings";
            string AdvancedRecoilSettings = "4. Advanced Recoil Settings";
            string WeaponSettings = "5. Weapon Settings";

            EnableHipfire = Config.Bind<bool>(RecoilSettings, "Enable Hipfire Recoil Climb", true, new ConfigDescription("Requires Restart. Enabled Recoil Climbing While Hipfiring", null, new ConfigurationManagerAttributes { Order = 10 }));
            ReduceCamRecoil = Config.Bind<bool>(RecoilSettings, "Reduce Camera Recoil", false, new ConfigDescription("Reduces Camera Recoil Per Shot. If Disabled, Camera Recoil Becomes More Intense As Weapon Recoil Increases.", null, new ConfigurationManagerAttributes { Order = 9 }));
            SensLimit = Config.Bind<float>(RecoilSettings, "Sensitivity Lower Limit", 0.4f, new ConfigDescription("Sensitivity Lower Limit While Firing. Lower Means More Sensitivity Reduction. 100% Means No Sensitivity Reduction.", new AcceptableValueRange<float>(0.1f, 1f), new ConfigurationManagerAttributes { Order = 8 }));
            RecoilIntensity = Config.Bind<float>(RecoilSettings, "Recoil Intensity", 1f, new ConfigDescription("Changes The Overall Intenisty Of Recoil. This Will Increase/Decrease Horizontal Recoil, Dispersion, Vertical Recoil.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 7 }));
            VertMulti = Config.Bind<float>(RecoilSettings, "Vertical Recoil Multi", 0.75f, new ConfigDescription("Up/Down.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 6 }));
            HorzMulti = Config.Bind<float>(RecoilSettings, "Horizontal Recoil Multi", 0.75f, new ConfigDescription("Forward/Back.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 5 }));
            DispMulti = Config.Bind<float>(RecoilSettings, "Dispersion Recoil Multi", 1.0f, new ConfigDescription("Spread.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 4 }));
            CamMulti = Config.Bind<float>(RecoilSettings, "Camera Recoil Multi", 1f, new ConfigDescription("Visual Recoil.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 3 }));
            ConSemiMulti = Config.Bind<float>(RecoilSettings, "Semi Convergence Multi", 2.5f, new ConfigDescription("Recoil Autocompensation. Higher = Snappier Recoil, Smaller Initial Jump.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 2 }));
            ConAutoMulti = Config.Bind<float>(RecoilSettings, "Auto Convergence Multi", 2f, new ConfigDescription("Recoil Autocompensation. Higher = Snappier Recoil, Smaller Initial Jump.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 1 }));

            EnableRecoilClimb = Config.Bind<bool>(AdvancedRecoilSettings, "Enable Recoil Climb", true, new ConfigDescription("The Core Of The Recoil Overhaul. Recoil Increase Per Shot, Nullifying Recoil Auto-Compensation In Full Auto And Requiring A Constant Pull Of The Mouse To Control Recoil. If Diabled Weapons Will Be Completely Unbalanced Without Stat Changes.", null, new ConfigurationManagerAttributes { Order = 13 }));
            SensChangeRate = Config.Bind<float>(AdvancedRecoilSettings, "Sensitivity Change Rate", 0.75f, new ConfigDescription("Rate At Which Sensitivity Is Reduced While Firing. Lower Means Faster Rate.", new AcceptableValueRange<float>(0.1f, 1f), new ConfigurationManagerAttributes { Order = 12 }));
            SensResetRate = Config.Bind<float>(AdvancedRecoilSettings, "Senisitivity Reset Rate", 1.2f, new ConfigDescription("Rate At Which Sensitivity Recovers After Firing. Higher Means Faster Rate.", new AcceptableValueRange<float>(1.01f, 2f), new ConfigurationManagerAttributes { Order = 11, IsAdvanced = true }));
            ConvergenceSpeedCurve = Config.Bind<float>(AdvancedRecoilSettings, "Convergence Curve Multi", 1f, new ConfigDescription("The Convergence Curve. Lower Means More Recoil.", new AcceptableValueRange<float>(0.01f, 1.5f), new ConfigurationManagerAttributes { Order = 10 }));
            vRecoilLimit = Config.Bind<float>(AdvancedRecoilSettings, "Vertical Recoil Upper Limit", 15.0f, new ConfigDescription("The Upper Limit For Vertical Recoil Increase As A Multiplier. E.g Value Of 10 Is A Limit Of 10x Starting Recoil.", new AcceptableValueRange<float>(1f, 50f), new ConfigurationManagerAttributes { Order = 9 }));
            vRecoilChangeMulti = Config.Bind<float>(AdvancedRecoilSettings, "Vertical Recoil Change Rate Multi", 1.0f, new ConfigDescription("A Multiplier For The Verftical Recoil Increase Per Shot.", new AcceptableValueRange<float>(0.9f, 1.1f), new ConfigurationManagerAttributes { Order = 8 }));
            vRecoilResetRate = Config.Bind<float>(AdvancedRecoilSettings, "Vertical Recoil Reset Rate", 0.91f, new ConfigDescription("The Rate At Which Vertical Recoil Resets Over Time After Firing. Lower Means Faster Rate.", new AcceptableValueRange<float>(0.1f, 0.99f), new ConfigurationManagerAttributes { Order = 7, IsAdvanced = true }));
            hRecoilLimit = Config.Bind<float>(AdvancedRecoilSettings, "Rearward Recoil Upper Limit", 1.5f, new ConfigDescription("The Upper Limit For Rearward Recoil Increase As A Multiplier. E.g Value Of 10 Is A Limit Of 10x Starting Recoil.", new AcceptableValueRange<float>(1f, 50f), new ConfigurationManagerAttributes { Order = 6 }));
            hRecoilChangeMulti = Config.Bind<float>(AdvancedRecoilSettings, "Rearward Recoil Change Rate Multi", 0.98f, new ConfigDescription("A Multiplier For The Rearward Recoil Increase Per Shot.", new AcceptableValueRange<float>(0.9f, 1.1f), new ConfigurationManagerAttributes { Order = 5 }));
            hRecoilResetRate = Config.Bind<float>(AdvancedRecoilSettings, "Rearward Recoil Reset Rate", 0.91f, new ConfigDescription("The Rate At Which Rearward Recoil Resets Over Time After Firing. Lower Means Faster Rate.", new AcceptableValueRange<float>(0.1f, 0.99f), new ConfigurationManagerAttributes { Order = 4, IsAdvanced = true }));
            convergenceResetRate = Config.Bind<float>(AdvancedRecoilSettings, "Convergence Reset Rate", 1.16f, new ConfigDescription("The Rate At Which Convergence Resets Over Time After Firing. Higher Means Faster Rate.", new AcceptableValueRange<float>(1.01f, 2f), new ConfigurationManagerAttributes { Order = 3, IsAdvanced = true }));
            convergenceLimit = Config.Bind<float>(AdvancedRecoilSettings, "Convergence Lower Limit", 0.3f, new ConfigDescription("The Lower Limit For Convergence. Convergence Is Kept In Proportion With Vertical Recoil While Firing, Down To The Set Limit. Value Of 0.3 Means Convegence Lower Limit Of 0.3 * Starting Convergance.", new AcceptableValueRange<float>(0.1f, 1.0f), new ConfigurationManagerAttributes { Order = 2 }));
            resetTime = Config.Bind<float>(AdvancedRecoilSettings, "Time Before Reset", 0.14f, new ConfigDescription("The Time In Seconds That Has To Be Elapsed Before Firing Is Considered Over, Stats Will Not Reset Until This Timer Is Done. Helps Prevent Spam Fire In Full Auto.", new AcceptableValueRange<float>(0.1f, 0.5f), new ConfigurationManagerAttributes { Order = 1 }));

            showCamRecoil = Config.Bind<bool>(WeapStatSettings, "Show Camera Recoil Stat", false, new ConfigDescription("Requiures Restart. Warning: Showing Too Many Stats On Weapons With Lots Of Slots Makes The Inspect Menu UI Difficult To Use.", null, new ConfigurationManagerAttributes { Order = 4 }));
            showDispersion = Config.Bind<bool>(WeapStatSettings, "Show Dispersion Stat", false, new ConfigDescription("Requiures Restart. Warning: Showing Too Many Stats On Weapons With Lots Of Slots Makes The Inspect Menu UI Difficult To Use.", null, new ConfigurationManagerAttributes { Order = 3 }));
          
            SwayIntensity = Config.Bind<float>(WeaponSettings, "Sway Intensity", 1f, new ConfigDescription("Changes The Intensity Of Aim Sway And Inertia.", new AcceptableValueRange<float>(0f, 2f), new ConfigurationManagerAttributes { Order = 1 }));



            new WeaponConstructorPatch().Enable();
            new GetAttributeIconPatches().Enable();
            new method_20Patch().Enable();
            new UpdateSwayFactorsPatch().Enable();
            new GetAimingPatch().Enable();
            new OnWeaponParametersChangedPatch().Enable();
            new ProcessPatch().Enable();
            new ShootPatch().Enable();
            new SetCurveParametersPatch().Enable();
            new UpdateSensitivityPatch().Enable();
            new AimingSensitivityPatch().Enable();
            new GetRotationMultiplierPatch().Enable();
        }

        void Update()
        {

            if (Helper.CheckIsReady())
            {

                if (Plugin.ShotCount > Plugin.PrevShotCount)
                {
                    Plugin.IsFiring = true;
                }

                if (Plugin.EnableRecoilClimb.Value == true && (Plugin.IsAiming == true || Plugin.EnableHipfire.Value == true))
                {
                    Recoil.DoRecoilClimb();
                }

                if (Plugin.ShotCount == Plugin.PrevShotCount)
                {
                    Plugin.Timer += Time.deltaTime;
                    if (Plugin.Timer >= Plugin.resetTime.Value)
                    {
                        Plugin.IsFiring = false;
                        Plugin.ShotCount = 0;
                        Plugin.PrevShotCount = 0;
                        Plugin.Timer = 0f;
                    }
                }

                if (Plugin.IsFiring == false)
                {
                    Recoil.ResetRecoil();
                }
            }
        }
    }
}

