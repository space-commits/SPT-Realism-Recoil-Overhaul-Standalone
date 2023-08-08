using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.InventoryLogic;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using static EFT.Player;
using EFT.Interactive;
using System.Linq;
using PlayerInterface = GInterface114;
using BuffInfo = SkillsClass.GClass1743;
using AimingSettings = BackendConfigSettingsClass.GClass1358;

namespace RecoilStandalone
{


    public class PwaWeaponParamsPatch : ModulePatch
    {
        private static FieldInfo ginterface114Field;

        protected override MethodBase GetTargetMethod()
        {
            ginterface114Field = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "ginterface114_0");

            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("method_21", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            PlayerInterface playerInterface = (PlayerInterface)ginterface114Field.GetValue(__instance);
            if (playerInterface != null && playerInterface.Weapon != null)
            {
                Weapon weapon = playerInterface.Weapon;
                Player player = Singleton<GameWorld>.Instance.GetAlivePlayerByProfileID(weapon.Owner.ID);
                if (player != null && player.MovementContext.CurrentState.Name != EPlayerState.Stationary && player.IsYourPlayer)
                {
                   float swayIntensity = Plugin.SwayIntensity.Value;
                    __instance.Shootingg.Intensity = Plugin.RecoilIntensity.Value;
                    __instance.Breath.Intensity = swayIntensity * __instance.IntensityByPoseLevel; 
                    __instance.HandsContainer.HandsRotation.InputIntensity = (__instance.HandsContainer.HandsPosition.InputIntensity = swayIntensity * swayIntensity);
                    __instance.CrankRecoil = Plugin.EnableCrank.Value;
                }
            }
        }
    }

    public class UpdateSwayFactorsPatch : ModulePatch
    {
        private static FieldInfo ginterface114Field;

