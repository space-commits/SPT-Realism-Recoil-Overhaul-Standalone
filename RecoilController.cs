using Comfort.Common;
using EFT.Animations;
using EFT.InventoryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RecoilStandalone
{


    public class RecoilController
    {
        public static float GetConvergenceMulti(Weapon weap) 
        {
           switch (weap.WeapClass)
            {
                case "pistol":
                    if (weap.Template.Convergence >= 4)
                    {
                        return 0.5f;
                    }
                    return 1f;
                case "shotgun":
                    return 0.25f;
                case "sniperRifle":
                    return 0.25f;
                case "marksmanRifle":
                    if (!weap.WeapFireType.Contains(Weapon.EFireMode.fullauto)) 
                    {
                        return 0.5f;
                    }
                    return 1f;
                default:
                    return 1;
            }
        }

        public static float GetVRecoilMulti(Weapon weap)
        {
            switch (weap.WeapClass)
            {
                case "pistol":
                    return 0.5f;
                case "shotgun":
                    return 2f;
                case "sniperRifle":
                    return 1.5f;
                case "marksmanRifle":
                    if (!weap.WeapFireType.Contains(Weapon.EFireMode.fullauto))
                    {
                        return 1.15f;
                    }
                    return 1f;
                default:
                    return 1;
            }
        }

        public static float GetCamRecoilMulti(Weapon weap)
        {
            switch (weap.WeapClass)
            {
                case "shotgun":
                    return 1f;
                case "sniperRifle":
                    return 1f;
                case "marksmanRifle":
                    if (!weap.WeapFireType.Contains(Weapon.EFireMode.fullauto))
                    {
                        return 1f;
                    }
                    return 1f;
                default:
                    return 1;
            }
        }

        public static void DoCantedRecoil(ref Vector3 targetRecoil, ref Vector3 currentRecoil, ref Quaternion weapRotation)
        {
            if (Plugin.IsFiring)
            {
                float recoilAmount = Plugin.TotalHRecoil / 35f;
                float recoilSpeed = Plugin.TotalConvergence * 0.6f;
                float totalRecoil = Mathf.Lerp(-recoilAmount, recoilAmount, Mathf.PingPong(Time.time * recoilSpeed, 1.0f));
                targetRecoil = new Vector3(0f, totalRecoil, 0f);
            }
            else
            {
                targetRecoil = Vector3.zero;
            }

            currentRecoil = Vector3.Lerp(currentRecoil, targetRecoil, 1f);
            Quaternion recoilQ = Quaternion.Euler(currentRecoil);
            weapRotation *= recoilQ;
        }

        public static void SetRecoilParams(ProceduralWeaponAnimation pwa, Weapon weap) 
        {
            pwa.HandsContainer.Recoil.Damping = (float)Math.Round(Plugin.RecoilDamping.Value, 3);
            pwa.HandsContainer.HandsPosition.Damping = (float)Math.Round(Plugin.HandsDamping.Value, 3);
            pwa.HandsContainer.Recoil.ReturnSpeed = Plugin.TotalConvergence;
        }
    }
}
