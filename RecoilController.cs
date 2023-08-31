using Comfort.Common;
using EFT.Animations;
using EFT.InventoryLogic;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RecoilStandalone
{


    public class RecoilController
    {
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
            float convBaseValue = weap.WeapClass == "pistol" && weap.Template.Convergence > 4 ? weap.Template.Convergence * 0.5f : weap.Template.Convergence;
            pwa.HandsContainer.Recoil.ReturnSpeed = Mathf.Min((float)Math.Round(convBaseValue * Plugin.ConvergenceMulti.Value, 2), 30f);
            Plugin.TotalConvergence = pwa.HandsContainer.Recoil.ReturnSpeed;
            Plugin.RecoilAngle = weap.Template.RecoilAngle;
        }
    }
}