        protected override MethodBase GetTargetMethod()
        {
            ginterface114Field = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "ginterface114_0");

            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("UpdateSwayFactors", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            PlayerInterface ginterface114 = (PlayerInterface)ginterface114Field.GetValue(__instance);
            if (ginterface114 != null && ginterface114.Weapon != null)
            {
                Weapon weapon = ginterface114.Weapon;
                Player player = Singleton<GameWorld>.Instance.GetAlivePlayerByProfileID(weapon.Owner.ID);
                if (player != null && player.MovementContext.CurrentState.Name != EPlayerState.Stationary && player.IsYourPlayer)
                {
                    float float_20 = (float)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "float_20").GetValue(__instance);
                    __instance.MotionReact.SwayFactors = new Vector3(float_20, __instance.IsAiming ? (float_20 * 0.3f) : float_20, float_20) * Plugin.SwayIntensity.Value;
                } 
            }
        }
    }

    public class GetAimingPatch : ModulePatch
    {
        private static FieldInfo playerField;

        protected override MethodBase GetTargetMethod()
        {
            playerField = AccessTools.Field(typeof(EFT.Player.ItemHandsController), "_player");

            return typeof(EFT.Player.FirearmController).GetMethod("get_IsAiming", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(EFT.Player.FirearmController __instance, ref bool ____isAiming)
        {
            Player player = (Player)playerField.GetValue(__instance);
            if (player.IsYourPlayer)
            {
                Plugin.IsAiming = ____isAiming;
            }
        }
    }


    public class OnWeaponParametersChangedPatch : ModulePatch
    {
        private static FieldInfo iWeaponField;
        private static FieldInfo weaponClassField;
        private static FieldInfo buffInfoField;

        protected override MethodBase GetTargetMethod()
        {
            iWeaponField = AccessTools.Field(typeof(ShotEffector), "_weapon");
            weaponClassField = AccessTools.Field(typeof(ShotEffector), "_mainWeaponInHands");
            buffInfoField = AccessTools.Field(typeof(ShotEffector), "_buffs");

            return typeof(ShotEffector).GetMethod("OnWeaponParametersChanged", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref ShotEffector __instance)
        {
            IWeapon _weapon = (IWeapon)iWeaponField.GetValue(__instance);
            if (_weapon.Item.Owner.ID.StartsWith("pmc") || _weapon.Item.Owner.ID.StartsWith("scav"))
            {
                BuffInfo buffInfo = (BuffInfo)buffInfoField.GetValue(__instance);
                Weapon weaponClass = (Weapon)weaponClassField.GetValue(__instance);
                WeaponTemplate template = _weapon.WeaponTemplate;

                Plugin.CurrentlyEquipedWeapon = weaponClass;

                float cameraRecoil = template.CameraRecoil * Plugin.CamMulti.Value;
     
                Plugin.StartingCamRecoilX = (float)Math.Round(cameraRecoil, 4);
                Plugin.StartingCamRecoilY = (float)Math.Round(-cameraRecoil, 4);
                Plugin.CurrentCamRecoilX = Plugin.StartingCamRecoilX;
                Plugin.CurrentCamRecoilY = Plugin.StartingCamRecoilY;

                Plugin.StartingVRecoilX = (float)Math.Round(__instance.RecoilStrengthXy.x, 3);
                Plugin.StartingVRecoilY = (float)Math.Round(__instance.RecoilStrengthXy.y, 3);
                Plugin.CurrentVRecoilX = Plugin.StartingVRecoilX;
                Plugin.CurrentVRecoilY = Plugin.StartingVRecoilY;

                Plugin.StartingHRecoilX = (float)Math.Round(__instance.RecoilStrengthZ.x, 3);
                Plugin.StartingHRecoilY = (float)Math.Round(__instance.RecoilStrengthZ.y, 3);
                Plugin.CurrentHRecoilX = Plugin.StartingHRecoilX;
                Plugin.CurrentHRecoilY = Plugin.StartingHRecoilY;

                Plugin.StartingConvergence = (float)Math.Round(_weapon.WeaponTemplate.Convergence * Singleton<BackendConfigSettingsClass>.Instance.Aiming.RecoilConvergenceMult, 2);
                Plugin.CurrentConvergence = Plugin.StartingConvergence;
                Plugin.ConvergenceProporitonK = (float)Math.Round(Plugin.StartingConvergence * Plugin.StartingVRecoilX, 2);

                AimingSettings aiming = Singleton<BackendConfigSettingsClass>.Instance.Aiming;

                Plugin.StartingDamping = (float)Math.Round(Plugin.RecoilDamping.Value, 3);
                Plugin.CurrentDamping = Plugin.StartingDamping;

                Plugin.StartingHandDamping = (float)Math.Round(Plugin.HandsDamping.Value, 3);
                Plugin.CurrentHandDamping = Plugin.StartingHandDamping;
            }
        }
    }

    public class ProcessPatch : ModulePatch
    {
        private static FieldInfo iWeaponField;
        private static FieldInfo weaponClassField;
        private static FieldInfo intensityFactorsField;

        protected override MethodBase GetTargetMethod()
        {
            iWeaponField = AccessTools.Field(typeof(ShotEffector), "_weapon");
            weaponClassField = AccessTools.Field(typeof(ShotEffector), "_mainWeaponInHands");
            intensityFactorsField = AccessTools.Field(typeof(ShotEffector), "_separateIntensityFactors");

            return typeof(ShotEffector).GetMethod("Process");
        }

        [PatchPrefix]
        public static bool Prefix(ref ShotEffector __instance, float str = 1f)
        {
            IWeapon iWeapon = (IWeapon)iWeaponField.GetValue(__instance);
            Weapon weaponClass = (Weapon)weaponClassField.GetValue(__instance);

            if (iWeapon.Item.Owner.ID.StartsWith("pmc") || iWeapon.Item.Owner.ID.StartsWith("scav"))
            {

                Plugin.Timer = 0f;
                Plugin.IsFiring = true;
                Plugin.ShotCount++;

                Vector3 separateIntensityFactors = (Vector3)intensityFactorsField.GetValue(__instance);

                if (Plugin.ShotCount == 1 && (weaponClass.WeapClass == "pistol" || weaponClass.WeapClass == "shotgun" || weaponClass.WeapClass == "marksmanRifle" || weaponClass.WeapClass == "grenadeLauncher" || (weaponClass.WeapClass == "sniperRifle" && weaponClass.BoltAction == true)))
                {
                    __instance.RecoilStrengthXy.x = Plugin.CurrentVRecoilX * 1.7f;
                    __instance.RecoilStrengthXy.y = Plugin.CurrentVRecoilY * 1.7f;
                    __instance.RecoilStrengthZ.x = Plugin.CurrentHRecoilX * 1.7f;
                    __instance.RecoilStrengthZ.y = Plugin.CurrentHRecoilY * 1.7f;
                }
                else if (Plugin.ShotCount > 1 && weaponClass.SelectedFireMode == Weapon.EFireMode.fullauto)
                {
                    __instance.RecoilStrengthXy.x = Plugin.CurrentVRecoilX * 0.63f;
                    __instance.RecoilStrengthXy.y = Plugin.CurrentVRecoilY * 0.63f;
                    __instance.RecoilStrengthZ.x = Plugin.CurrentHRecoilX * 0.6f;
                    __instance.RecoilStrengthZ.y = Plugin.CurrentHRecoilY * 0.6f;
                }
                else
                {
                    __instance.RecoilStrengthZ.x = Plugin.CurrentHRecoilX;
                    __instance.RecoilStrengthZ.y = Plugin.CurrentHRecoilY;
                    __instance.RecoilStrengthXy.x = Plugin.CurrentVRecoilX;
                    __instance.RecoilStrengthXy.y = Plugin.CurrentVRecoilY;
                }

                if (Plugin.ShotCount > 1 && weaponClass.WeapClass == "pistol" && weaponClass.SelectedFireMode == Weapon.EFireMode.fullauto)
                {
                    __instance.RecoilStrengthZ.x *= 0.5f;
                    __instance.RecoilStrengthZ.y *= 0.5f;
                    __instance.RecoilStrengthXy.x *= 0.25f;
                    __instance.RecoilStrengthXy.y *= 0.25f;
                }

                __instance.RecoilRadian = __instance.RecoilDegree * 0.017453292f;

                __instance.ShotVals[3].Intensity = Plugin.CurrentCamRecoilX * str;
                __instance.ShotVals[4].Intensity = Plugin.CurrentCamRecoilY * str;

                float recoilRadian = Random.Range(__instance.RecoilRadian.x, __instance.RecoilRadian.y * Plugin.DispMulti.Value);
                float vertRecoil = Random.Range(__instance.RecoilStrengthXy.x, __instance.RecoilStrengthXy.y) * str * Plugin.VertMulti.Value;
                float hRecoil = Random.Range(__instance.RecoilStrengthZ.x, __instance.RecoilStrengthZ.y) * str * Plugin.HorzMulti.Value;
                __instance.RecoilDirection = new Vector3(-Mathf.Sin(recoilRadian) * vertRecoil * separateIntensityFactors.x, Mathf.Cos(recoilRadian) * vertRecoil * separateIntensityFactors.y, hRecoil * separateIntensityFactors.z) * __instance.Intensity;
                IWeapon weapon = iWeapon;
                Vector2 vector = (weapon != null) ? weapon.MalfState.OverheatBarrelMoveDir : Vector2.zero;
                IWeapon weapon2 = iWeapon;
                float num4 = (weapon2 != null) ? weapon2.MalfState.OverheatBarrelMoveMult : 0f;
                float num5 = (__instance.RecoilRadian.x + __instance.RecoilRadian.y) / 2f * ((__instance.RecoilStrengthXy.x + __instance.RecoilStrengthXy.y) / 2f) * num4;
                __instance.RecoilDirection.x = __instance.RecoilDirection.x + vector.x * num5;
                __instance.RecoilDirection.y = __instance.RecoilDirection.y + vector.y * num5;
                ShotEffector.ShotVal[] shotVals = __instance.ShotVals;
                for (int i = 0; i < shotVals.Length; i++)
                {
                    shotVals[i].Process(__instance.RecoilDirection);
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public class ShootPatch : ModulePatch
    {
        private static FieldInfo ginterface114Field;

        protected override MethodBase GetTargetMethod()
        {
            ginterface114Field = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "ginterface114_0");

            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("Shoot");
        }

        [PatchPostfix]
        public static void PatchPostfix(EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            PlayerInterface ginterface114 = (PlayerInterface)ginterface114Field.GetValue(__instance);

            if (ginterface114 != null && ginterface114.Weapon != null)
            {
                Weapon weapon = ginterface114.Weapon;
                Player player = Singleton<GameWorld>.Instance.GetAlivePlayerByProfileID(weapon.Owner.ID);
                if (player != null && player.IsYourPlayer && player.MovementContext.CurrentState.Name != EPlayerState.Stationary)
                {
                    __instance.HandsContainer.Recoil.Damping = Plugin.CurrentDamping;
                    __instance.HandsContainer.HandsPosition.Damping = Plugin.CurrentHandDamping;

                    if (Plugin.ShotCount <= 4)
                    {
                        __instance.HandsContainer.Recoil.ReturnSpeed = Plugin.CurrentConvergence * Plugin.ConSemiMulti.Value;
                    }
                    if (Plugin.ShotCount > 4)
                    {
                        __instance.HandsContainer.Recoil.ReturnSpeed = Plugin.CurrentConvergence * Plugin.ConAutoMulti.Value;
                    }

                }
            }
        }
    }

    public class SetCurveParametersPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EFT.Animations.RecoilSpring).GetMethod("SetCurveParameters");
        }
        [PatchPostfix]
        public static void PatchPostfix(EFT.Animations.RecoilSpring __instance)
        {
            float[] _originalKeyValues = (float[])AccessTools.Field(typeof(EFT.Animations.RecoilSpring), "_originalKeyValues").GetValue(__instance);

            float value = __instance.ReturnSpeedCurve[0].value;
            for (int i = 1; i < _originalKeyValues.Length; i++)
            {
                Keyframe key = __instance.ReturnSpeedCurve[i];
                key.value = value + _originalKeyValues[i] * Plugin.ConvergenceSpeedCurve.Value;
                __instance.ReturnSpeedCurve.RemoveKey(i);
                __instance.ReturnSpeedCurve.AddKey(key);
            }
        }
    }

}

