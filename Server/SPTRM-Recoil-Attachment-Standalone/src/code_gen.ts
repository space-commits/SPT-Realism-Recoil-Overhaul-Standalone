import { ITemplateItem } from "@spt-aki/models/eft/common/tables/ITemplateItem";
import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "@spt-aki/models/spt/utils/ILogger";
import { Arrays } from "./arrays";
import { Helper } from "./helper";
import { ParentClasses } from "./enums";


// const magazineJSON = require("../db/bots/loadouts/common/magazines.json");

const armorComponentsTemplates = require("../db/templates/armor/armorComponentsTemplates.json");
const armorChestrigTemplates = require("../db/templates/armor/armorChestrigTemplates.json");
const helmetTemplates = require("../db/templates/armor/helmetTemplates.json");
const armorVestsTemplates = require("../db/templates/armor/armorVestsTemplates.json");


const ammoTemplates = require("../db/templates/ammo/ammoTemplates.json");

const MuzzleDeviceTemplates = require("../db/templates/attatchments/MuzzleDeviceTemplates.json");
const BarrelTemplates = require("../db/templates/attatchments/BarrelTemplates.json");
const MountTemplates = require("../db/templates/attatchments/MountTemplates.json");
const ReceiverTemplates = require("../db/templates/attatchments/ReceiverTemplates.json");
const StockTemplates = require("../db/templates/attatchments/StockTemplates.json");
const ChargingHandleTemplates = require("../db/templates/attatchments/ChargingHandleTemplates.json");
const ScopeTemplates = require("../db/templates/attatchments/ScopeTemplates.json");
const IronSightTemplates = require("../db/templates/attatchments/IronSightTemplates.json");
const MagazineTemplates = require("../db/templates/attatchments/MagazineTemplates.json");
const AuxiliaryModTemplates = require("../db/templates/attatchments/AuxiliaryModTemplates.json");
const ForegripTemplates = require("../db/templates/attatchments/ForegripTemplates.json");
const PistolGripTemplates = require("../db/templates/attatchments/PistolGripTemplates.json");
const GasblockTemplates = require("../db/templates/attatchments/GasblockTemplates.json");
const HandguardTemplates = require("../db/templates/attatchments/HandguardTemplates.json");
const FlashlightLaserTemplates = require("../db/templates/attatchments/FlashlightLaserTemplates.json");

const AssaultRifleTemplates = require("../db/templates/weapons/AssaultRifleTemplates.json");
const AssaultCarbineTemplates = require("../db/templates/weapons/AssaultCarbineTemplates.json");
const MachinegunTemplates = require("../db/templates/weapons/MachinegunTemplates.json");
const MarksmanRifleTemplates = require("../db/templates/weapons/MarksmanRifleTemplates.json");
const PistolTemplates = require("../db/templates/weapons/PistolTemplates.json");
const ShotgunTemplates = require("../db/templates/weapons/ShotgunTemplates.json");
const SMGTemplates = require("../db/templates/weapons/SMGTemplates.json");
const SniperRifleTemplates = require("../db/templates/weapons/SniperRifleTemplates.json");
const SpecialWeaponTemplates = require("../db/templates/weapons/SpecialWeaponTemplates.json");
const GrenadeLauncherTemplates = require("../db/templates/weapons/GrenadeLauncherTemplates.json");



export class CodeGen {

    constructor(private logger: ILogger, private tables: IDatabaseTables, private modConf, private helper: Helper, private arrays: Arrays) { }

    globalDB = this.tables.globals.config;
    itemDB = this.tables.templates.items;

