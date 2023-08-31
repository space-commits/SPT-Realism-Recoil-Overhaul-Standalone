using Aki.Common.Http;
using Aki.Common.Utils;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using Comfort.Common;
using Diz.Jobs;
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
using static System.Net.Mime.MediaTypeNames;

namespace RecoilStandalone
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> ResetTime { get; set; }
        public static ConfigEntry<float> ConvergenceSpeedCurve { get; set; }
        public static ConfigEntry<float> SwayIntensity { get; set; }
        public static ConfigEntry<float> RecoilIntensity { get; set; }
        public static ConfigEntry<float> VertMulti { get; set; }
        public static ConfigEntry<float> HorzMulti { get; set; }
        public static ConfigEntry<float> DispMulti { get; set; }
        public static ConfigEntry<float> CamMulti { get; set; }
        public static ConfigEntry<float> ConvergenceMulti { get; set; }
        public static ConfigEntry<float> RecoilDamping { get; set; }
        public static ConfigEntry<float> HandsDamping { get; set; }
        public static ConfigEntry<bool> EnableCrank { get; set; }

        public static ConfigEntry<float> ResetSpeed { get; set; }
        public static ConfigEntry<float> RecoilClimbFactor { get; set; }
        public static ConfigEntry<float> RecoilDispersionFactor { get; set; }
        public static ConfigEntry<float> RecoilDispersionSpeed { get; set; }
        public static ConfigEntry<float> RecoilSmoothness { get; set; }
        public static ConfigEntry<float> ResetSensitivity { get; set; }
        public static ConfigEntry<bool> ResetVertical { get; set; }
        public static ConfigEntry<bool> ResetHorizontal { get; set; }

        public static ConfigEntry<float> test1 { get; set; }
        public static ConfigEntry<float> test2 { get; set; }
        public static ConfigEntry<float> test3 { get; set; }
        public static ConfigEntry<float> test4 { get; set; }

        public static bool IsFiring = false;
        public static int ShotCount = 0;
        public static int PrevShotCount = ShotCount;
        public static bool StatsAreReset;

        public static float RecoilAngle;
        public static float TotalDispersion;
        public static float TotalDamping;
        public static float TotalHandDamping;
        public static float TotalConvergence;
        public static float TotalCameraRecoil;
        public static float TotalVRecoil;
        public static float TotalHRecoil;

        public static float BreathIntensity = 1f;
        public static float HandsIntensity = 1f;
        public static float SetRecoilIntensity = 1f;

        public static float MountingSwayBonus = 1f;
        public static float MountingRecoilBonus = 1f;
        public static float BracingSwayBonus = 1f;
        public static float BracingRecoilBonus = 1f;
        public static bool IsMounting = false;

        public static float Timer = 0.0f;
        public static bool IsAiming;

        public static bool LauncherIsActive = false;

        public static Weapon CurrentlyEquipedWeapon;

        public static bool RealismModIsPresent = false;
        public static bool CombatStancesIsPresent = false;
        private static bool checkedForOtherMods = false;
        private static bool warnedUser = false;

        void Awake()
        {
            string testing = "0. Testing";
            string RecoilSettings = "1. Recoil Settings";
            string AdvancedRecoilSettings = "2. Advanced Recoil Settings";
            string WeaponSettings = "3. Weapon Settings";

            test1 = Config.Bind<float>(testing, "test 1", 1f, new ConfigDescription("", new AcceptableValueRange<float>(-5000f, 5000f), new ConfigurationManagerAttributes { Order = 600, IsAdvanced = true }));
            test2 = Config.Bind<float>(testing, "test 2", 1f, new ConfigDescription("", new AcceptableValueRange<float>(-5000f, 5000f), new ConfigurationManagerAttributes { Order = 500, IsAdvanced = true }));
            test3 = Config.Bind<float>(testing, "test 3", 1f, new ConfigDescription("", new AcceptableValueRange<float>(-5000f, 5000f), new ConfigurationManagerAttributes { Order = 400, IsAdvanced = true }));
            test4 = Config.Bind<float>(testing, "test 4", 1f, new ConfigDescription("", new AcceptableValueRange<float>(-5000f, 5000f), new ConfigurationManagerAttributes { Order = 300, IsAdvanced = true }));

            RecoilSmoothness = Config.Bind<float>(RecoilSettings, "Recoil Smoothness", 0.05f, new ConfigDescription("How Fast Recoil Moves Weapon While Firing, Higher Value Increases Smoothness.", new AcceptableValueRange<float>(0f, 2f), new ConfigurationManagerAttributes { Order = 130 }));
            ResetSpeed = Config.Bind<float>(RecoilSettings, "Reset Speed", 0.005f, new ConfigDescription("How Fast The Weapon's Vertical Position Resets After Firing.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 120 }));
            RecoilClimbFactor = Config.Bind<float>(RecoilSettings, "Recoil Climb Multi", 0.2f, new ConfigDescription("Multiplier For How Much The Weapon Climbs Vertically Per Shot.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 110 }));
            ResetSensitivity = Config.Bind<float>(RecoilSettings, "Reset Sensitvity", 0.15f, new ConfigDescription("The Amount Of Mouse Movement After Firing Needed To Cancel Reseting Back To Weapon's Original Position.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 100 }));
            ResetVertical = Config.Bind<bool>(RecoilSettings, "Enable Vertical Reset", true, new ConfigDescription("Enables Weapon Reseting Back To Original Vertical Position.", null, new ConfigurationManagerAttributes { Order = 90 }));
            ResetHorizontal = Config.Bind<bool>(RecoilSettings, "Enable Horizontal Reset", false, new ConfigDescription("Enables Weapon Reseting Back To Original Horizontal Position.", null, new ConfigurationManagerAttributes { Order = 80 }));
            RecoilDispersionFactor = Config.Bind<float>(RecoilSettings, "Recoil Dispersion Multi", 0.01f, new ConfigDescription("Increases The Size The Classic S Pattern.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 70 }));
            RecoilDispersionSpeed = Config.Bind<float>(RecoilSettings, "Recoil Dispersion Speed", 2f, new ConfigDescription("Increases The Speed At Which Recoil Makes The Classic S Pattern.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 60 }));

            RecoilIntensity = Config.Bind<float>(RecoilSettings, "Recoil Intensity", 1f, new ConfigDescription("Changes The Overall Intenisty Of Recoil. This Will Increase/Decrease Horizontal Recoil, Dispersion, Vertical Recoil. Does Not Affect Recoil Climb, Mostly Spread And Visual.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 50 }));
            VertMulti = Config.Bind<float>(RecoilSettings, "Vertical Recoil Multi.", 0.55f, new ConfigDescription("Up/Down.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 40 }));
            HorzMulti = Config.Bind<float>(RecoilSettings, "Horizontal Recoil Multi", 1.0f, new ConfigDescription("Forward/Back.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 30 }));
            DispMulti = Config.Bind<float>(RecoilSettings, "Dispersion Recoil Multi", 1.0f, new ConfigDescription("Spread.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 20 }));
            CamMulti = Config.Bind<float>(RecoilSettings, "Camera Recoil Multi", 1f, new ConfigDescription("Visual Recoil.", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { Order = 10 }));
            ConvergenceMulti = Config.Bind<float>(RecoilSettings, "Convergence Multi", 15f, new ConfigDescription("Higher = Snappier Recoil, Faster Reset.", new AcceptableValueRange<float>(0f, 40f), new ConfigurationManagerAttributes { Order = 1 }));
             
            ConvergenceSpeedCurve = Config.Bind<float>(AdvancedRecoilSettings, "Convergence Curve Multi", 1f, new ConfigDescription("The Convergence Curve. Lower Means More Recoil.", new AcceptableValueRange<float>(0.01f, 1.5f), new ConfigurationManagerAttributes { Order = 100 }));
            ResetTime = Config.Bind<float>(AdvancedRecoilSettings, "Time Before Reset", 0.14f, new ConfigDescription("The Time In Seconds That Has To Be Elapsed Before Firing Is Considered Over, Recoil Will Not Reset Until It Is Over.", new AcceptableValueRange<float>(0.1f, 0.5f), new ConfigurationManagerAttributes { Order = 10 }));
            EnableCrank = Config.Bind<bool>(AdvancedRecoilSettings, "Rearward Recoil", true, new ConfigDescription("Makes Recoil Go Towards Player's Shoulder Instead Of Forward.", null, new ConfigurationManagerAttributes { Order = 3 }));
            HandsDamping = Config.Bind<float>(AdvancedRecoilSettings, "Rearward Recoil Wiggle", 0.7f, new ConfigDescription("The Amount Of Wiggle After Firing.", new AcceptableValueRange<float>(0.2f, 0.9f), new ConfigurationManagerAttributes { Order = 1 }));
            RecoilDamping = Config.Bind<float>(AdvancedRecoilSettings, "Vertical Recoil Wiggle", 0.7f, new ConfigDescription("The Amount Of Wiggle After Firing.", new AcceptableValueRange<float>(0.2f, 0.9f), new ConfigurationManagerAttributes { Order = 2 }));
           
            SwayIntensity = Config.Bind<float>(WeaponSettings, "Sway Intensity", 1f, new ConfigDescription("Changes The Intensity Of Aim Sway And Inertia.", new AcceptableValueRange<float>(0f, 2f), new ConfigurationManagerAttributes { Order = 1 }));

            new UpdateWeaponVariablesPatch().Enable();
            new UpdateSwayFactorsPatch().Enable();
            new GetAimingPatch().Enable();
            new ProcessPatch().Enable();
            new ShootPatch().Enable();
            new SetCurveParametersPatch().Enable();
            new PlayerLateUpdatePatch().Enable();
            new RotatePatch().Enable();
            new ApplyComplexRotationPatch().Enable();
        }

        void Update()
        {
            if (!checkedForOtherMods)
            {
                RealismModIsPresent = Chainloader.PluginInfos.ContainsKey("RealismMod");
                CombatStancesIsPresent = Chainloader.PluginInfos.ContainsKey("CombatStances") && Chainloader.PluginInfos.ContainsKey("StanceRecoilBridge");

                checkedForOtherMods = true;
                Logger.LogWarning("Combat Stances Is Present = " + Chainloader.PluginInfos.ContainsKey("CombatStances"));
                Logger.LogWarning("Realism Mod Is Present = " + RealismModIsPresent);
            }

            if ((int)Time.time % 5 == 0 && !warnedUser)
            {
                warnedUser = true;
                if (Chainloader.PluginInfos.ContainsKey("CombatStances") && !Chainloader.PluginInfos.ContainsKey("StanceRecoilBridge"))
                {
                    NotificationManagerClass.DisplayWarningNotification("ERROR: COMBAT STANCES IS INSTALLED BUT THE COMPATIBILITY BRIDGE IS NOT! INSTALL IT BEFORE USING THESE MODS TOGETHER!", EFT.Communications.ENotificationDurationType.Long);
                }
                if (Chainloader.PluginInfos.ContainsKey("RealismMod"))
                {
                    NotificationManagerClass.DisplayWarningNotification("ERROR: REALISM MOD IS ALSO INSTALLED! REALISM ALREADY CONTAINS RECOIL OVERHAUL, UNINSTALL ONE OF THESE MODS!", EFT.Communications.ENotificationDurationType.Long);
                }
                if (!Chainloader.PluginInfos.ContainsKey("CombatStances") && Chainloader.PluginInfos.ContainsKey("StanceRecoilBridge"))
                {
                    NotificationManagerClass.DisplayWarningNotification("ERROR: COMBAT STANCES COMPATIBILITY BRDIGE IS INSTALLED BUT COMBAT STANCES IS NOT! REMOVE THE BRIDGE OR INSTALL COMBAT STANCES!", EFT.Communications.ENotificationDurationType.Long);
                }
            }
            if ((int)Time.time % 5 != 0)
            {
                warnedUser = false;
            }
  
            if (Utils.CheckIsReady())
            {

                if (Plugin.ShotCount > Plugin.PrevShotCount)
                {
                    Plugin.IsFiring = true;
                    Plugin.PrevShotCount = Plugin.ShotCount;
                }

                if (Plugin.ShotCount == Plugin.PrevShotCount)
                {
                    Plugin.Timer += Time.deltaTime;
                    if (Plugin.Timer >= Plugin.ResetTime.Value)
                    {
                        Plugin.IsFiring = false;
                        Plugin.ShotCount = 0;
                        Plugin.PrevShotCount = 0;
                        Plugin.Timer = 0f;
                    }
                }
            }
        }
    }
}

