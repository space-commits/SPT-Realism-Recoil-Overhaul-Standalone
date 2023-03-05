using Aki.Reflection.Patching;
using EFT.InventoryLogic;
using HarmonyLib;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using static RecoilStandalone.Attributes;
using UnityEngine;
using EFT;

namespace RecoilStandalone
{
    public class WeaponConstructorPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Weapon).GetConstructor(new Type[] { typeof(string), typeof(WeaponTemplate) });
        }


        [PatchPostfix]
        private static void PatchPostfix(Weapon __instance, string id, WeaponTemplate template)
        {

            if (Plugin.showDispersion.Value == true)
            {
                List<ItemAttributeClass> dispersionAttList = __instance.Attributes;
                ItemAttributeClass dispersionAtt = new ItemAttributeClass((EItemAttributeId)ENewItemAttributeId.Dispersion);
                dispersionAtt.Name = ENewItemAttributeId.Dispersion.GetName();
                dispersionAtt.LessIsGood = true;
                dispersionAtt.Base = () => __instance.Template.RecolDispersion;
                dispersionAtt.StringValue = () => __instance.Template.RecolDispersion.ToString();
                dispersionAtt.DisplayType = () => EItemAttributeDisplayType.Compact;
                dispersionAttList.Add(dispersionAtt);
            }

            if (Plugin.showCamRecoil.Value == true)
            {
                List<ItemAttributeClass> camRecoilAttList = __instance.Attributes;
                ItemAttributeClass camRecoilAtt = new ItemAttributeClass((EItemAttributeId)ENewItemAttributeId.CameraRecoil);
                camRecoilAtt.Name = ENewItemAttributeId.CameraRecoil.GetName();
                camRecoilAtt.LessIsGood = true;
                camRecoilAtt.Base = () => __instance.Template.CameraRecoil;
                camRecoilAtt.StringValue = () => __instance.Template.CameraRecoil.ToString();
                camRecoilAtt.DisplayType = () => EItemAttributeDisplayType.Compact;
                camRecoilAttList.Add(camRecoilAtt);
            }
        }
    }
}