    public ammoTemplatesCodeGen() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._parent === ParentClasses.AMMO || ParentClasses.AMMO_BOX) {
                this.itemWriteToFile(ammoTemplates, "ammoTemplates", i, serverItem, "ammo", this.assignJSONToAmmo);
            }
        }
    }

    public armorTemplatesCodeGen() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._parent === ParentClasses.CHESTRIG && serverItem._props.armorClass > 0) {
                this.itemWriteToFile(armorChestrigTemplates, "armorChestrigTemplates", i, serverItem, "armor", this.assignJSONToArmor);
            }
            if (serverItem._parent === ParentClasses.ARMOREDEQUIPMENT && serverItem._props.armorClass > 0) {
                this.itemWriteToFile(armorComponentsTemplates, "armorComponentsTemplates", i, serverItem, "armor", this.assignJSONToArmor);
            }
            if (serverItem._parent === ParentClasses.HEADWEAR && serverItem._props.armorClass > 0) {
                this.itemWriteToFile(helmetTemplates, "helmetTemplates", i, serverItem, "armor", this.assignJSONToArmor);
            }
            if (serverItem._parent === ParentClasses.ARMORVEST && serverItem._props.armorClass > 0) {
                this.itemWriteToFile(armorVestsTemplates, "armorVestsTemplates", i, serverItem, "armor", this.assignJSONToArmor);
            }
        }
    }

    public weapTemplatesCodeGen() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._props.RecolDispersion) {
                if (serverItem._props.weapClass === "assaultCarbine") {
                    this.itemWriteToFile(AssaultCarbineTemplates, "AssaultCarbineTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "assaultRifle") {
                    this.itemWriteToFile(AssaultRifleTemplates, "AssaultRifleTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "smg") {
                    this.itemWriteToFile(SMGTemplates, "SMGTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "machinegun") {
                    this.itemWriteToFile(MachinegunTemplates, "MachinegunTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "marksmanRifle") {

                    this.itemWriteToFile(MarksmanRifleTemplates, "MarksmanRifleTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "sniperRifle") {
                    this.itemWriteToFile(SniperRifleTemplates, "SniperRifleTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "pistol") {
                    this.itemWriteToFile(PistolTemplates, "PistolTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "shotgun") {
                    this.itemWriteToFile(ShotgunTemplates, "ShotgunTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "specialWeapon") {
                    this.itemWriteToFile(SpecialWeaponTemplates, "SpecialWeaponTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
                if (serverItem._props.weapClass === "grenadeLauncher") {
                    this.itemWriteToFile(GrenadeLauncherTemplates, "GrenadeLauncherTemplates", i, serverItem, "weapons", this.assignJSONToWeap)
                }
            }
        }
    }

    public attTemplatesCodeGen() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._props.ToolModdable == true || serverItem._props.ToolModdable == false) {
                for (let value in this.arrays.modTypes) {
                    if (serverItem._parent === this.arrays.modTypes[value]) {
                        if (this.arrays.modTypes[value] === "550aa4bf4bdc2dd6348b456b" ||
                            this.arrays.modTypes[value] === "550aa4dd4bdc2dc9348b4569" ||
                            this.arrays.modTypes[value] === "550aa4cd4bdc2dd8348b456c"
                        ) {
                            let id = "muzzle"
                            this.itemWriteToFile(MuzzleDeviceTemplates, "MuzzleDeviceTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "555ef6e44bdc2de9068b457e") {
                            let id = "barrel"
                            this.itemWriteToFile(BarrelTemplates, "BarrelTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818b224bdc2dde698b456f") {
                            let id = "mount"
                            this.itemWriteToFile(MountTemplates, "MountTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818a304bdc2db5418b457d") {
                            let id = "receiver"
                            this.itemWriteToFile(ReceiverTemplates, "ReceiverTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818a594bdc2db9688b456a") {
                            let id = "stock"
                            this.itemWriteToFile(StockTemplates, "StockTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818a6f4bdc2db9688b456b") {
                            let id = "charging"
                            this.itemWriteToFile(ChargingHandleTemplates, "ChargingHandleTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818acf4bdc2dde698b456b" ||
                            this.arrays.modTypes[value] === "55818ad54bdc2ddc698b4569" ||
                            this.arrays.modTypes[value] === "55818add4bdc2d5b648b456f" ||
                            this.arrays.modTypes[value] === "55818ae44bdc2dde698b456c" ||
                            this.arrays.modTypes[value] === "55818aeb4bdc2ddc698b456a"
                        ) {
                            let id = "scope"
                            this.itemWriteToFile(ScopeTemplates, "ScopeTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818ac54bdc2d5b648b456e") {
                            let id = "irons"
                            this.itemWriteToFile(IronSightTemplates, "IronSightTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "5448bc234bdc2d3c308b4569" ||
                            this.arrays.modTypes[value] === "610720f290b75a49ff2e5e25" ||
                            this.arrays.modTypes[value] === "627a137bf21bc425b06ab944"
                        ) {
                            let id = "magazine"
                            this.itemWriteToFile(MagazineTemplates, "MagazineTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "5a74651486f7744e73386dd1" ||
                            this.arrays.modTypes[value] === "55818afb4bdc2dde698b456d"
                        ) {
                            let id = "aux"
                            this.itemWriteToFile(AuxiliaryModTemplates, "AuxiliaryModTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818af64bdc2d5b648b4570") {
                            let id = "foregrip"
                            this.itemWriteToFile(ForegripTemplates, "ForegripTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818a684bdc2ddd698b456d") {
                            let id = "pistolgrip"
                            this.itemWriteToFile(PistolGripTemplates, "PistolGripTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "56ea9461d2720b67698b456f") {
                            let id = "gasblock"
                            this.itemWriteToFile(GasblockTemplates, "GasblockTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818a104bdc2db9688b4569") {
                            let id = "handguard"
                            this.itemWriteToFile(HandguardTemplates, "HandguardTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                        if (this.arrays.modTypes[value] === "55818b084bdc2d5b648b4571" ||
                            this.arrays.modTypes[value] === "55818b164bdc2ddc698b456c"
                        ) {
                            let id = "flashlight"
                            this.itemWriteToFile(FlashlightLaserTemplates, "FlashlightLaserTemplates", i, serverItem, "attatchments", this.assignJSONToMod, id);
                        }
                    }
                }
            }
        }
    }

    private itemWriteToFile(filePathObj: object, fileStr: string, index: string, serverItem: ITemplateItem, folderStr: string, funJsonAssign: Function, id?: string) {
        let fileItem = filePathObj[index];

        filePathObj[index] = funJsonAssign(serverItem, fileItem, id);

        this.helper.saveToJSONFile(filePathObj, `db/templates/${folderStr}/${fileStr}.json`);
    }

    private assignJSONToAmmo(serverItem: ITemplateItem, fileItem: any) {

        if (fileItem) {
            fileItem.loyaltyLevel = 2;
            fileItem;
            return fileItem;
        }

        let ItemID = serverItem._id;
        let Name = serverItem._name;
        let LoyaltyLevel = 2;

        let item = {
            ItemID,
            Name,
            LoyaltyLevel
        };

        return item;
    }


    private assignJSONToArmor(serverItem: ITemplateItem, fileItem: any) {

        if (fileItem) {
            fileItem;
            return fileItem;
        }

        let ItemID = serverItem._id;
        let Name = serverItem._name;
        let AllowADS = true;
        let LoyaltyLevel = 2;
        let ArmorClass = "";

        let item = {
            ItemID,
            Name,
            AllowADS,
            LoyaltyLevel,
            ArmorClass
        };

        return item;
    }

    private assignJSONToWeap(serverItem: ITemplateItem, fileItem: any) {

        // new items properties can be added, and  property values can be replaced, by delcaring them in this if statement
        if (fileItem) {
            // fileItem.HeatFactor = serverItem._props.HeatFactor; You need to give it a value. If you set it to the server item's propety value, the new property will only appear if the server mod has that property
            fileItem;
            return fileItem;
        }

        let ItemID = serverItem._id;
        let Name = serverItem._name;
        let WeapType = "";
        let OperationType = "";
        let WeapAccuracy = 0;
        let BaseTorque = 0;
        let RecoilDamping = 75;
        let RecoilHandDamping = 60;
        let HasShoulderContact = false;
        let WeaponAllowADS = false;
        let Ergonomics = serverItem._props.Ergonomics
        let VerticalRecoil = serverItem._props.RecoilForceUp;
        let HorizontalRecoil = serverItem._props.RecoilForceBack;
        let Dispersion = serverItem._props.RecolDispersion;
        let CameraRecoil = serverItem._props.CameraRecoil;
        let CameraSnap = serverItem._props.CameraSnap;
        let Convergence = serverItem._props.Convergence;
        let RecoilAngle = serverItem._props.RecoilAngle;
        let DurabilityBurnRatio = serverItem._props.DurabilityBurnRatio;
        let BaseMalfunctionChance = serverItem._props.BaseMalfunctionChance;
        let HeatFactorGun = serverItem._props.HeatFactorGun;
        let HeatFactorByShot = serverItem._props.HeatFactorByShot;
        let CoolFactorGun = serverItem._props.CoolFactorGun;
        let CoolFactorGunMods = serverItem._props.CoolFactorGunMods;
        let AllowOverheat = serverItem._props.AllowOverheat;
        let CenterOfImpact = serverItem._props.CenterOfImpact;
        let HipAccuracyRestorationDelay = serverItem._props.HipAccuracyRestorationDelay;
        let HipAccuracyRestorationSpeed = serverItem._props.HipAccuracyRestorationSpeed;
        let HipInnaccuracyGain = serverItem._props.HipInnaccuracyGain;
        let ShotgunDispersion = serverItem._props.ShotgunDispersion;
        let Velocity = serverItem._props.Velocity;
        let Weight = serverItem._props.Weight;
        let AutoROF = serverItem._props.bFirerate;
        let SemiROF = serverItem._props.SingleFireRate;
        let loyaltyLevel = 2;

        let item = {
            ItemID,
            Name,
            WeapType,
            OperationType,
            WeapAccuracy,
            BaseTorque,
            RecoilDamping,
            RecoilHandDamping,
            HasShoulderContact,
            WeaponAllowADS,
            Ergonomics,
            VerticalRecoil,
            HorizontalRecoil,
            Dispersion,
            CameraRecoil,
            CameraSnap,
            Convergence,
            RecoilAngle,
            DurabilityBurnRatio,
            BaseMalfunctionChance,
            HeatFactorGun,
            HeatFactorByShot,
            CoolFactorGun,
            CoolFactorGunMods,
            AllowOverheat,
            CenterOfImpact,
            HipAccuracyRestorationDelay,
            HipAccuracyRestorationSpeed,
            HipInnaccuracyGain,
            ShotgunDispersion,
            Velocity,
            Weight,
            AutoROF,
            SemiROF,
            loyaltyLevel
        };
        return item;

    }



    private assignJSONToMod(serverItem: ITemplateItem, fileItem: any, ID: string) {

        //new items properties can be added, and  property values can be replaced, by delcaring them in this if statement
        if (fileItem) {
            // fileItem.HeatFactor = serverItem._props.HeatFactor; You need to give it a value. If you set it to the server item's propety value, the new property will only appear if the server mod has that property
            //    if(serverItem._props?.Recoil !== undefined){
            //     fileItem.VerticalRecoil = serverItem._props.Recoil;
            //     fileItem.HorizontalRecoil = serverItem._props.Recoil;
            //    }
            return fileItem;
        }

        let ItemID = serverItem._id;
        let Name = serverItem._name;
        let ModType = "";
        let VerticalRecoil = serverItem._props.Recoil;
        let HorizontalRecoil = serverItem._props.Recoil;
        let Dispersion = 0;
        let CameraRecoil = 0;
        let AutoROF = 0;
        let SemiROF = 0;
        let ModMalfunctionChance = 0;
        let ReloadSpeed = 0;
        let AimSpeed = 0;
        let Length = 0;
        let CanCylceSubs = false;
        let RecoilAngle = 0;
        let StockAllowADS = false;
        let FixSpeed = 0;
        let ChamberSpeed = 0;
        let ModShotDispersion = 0;
        let Ergonomics = serverItem._props.Ergonomics
        let Accuracy = serverItem._props.Accuracy
        let CenterOfImpact = serverItem._props.CenterOfImpact
        let HeatFactor = serverItem._props.HeatFactor
        let CoolFactor = serverItem._props.CoolFactor
        let MagMalfunctionChance = serverItem._props.MalfunctionChance
        let LoadUnloadModifier = serverItem._props.LoadUnloadModifier
        let CheckTimeModifier = serverItem._props.CheckTimeModifier
        let DurabilityBurnModificator = serverItem._props.DurabilityBurnModificator;
        let HasShoulderContact = serverItem._props.HasShoulderContact;
        let BlocksFolding = serverItem._props.BlocksFolding;
        let Velocity = serverItem._props.Velocity;
        let ConflictingItems = serverItem._props.ConflictingItems;
        let Weight = serverItem._props.Weight;
        let ShotgunDispersion = serverItem._props.ShotgunDispersion;

        if (ID === "muzzle") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                Dispersion,
                CameraRecoil,
                AutoROF,
                SemiROF,
                ModMalfunctionChance,
                CanCylceSubs,
                Accuracy,
                HeatFactor,
                CoolFactor,
                DurabilityBurnModificator,
                Velocity,
                RecoilAngle,
                ConflictingItems,
                Ergonomics,
                Weight,
                ModShotDispersion
            };
            return item;
        }

        if (ID === "barrel") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                AutoROF,
                SemiROF,
                ModMalfunctionChance,
                Length,
                Accuracy,
                CenterOfImpact,
                HeatFactor,
                CoolFactor,
                DurabilityBurnModificator,
                Velocity,
                ConflictingItems,
                Ergonomics,
                Weight,
                ShotgunDispersion
            };
            return item;
        }
        if (ID === "mount") {
            let item = {
                ItemID,
                Name,
                ModType,
                Ergonomics,
                Accuracy,
                ConflictingItems,
                Weight
            };
            return item;
        }
        if (ID === "receiver") {
            let item = {
                ItemID,
                Name,
                ModType,
                ModMalfunctionChance,
                Accuracy,
                HeatFactor,
                CoolFactor,
                DurabilityBurnModificator,
                ConflictingItems,
                Ergonomics,
                Weight
            };
            return item;
        }
        if (ID === "charging") {
            let item = {
                ItemID,
                Name,
                ModType,
                ReloadSpeed,
                ConflictingItems,
                FixSpeed,
                Ergonomics,
                Weight,
                ChamberSpeed
            };
            return item;
        }
        if (ID === "scope" || ID === "irons") {
            let item = {
                ItemID,
                Name,
                ModType,
                AimSpeed,
                Accuracy,
                ConflictingItems,
                Ergonomics,
                Weight
            };
            return item;
        }
        if (ID === "magazine") {
            let item = {
                ItemID,
                Name,
                ModType,
                ReloadSpeed,
                Ergonomics,
                MagMalfunctionChance,
                LoadUnloadModifier,
                CheckTimeModifier,
                ConflictingItems,
                Weight
            };
            return item;
        }
        if (ID === "aux") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                AutoROF,
                SemiROF,
                ModMalfunctionChance,
                AimSpeed,
                ReloadSpeed,
                Ergonomics,
                Accuracy,
                ConflictingItems,
                FixSpeed,
                HeatFactor,
                CoolFactor,
                DurabilityBurnModificator,
                Weight

            };
            return item;
        }
        if (ID === "foregrip" || ID === "pistolgrip") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                Dispersion,
                AimSpeed,
                Ergonomics,
                Accuracy,
                ConflictingItems,
                Weight
            };
            return item;
        }
        if (ID === "stock") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                Dispersion,
                AutoROF,
                SemiROF,
                ModMalfunctionChance,
                CameraRecoil,
                AimSpeed,
                Ergonomics,
                Accuracy,
                HasShoulderContact,
                BlocksFolding,
                StockAllowADS,
                ConflictingItems,
                Weight
            };
            return item;
        }
        if (ID === "gasblock") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                Dispersion,
                AutoROF,
                SemiROF,
                ModMalfunctionChance,
                CanCylceSubs,
                Accuracy,
                HeatFactor,
                CoolFactor,
                DurabilityBurnModificator,
                ConflictingItems,
                Ergonomics,
                Weight
            };
            return item;
        }
        if (ID === "handguard") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                Dispersion,
                AimSpeed,
                ChamberSpeed,
                Length,
                Ergonomics,
                Accuracy,
                HeatFactor,
                CoolFactor,
                ConflictingItems,
                Weight
            };
            return item;
        }
        if (ID === "flashlight") {
            let item = {
                ItemID,
                Name,
                ModType,
                ConflictingItems,
                Ergonomics,
                Weight
            };
            return item;
        }
        if (ID === "unknown") {
            let item = {
                ItemID,
                Name,
                ModType,
                VerticalRecoil,
                HorizontalRecoil,
                Dispersion,
                CameraRecoil,
                AutoROF,
                SemiROF,
                ModMalfunctionChance,
                ReloadSpeed,
                AimSpeed,
                ChamberSpeed,
                Length,
                CanCylceSubs,
                Ergonomics,
                Accuracy,
                CenterOfImpact,
                HeatFactor,
                CoolFactor,
                MagMalfunctionChance,
                LoadUnloadModifier,
                CheckTimeModifier,
                DurabilityBurnModificator,
                HasShoulderContact,
                BlocksFolding,
                Velocity,
                RecoilAngle,
                ConflictingItems,
                FixSpeed,
                StockAllowADS,
                Weight
            };
            return item;
        }
    }
}