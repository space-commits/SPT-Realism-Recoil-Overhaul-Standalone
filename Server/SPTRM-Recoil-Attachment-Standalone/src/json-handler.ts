import { ITemplateItem } from "@spt-aki/models/eft/common/tables/ITemplateItem";
import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "@spt-aki/models/spt/utils/ILogger";
import { ConfigChecker } from "./helper";

const modConfig = require("../config/config.json");
const weapPath = modConfig.weap_preset;
const attPath = modConfig.att_preset;

const armorComponentsTemplates = require("../db/templates/armor/armorComponentsTemplates.json");
const armorChestrigTemplates = require("../db/templates/armor/armorChestrigTemplates.json");
const helmetTemplates = require("../db/templates/armor/helmetTemplates.json");
const armorVestsTemplates = require("../db/templates/armor/armorVestsTemplates.json");
const armorMasksTemplates = require("../db/templates/armor/armorMasksTemplates.json");


const MuzzleDeviceTemplates = require("../db/templates/attatchments/"+`${attPath}`+"/MuzzleDeviceTemplates.json");
const BarrelTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/BarrelTemplates.json");
const MountTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/MountTemplates.json");
const ReceiverTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/ReceiverTemplates.json");
const StockTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/StockTemplates.json");
const ChargingHandleTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/ChargingHandleTemplates.json");
const ScopeTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/ScopeTemplates.json");
const IronSightTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/IronSightTemplates.json");
const MagazineTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/MagazineTemplates.json");
const AuxiliaryModTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/AuxiliaryModTemplates.json");
const ForegripTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/ForegripTemplates.json");
const PistolGripTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/PistolGripTemplates.json");
const GasblockTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/GasblockTemplates.json");
const HandguardTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/HandguardTemplates.json");
const FlashlightLaserTemplates = require("../db/templates/attatchments/" + `${attPath}` + "/FlashlightLaserTemplates.json");

const AssaultRifleTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/AssaultRifleTemplates.json");
const AssaultCarbineTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/AssaultCarbineTemplates.json");
const MachinegunTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/MachinegunTemplates.json");
const MarksmanRifleTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/MarksmanRifleTemplates.json");
const PistolTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/PistolTemplates.json");
const ShotgunTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/ShotgunTemplates.json");
const SMGTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/SMGTemplates.json");
const SniperRifleTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/SniperRifleTemplates.json");
const SpecialWeaponTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/SpecialWeaponTemplates.json");
const GrenadeLauncherTemplates = require("../db/templates/weapons/" + `${weapPath}` + "/GrenadeLauncherTemplates.json");


export class JsonHandler {
    constructor(private tables: IDatabaseTables, private logger: ILogger) { }

    itemDB = this.tables.templates.items;

