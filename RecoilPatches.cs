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
using InventoryItemHandler = GClass2672;
using EFT.Animations;
using System.Collections.Generic;

namespace RecoilStandalone
{
    public class ApplyComplexRotationPatch : ModulePatch
    {
        private static FieldInfo weapRotationField;
        private static FieldInfo currentRotationField;
        private static FieldInfo pitchField;
        private static FieldInfo blindfireRotationField;
        private static PropertyInfo overlappingBlindfireField;

        protected override MethodBase GetTargetMethod()
        {
            pitchField = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "float_14");
            blindfireRotationField = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "vector3_6");
            weapRotationField = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "quaternion_6");
            currentRotationField = AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "quaternion_1");
            overlappingBlindfireField = AccessTools.Property(typeof(EFT.Animations.ProceduralWeaponAnimation), "Single_3");

            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("ApplyComplexRotation", BindingFlags.Instance | BindingFlags.Public);
        }

        private static Vector3 currentRecoil = Vector3.zero;
        private static Vector3 targetRecoil = Vector3.zero;

        [PatchPostfix]
        private static void Postfix(ref EFT.Animations.ProceduralWeaponAnimation __instance, float dt)
        {
            if (!Plugin.CombatStancesIsPresent)
            {
                PlayerInterface playerInterface = (PlayerInterface)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "ginterface114_0").GetValue(__instance);
                if (playerInterface != null && playerInterface.Weapon != null)
                {
                    Weapon weapon = playerInterface.Weapon;
                    Player player = Singleton<GameWorld>.Instance.GetAlivePlayerByProfileID(weapon.Owner.ID);
                    if (player != null && player.IsYourPlayer)
                    {
                        float pitch = (float)pitchField.GetValue(__instance);
                        float overlappingBlindfire = (float)overlappingBlindfireField.GetValue(__instance);
                        Vector3 blindFireRotation = (Vector3)blindfireRotationField.GetValue(__instance);
                        Quaternion currentRotation = (Quaternion)currentRotationField.GetValue(__instance);
                        Vector3 weaponWorldPos = __instance.HandsContainer.WeaponRootAnim.position;

                        Quaternion weapRotation = (Quaternion)weapRotationField.GetValue(__instance);
                        Quaternion rhs = Quaternion.Euler(pitch * overlappingBlindfire * blindFireRotation);

                        RecoilController.DoCantedRecoil(ref targetRecoil, ref currentRecoil, ref weapRotation);
                        __instance.HandsContainer.WeaponRootAnim.SetPositionAndRotation(weaponWorldPos, weapRotation * rhs * currentRotation);
                    }
                }
            }
        }
    }

    public class RotatePatch : ModulePatch
    {
        private static Vector2 recordedRotation = Vector3.zero;
        private static Vector2 targetRotation = Vector3.zero;
        private static bool hasReset = false;
        private static float timer = 0.0f;
        private static float resetTime = 0.5f;

        private static float spiralTime;

        protected override MethodBase GetTargetMethod()
        {
            return typeof(MovementState).GetMethod("Rotate", BindingFlags.Instance | BindingFlags.Public);
        }

        private static void resetTimer(Vector2 target, Vector2 current)
        {
            timer += Time.deltaTime;

            if (timer >= resetTime && target == current)
            {
                hasReset = true;
            }
        }

        [PatchPrefix]
        private static bool Prefix(MovementState __instance, ref Vector2 deltaRotation, bool ignoreClamp)
        {
            GClass1667 MovementContext = (GClass1667)AccessTools.Field(typeof(MovementState), "MovementContext").GetValue(__instance);
            Player player = (Player)AccessTools.Field(typeof(GClass1667), "player_0").GetValue(MovementContext);

            if (player.IsYourPlayer)
            {
                float fpsFactor = 144f / (1f / Time.unscaledDeltaTime);

                if (Plugin.ShotCount > Plugin.PrevShotCount)
                {
                    hasReset = false;
                    timer = 0f;

                    FirearmController fc = player.HandsController as FirearmController;
                    float shotCountFactor = Mathf.Min(Plugin.ShotCount * 0.4f, 1.75f);
                    float angle = ((90f - Plugin.RecoilAngle) / 50f);
                    float dispersion = Mathf.Max(Plugin.TotalDispersion * 2.5f * Plugin.RecoilDispersionFactor.Value * shotCountFactor * fpsFactor, 0f);
                    float dispersionSpeed = Math.Max(Time.time * Plugin.RecoilDispersionSpeed.Value, 0.1f);

                    float xRotation = 0f;
                    float yRotation = 0f;

                    //S pattern
                    if (!Plugin.IsVector)
                    {
                        xRotation = Mathf.Lerp(-dispersion, dispersion, Mathf.PingPong(dispersionSpeed, 1f)) + angle;
                        yRotation = Mathf.Min(-Plugin.TotalVRecoil * Plugin.RecoilClimbFactor.Value * shotCountFactor * fpsFactor, 0f);
                    }
                    else 
                    {
                        //spiral + pingpong, would work well as vector recoil
                        spiralTime += Time.deltaTime * 20f;
                        float recoilAmount = Plugin.TotalVRecoil * Plugin.RecoilClimbFactor.Value * shotCountFactor * fpsFactor ;
                        yRotation = Mathf.Lerp(-recoilAmount, recoilAmount, Mathf.PingPong(Time.time * 4f, 1f));
                        xRotation = Mathf.Sin(spiralTime * 10f) * 1f;
                    }

                    //Spiral/circular, could modify x axis with ping pong or something to make it more random or simply use random.range
                    /*              spiralTime += Time.deltaTime * 20f;
                                  float xRotaion = Mathf.Sin(spiralTime * 10f) * 1f;
                                  float yRotation = Mathf.Cos(spiralTime * 10f) * 1f;*/


                    targetRotation = MovementContext.Rotation + new Vector2(xRotation, yRotation);

                    if ((Plugin.ResetVertical.Value && (MovementContext.Rotation.y > recordedRotation.y + 1f || deltaRotation.y <= -1f)) || (Plugin.ResetHorizontal.Value && (MovementContext.Rotation.x > recordedRotation.x + 1f || deltaRotation.x <= -1f)))
                    {
                        recordedRotation = MovementContext.Rotation;
                    }
                }
                else if (!hasReset && !Plugin.IsFiring)
                {
                    float resetSpeed = Plugin.TotalConvergence * Plugin.ResetSpeed.Value;

                    bool xIsBelowThreshold = Mathf.Abs(deltaRotation.x) <= Plugin.ResetSensitivity.Value;
                    bool yIsBelowThreshold = Mathf.Abs(deltaRotation.y) <= Plugin.ResetSensitivity.Value;

                    if (Plugin.ResetVertical.Value && Plugin.ResetHorizontal.Value && xIsBelowThreshold && yIsBelowThreshold)
                    {
                        MovementContext.Rotation = Vector2.Lerp(MovementContext.Rotation, new Vector2(recordedRotation.x, recordedRotation.y), resetSpeed);
                    }
                    else if (Plugin.ResetHorizontal.Value && xIsBelowThreshold)
                    {
                        MovementContext.Rotation = Vector2.Lerp(MovementContext.Rotation, new Vector2(recordedRotation.x, MovementContext.Rotation.y), resetSpeed);
                    }
                    else if (Plugin.ResetVertical.Value && yIsBelowThreshold)
                    {
                        MovementContext.Rotation = Vector2.Lerp(MovementContext.Rotation, new Vector2(MovementContext.Rotation.x, recordedRotation.y), resetSpeed);
                    }
                    else
                    {
                        recordedRotation = MovementContext.Rotation;
                    }

                    resetTimer(new Vector2(MovementContext.Rotation.x, recordedRotation.y), MovementContext.Rotation);
                }
                else if (!Plugin.IsFiring)
                {
                    recordedRotation = MovementContext.Rotation;
                }
                if (Plugin.IsFiring)
                {
                    MovementContext.Rotation = Vector2.Lerp(MovementContext.Rotation, targetRotation, Plugin.RecoilSmoothness.Value);
                }
            }
            return true;
        }
    }

    public class PlayerLateUpdatePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethod("LateUpdate", BindingFlags.Instance | BindingFlags.NonPublic);
        }

    
        [PatchPostfix]
        private static void PatchPostfix(Player __instance)
        {
            if (Utils.CheckIsReady() && __instance.IsYourPlayer)
            {
                float mountingSwayBonus = Plugin.IsMounting ? Plugin.MountingSwayBonus : Plugin.BracingSwayBonus;
                float mountingRecoilBonus = Plugin.IsMounting ? Plugin.MountingRecoilBonus : Plugin.BracingRecoilBonus;
                bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

                __instance.ProceduralWeaponAnimation.CrankRecoil = Plugin.EnableCrank.Value;
                __instance.ProceduralWeaponAnimation.Shootingg.Intensity = Plugin.RecoilIntensity.Value * mountingRecoilBonus;

                if (Plugin.IsFiring && isMoving)
                {
                    float swayIntensity = Plugin.SwayIntensity.Value * mountingSwayBonus * 0.01f;
                    __instance.ProceduralWeaponAnimation.Breath.Intensity = swayIntensity * Plugin.BreathIntensity;
                    __instance.ProceduralWeaponAnimation.HandsContainer.HandsRotation.InputIntensity = swayIntensity * swayIntensity;
                }
                else
                {
                    float swayIntensity = Plugin.SwayIntensity.Value * mountingSwayBonus;
                    __instance.ProceduralWeaponAnimation.Breath.Intensity = swayIntensity * Plugin.BreathIntensity;
                    __instance.ProceduralWeaponAnimation.HandsContainer.HandsRotation.InputIntensity = swayIntensity * swayIntensity;
                }

                if (Plugin.IsFiring)
                {
                    RecoilController.SetRecoilParams(__instance.ProceduralWeaponAnimation, __instance.HandsController.Item as Weapon, isMoving);
                }
                else if (!Plugin.CombatStancesIsPresent) 
                {
                    __instance.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Damping = 0.45f;
                }
            }
        }
    }

    public class UpdateWeaponVariablesPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EFT.Animations.ProceduralWeaponAnimation).GetMethod("UpdateWeaponVariables", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref EFT.Animations.ProceduralWeaponAnimation __instance)
        {
            PlayerInterface playerInterface = (PlayerInterface)AccessTools.Field(typeof(EFT.Animations.ProceduralWeaponAnimation), "ginterface114_0").GetValue(__instance);

            if (playerInterface != null && playerInterface.Weapon != null)
            {
                Weapon weapon = playerInterface.Weapon;
                Player player = Singleton<GameWorld>.Instance.GetAlivePlayerByProfileID(weapon.Owner.ID);
                if (player != null && player.MovementContext.CurrentState.Name != EPlayerState.Stationary && player.IsYourPlayer)
                {
                    Plugin.HandsIntensity = __instance.HandsContainer.HandsRotation.InputIntensity;
                    Plugin.BreathIntensity = __instance.Breath.Intensity;
                    float baseConvergence = weapon.Template.Convergence;
                    float classMulti = RecoilController.GetConvergenceMulti(weapon);
                    float stockMulti = __instance._shouldMoveWeaponCloser ? 0.1f : 1f;
                    float convBaseValue = baseConvergence * classMulti * stockMulti;
                    Plugin.TotalConvergence = Mathf.Min((float)Math.Round(convBaseValue * Plugin.ConvergenceMulti.Value, 2), 30f);
                    __instance.HandsContainer.Recoil.ReturnSpeed = Plugin.TotalConvergence;
                    Plugin.RecoilAngle = RecoilController.GetRecoilAngle(weapon);
                    Plugin.IsVector = weapon.TemplateId == "5fb64bc92b1b027b1f50bcf2" || weapon.TemplateId == "5fc3f2d5900b1d5091531e57";
                    Plugin.HasStock = __instance._shouldMoveWeaponCloser;

                    Logger.LogWarning(__instance._shouldMoveWeaponCloser);
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
                Vector3 separateIntensityFactors = (Vector3)intensityFactorsField.GetValue(__instance);

                float classVMulti = RecoilController.GetVRecoilMulti(weaponClass);
                float classCamMulti = RecoilController.GetCamRecoilMulti(weaponClass);
                float mountingRecoilBonus = Plugin.IsMounting ? Plugin.MountingRecoilBonus : Plugin.BracingRecoilBonus;

                float cameraRecoil = weaponClass.Template.CameraRecoil * Plugin.CamMulti.Value * str * classCamMulti;
                Plugin.TotalCameraRecoil = cameraRecoil * mountingRecoilBonus;

                __instance.RecoilRadian = __instance.RecoilDegree * 0.017453292f;

                __instance.ShotVals[3].Intensity = cameraRecoil;
                __instance.ShotVals[4].Intensity = -cameraRecoil;

                float recoilRadian = Random.Range(__instance.RecoilRadian.x, __instance.RecoilRadian.y * Plugin.DispMulti.Value);
                float vertRecoil = Random.Range(__instance.RecoilStrengthXy.x, __instance.RecoilStrengthXy.y) * str * Plugin.VertMulti.Value * classVMulti;
                float hRecoil = Mathf.Min(25f ,Random.Range(__instance.RecoilStrengthZ.x, __instance.RecoilStrengthZ.y) * str * Plugin.HorzMulti.Value);
                __instance.RecoilDirection = new Vector3(-Mathf.Sin(recoilRadian) * vertRecoil * separateIntensityFactors.x, Mathf.Cos(recoilRadian) * vertRecoil * separateIntensityFactors.y, hRecoil * separateIntensityFactors.z) * __instance.Intensity;
                
                Plugin.TotalHRecoil = hRecoil * mountingRecoilBonus;
                Plugin.TotalVRecoil = vertRecoil * mountingRecoilBonus;
                Plugin.TotalDispersion = weaponClass.Template.RecolDispersion * mountingRecoilBonus;

                Vector2 heatDirection = (iWeapon != null) ? iWeapon.MalfState.OverheatBarrelMoveDir : Vector2.zero;
                float heatFactor = (iWeapon != null) ? iWeapon.MalfState.OverheatBarrelMoveMult : 0f;
                float totalRecoilFactor = (__instance.RecoilRadian.x + __instance.RecoilRadian.y) / 2f * ((__instance.RecoilStrengthXy.x + __instance.RecoilStrengthXy.y) / 2f) * heatFactor;
                __instance.RecoilDirection.x = __instance.RecoilDirection.x + heatDirection.x * totalRecoilFactor;
                __instance.RecoilDirection.y = __instance.RecoilDirection.y + heatDirection.y * totalRecoilFactor;
                ShotEffector.ShotVal[] shotVals = __instance.ShotVals;
                
                for (int i = 0; i < shotVals.Length; i++)
                {
                    shotVals[i].Process(__instance.RecoilDirection);
                }

                Plugin.Timer = 0f;
                Plugin.IsFiring = true;
                Plugin.ShotCount++;

                return false;
            }
            return true;
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
                    bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
                    RecoilController.SetRecoilParams(__instance, weapon, isMoving);
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

