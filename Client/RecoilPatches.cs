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

namespace RecoilStandalone
{


    public class method_20Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("method_20", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            Player.FirearmController firearmController = (Player.FirearmController)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "firearmController_0").GetValue(__instance);

            if (firearmController != null)
            {
                Player player = (Player)AccessTools.Field(typeof(EFT.Player.FirearmController), "_player").GetValue(firearmController);
                if (player.IsYourPlayer == true)
                {
                   float swayIntensity = Plugin.SwayIntensity.Value;
                    __instance.Shootingg.Intensity = Plugin.RecoilIntensity.Value;
                    __instance.Breath.Intensity = swayIntensity * __instance.IntensityByPoseLevel; 
                    __instance.HandsContainer.HandsRotation.InputIntensity = (__instance.HandsContainer.HandsPosition.InputIntensity = swayIntensity * swayIntensity);
                    __instance.CrankRecoil = true;
                }
            }
        }
    }

    public class UpdateSwayFactorsPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("UpdateSwayFactors", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            Player.FirearmController firearmController = (Player.FirearmController)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "firearmController_0").GetValue(__instance);

            if (firearmController != null)
            {
                Player player = (Player)AccessTools.Field(typeof(EFT.Player.FirearmController), "_player").GetValue(firearmController);
                if (player.IsYourPlayer == true) 
                {
                    float float_20 = (float)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "float_20").GetValue(__instance);
                    __instance.MotionReact.SwayFactors = new Vector3(float_20, __instance.IsAiming ? (float_20 * 0.3f) : float_20, float_20) * Plugin.SwayIntensity.Value;
                }
            }
        }
    }

    public class GetAimingPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EFT.Player.FirearmController).GetMethod("get_IsAiming", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(EFT.Player.FirearmController __instance, ref bool ____isAiming)
        {
            if (Helper.IsReady == true)
            {
                Player player = (Player)AccessTools.Field(typeof(EFT.Player.ItemHandsController), "_player").GetValue(__instance);
                if (!player.IsAI)
                {
                    Plugin.IsAiming = ____isAiming;
                }
            }
        }
    }


    public class OnWeaponParametersChangedPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ShotEffector).GetMethod("OnWeaponParametersChanged", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref ShotEffector __instance)
        {
            IWeapon _weapon = (IWeapon)AccessTools.Field(typeof(ShotEffector), "_weapon").GetValue(__instance);

            if (_weapon.Item.Owner.ID.StartsWith("pmc") || _weapon.Item.Owner.ID.StartsWith("scav"))
            {
                SkillsClass.GClass1675 buffInfo = (SkillsClass.GClass1675)AccessTools.Field(typeof(ShotEffector), "_buffs").GetValue(__instance);
                Weapon weaponClass = (Weapon)AccessTools.Field(typeof(ShotEffector), "_mainWeaponInHands").GetValue(__instance);
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

                BackendConfigSettingsClass.GClass1310 Aiming = Singleton<BackendConfigSettingsClass>.Instance.Aiming;

                Plugin.StartingDamping = (float)Math.Round(Aiming.RecoilDamping, 3);
                Plugin.CurrentDamping = Plugin.StartingDamping;

                Plugin.StartingHandDamping = (float)Math.Round(Aiming.RecoilHandDamping, 3);
                Plugin.CurrentHandDamping = Plugin.StartingHandDamping;

            }
        }
    }

    public class ProcessPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ShotEffector).GetMethod("Process");
        }
        [PatchPrefix]
        public static bool Prefix(ref ShotEffector __instance, float str = 1f)
        {

            IWeapon iWeapon = (IWeapon)AccessTools.Field(typeof(ShotEffector), "_weapon").GetValue(__instance);
            Weapon weaponClass = (Weapon)AccessTools.Field(typeof(ShotEffector), "_mainWeaponInHands").GetValue(__instance);

            if (iWeapon.Item.Owner.ID.StartsWith("pmc") || iWeapon.Item.Owner.ID.StartsWith("scav"))
            {

                Plugin.Timer = 0f;
                Plugin.IsFiring = true;
                Plugin.ShotCount++;

                Vector3 _separateIntensityFactors = (Vector3)AccessTools.Field(typeof(ShotEffector), "_separateIntensityFactors").GetValue(__instance);


                if (Plugin.ShotCount == 1)
                {
                    __instance.RecoilStrengthXy.x = Plugin.CurrentVRecoilX * 1.35f;
                    __instance.RecoilStrengthXy.y = Plugin.CurrentVRecoilY * 1.35f;
                    __instance.RecoilStrengthZ.x = Plugin.CurrentHRecoilX * 1.35f;
                    __instance.RecoilStrengthZ.y = Plugin.CurrentHRecoilY * 1.35f;
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

                if (weaponClass.WeapClass == "pistol" && weaponClass.SelectedFireMode == Weapon.EFireMode.fullauto)
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
                __instance.RecoilDirection = new Vector3(-Mathf.Sin(recoilRadian) * vertRecoil * _separateIntensityFactors.x, Mathf.Cos(recoilRadian) * vertRecoil * _separateIntensityFactors.y, hRecoil * _separateIntensityFactors.z) * __instance.Intensity;
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
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("Shoot");
        }
        [PatchPostfix]
        public static void PatchPostfix(EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            Player.FirearmController firearmController = (Player.FirearmController)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "firearmController_0").GetValue(__instance);

            if (firearmController != null)
            {
                Player player = (Player)AccessTools.Field(typeof(EFT.Player.FirearmController), "_player").GetValue(firearmController);
                if (player.IsYourPlayer == true)
                {
              
                    __instance.HandsContainer.Recoil.Damping = Plugin.CurrentDamping;
                    __instance.HandsContainer.HandsPosition.Damping = Plugin.CurrentHandDamping;

                    if (Plugin.ShotCount == 1)
                    {
                        __instance.HandsContainer.Recoil.ReturnSpeed = Plugin.CurrentConvergence * Plugin.ConSemiMulti.Value;
                    }
                    if (Plugin.ShotCount > 1)
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