    public pushModsToServer() {

        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._props.ToolModdable == true || serverItem._props.ToolModdable == false) {
                this.callHelper(MuzzleDeviceTemplates, serverItem, this.modPusherHelper);
                this.callHelper(BarrelTemplates, serverItem, this.modPusherHelper);
                this.callHelper(MountTemplates, serverItem, this.modPusherHelper);
                this.callHelper(ReceiverTemplates, serverItem, this.modPusherHelper);
                this.callHelper(StockTemplates, serverItem, this.modPusherHelper);
                this.callHelper(ChargingHandleTemplates, serverItem, this.modPusherHelper);
                this.callHelper(ScopeTemplates, serverItem, this.modPusherHelper);
                this.callHelper(IronSightTemplates, serverItem, this.modPusherHelper);
                this.callHelper(MagazineTemplates, serverItem, this.modPusherHelper);
                this.callHelper(AuxiliaryModTemplates, serverItem, this.modPusherHelper);
                this.callHelper(ForegripTemplates, serverItem, this.modPusherHelper);
                this.callHelper(PistolGripTemplates, serverItem, this.modPusherHelper);
                this.callHelper(GasblockTemplates, serverItem, this.modPusherHelper);
                this.callHelper(HandguardTemplates, serverItem, this.modPusherHelper);
                this.callHelper(FlashlightLaserTemplates, serverItem, this.modPusherHelper);
            }
        }
    }

    public pushWeaponsToServer() {

        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._props.RecolDispersion) {
                this.callHelper(AssaultRifleTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(AssaultCarbineTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(MachinegunTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(MarksmanRifleTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(PistolTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(ShotgunTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(SMGTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(SniperRifleTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(SpecialWeaponTemplates, serverItem, this.weapPusherHelper);
                this.callHelper(GrenadeLauncherTemplates, serverItem, this.weapPusherHelper);
            }
        }
    }

    public pushArmorToServer() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._props?.armorClass !== null && serverItem._props?.armorClass > 0) {
                this.callHelper(armorChestrigTemplates, serverItem, this.armorPusher);
                this.callHelper(armorComponentsTemplates, serverItem, this.armorPusher);
                this.callHelper(helmetTemplates, serverItem, this.armorPusher);
                this.callHelper(armorVestsTemplates, serverItem, this.armorPusher);
                this.callHelper(armorMasksTemplates, serverItem, this.armorPusher);
            }
        }
    }

    private callHelper(template: any, serverItem: ITemplateItem, funPusherHelper: Function) {

        for (let i in template) {
            let fileItem = template[i];
            funPusherHelper(serverItem, fileItem);
        }
    }

    private armorPusher(serverItem: any, fileItem: any) {

        if (serverItem._id === fileItem.ItemID) {

            var serverConfItems = serverItem._props.ConflictingItems;
            var armorPropertyValues = ["SPTRM", fileItem?.AllowADS?.toString() || "true", fileItem?.ArmorClass || "0"];

            var combinedArr = armorPropertyValues.concat(serverConfItems)
            serverItem._props.ConflictingItems = combinedArr;
        }
    }

    private modPusherHelper(serverItem: any, fileItem: any) {


        if (ConfigChecker.dllIsPresent == true) {
            if (serverItem._id === fileItem.ItemID) {
                serverItem._props.Ergonomics = fileItem.Ergonomics;
                serverItem._props.Accuracy = fileItem.Accuracy;
                serverItem._props.CenterOfImpact = fileItem.CenterOfImpact;
                serverItem._props.HeatFactor = fileItem.HeatFactor;
                serverItem._props.CoolFactor = fileItem.CoolFactor;
                serverItem._props.MalfunctionChance = fileItem.MagMalfunctionChance;
                serverItem._props.LoadUnloadModifier = fileItem.LoadUnloadModifier;
                serverItem._props.CheckTimeModifier = fileItem.CheckTimeModifier;
                serverItem._props.DurabilityBurnModificator = fileItem.DurabilityBurnModificator;
                serverItem._props.HasShoulderContact = fileItem.HasShoulderContact;
                serverItem._props.BlocksFolding = fileItem.BlocksFolding;
                serverItem._props.Velocity = fileItem.Velocity;
                serverItem._props.Weight = fileItem.Weight;
                serverItem._props.ShotgunDispersion = fileItem.ShotgunDispersion;

                var serverConfItems = serverItem._props.ConflictingItems;

                var modPropertyValues = ["SPTRM", fileItem?.ModType?.toString() || "undefined", fileItem?.VerticalRecoil?.toString() || "0", fileItem?.HorizontalRecoil?.toString() || "0", fileItem?.Dispersion?.toString() || "0", fileItem?.CameraRecoil?.toString() || "0",
                    fileItem?.AutoROF?.toString() || "0", fileItem?.SemiROF?.toString() || "0", fileItem?.ModMalfunctionChance?.toString() || "0", fileItem?.ReloadSpeed?.toString() || "0", fileItem?.AimSpeed?.toString() || "0", fileItem?.ChamberSpeed?.toString() || "0",
                    fileItem?.Length?.toString() || "0", fileItem?.CanCylceSubs?.toString() || "false", fileItem?.RecoilAngle?.toString() || "0", fileItem?.StockAllowADS?.toString() || "false", fileItem?.FixSpeed?.toString() || "0", fileItem?.ModShotDispersion?.toString() || "0",
                    fileItem?.ModShotDispersion?.toString() || "0"];

                var combinedArr = modPropertyValues.concat(serverConfItems)
                serverItem._props.ConflictingItems = combinedArr;
            }
        }
    }

    private weapPusherHelper(serverItem: any, fileItem: any) {


        if (serverItem._id === fileItem.ItemID) {

            if (ConfigChecker.dllIsPresent == true) {
                serverItem._props.Ergonomics = fileItem.Ergonomics;
                serverItem._props.RecoilForceUp = fileItem.VerticalRecoil;
                serverItem._props.CenterOfImpact = fileItem.CenterOfImpact;
                serverItem._props.HeatFactor = fileItem.HeatFactor;
                serverItem._props.RecoilForceBack = fileItem.HorizontalRecoil;
                serverItem._props.RecolDispersion = fileItem.Dispersion;
                serverItem._props.CameraRecoil = fileItem.CameraRecoil;
                serverItem._props.CameraSnap = fileItem.CameraSnap;
                serverItem._props.Convergence = fileItem.Convergence;
                serverItem._props.DurabilityBurnRatio = fileItem.DurabilityBurnRatio;
                serverItem._props.RecoilAngle = fileItem.RecoilAngle;
                serverItem._props.AllowOverheat = fileItem.AllowOverheat;
                serverItem._props.HipAccuracyRestorationDelay = fileItem.HipAccuracyRestorationDelay;
                serverItem._props.HipAccuracyRestorationSpeed = fileItem.HipAccuracyRestorationSpeed;
                serverItem._props.HipInnaccuracyGain = fileItem.HipInnaccuracyGain;
                serverItem._props.ShotgunDispersion = fileItem.ShotgunDispersion;
                serverItem._props.Velocity = fileItem.Velocity;
                serverItem._props.Weight = fileItem.Weight;
                serverItem._props.bFirerate = fileItem.AutoROF;
                serverItem._props.SingleFireRate = fileItem.SemiROF;
                serverItem._props.DoubleActionAccuracyPenalty = fileItem.DoubleActionAccuracyPenalty;


                if (fileItem.weapFireType !== undefined) {
                    serverItem._props.weapFireType = fileItem.weapFireType;
                }

                var serverConfItems = serverItem._props.ConflictingItems;
                var weapPropertyValues = ["SPTRM", fileItem?.WeapType?.toString() || "undefined", fileItem?.BaseTorque?.toString() || "0", fileItem?.HasShoulderContact?.toString() || "false", "unused", fileItem?.OperationType?.toString() || "undefined", fileItem?.WeapAccuracy?.toString() || "0",
                    fileItem?.RecoilDamping?.toString() || "0.7", fileItem?.RecoilHandDamping?.toString() || "0.65", fileItem?.WeaponAllowADS?.toString() || "false"];


                var combinedArr = weapPropertyValues.concat(serverConfItems)
                serverItem._props.ConflictingItems = combinedArr;
            }
        }
    }

}