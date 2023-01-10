import { DependencyContainer } from "tsyringe";
import { IPreAkiLoadMod } from "@spt-aki/models/external/IPreAkiLoadMod";
import { IPostDBLoadMod } from "@spt-aki/models/external/IPostDBLoadMod";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { ILogger } from "../types/models/spt/utils/ILogger";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import type { StaticRouterModService } from "@spt-aki/services/mod/staticRouter/StaticRouterModService";
import { ProfileHelper } from "@spt-aki/helpers/ProfileHelper";
import { HttpResponseUtil } from "@spt-aki/utils/HttpResponseUtil";
import { IRagfairConfig } from "@spt-aki/models/spt/config/IRagfairConfig";
import { ITraderConfig } from "@spt-aki/models/spt/config/ITraderConfig";
import { IAirdropConfig } from "@spt-aki/models/spt/config/IAirdropConfig";
import { IPmcData } from "@spt-aki/models/eft/common/IPmcData";
import { RandomUtil } from "@spt-aki/utils/RandomUtil";
import { HashUtil } from "@spt-aki/utils/HashUtil";
import { BotWeaponGenerator } from "@spt-aki/generators/BotWeaponGenerator";
import { BotGeneratorHelper } from "@spt-aki/helpers/BotGeneratorHelper";
import { WeightedRandomHelper } from "@spt-aki/helpers/WeightedRandomHelper";
import { JsonUtil } from "@spt-aki/utils/JsonUtil";
import { Chances, Inventory, ItemMinMax, Items, Mods, ModsChances } from "@spt-aki/models/eft/common/tables/IBotType";
import { Item } from "@spt-aki/models/eft/common/tables/IItem";
import { ITemplateItem } from "@spt-aki/models/eft/common/tables/ITemplateItem";
import { RagfairOfferService } from "@spt-aki/services/RagfairOfferService";
import { ItemHelper } from "@spt-aki/helpers/ItemHelper";
import { ProbabilityHelper } from "@spt-aki/helpers/ProbabilityHelper";
import { RagfairOfferGenerator } from "@spt-aki/generators/RagfairOfferGenerator";
import { GenerateWeaponResult } from "@spt-aki/models/spt/bots/GenerateWeaponResult";
import { BotLootCacheService } from "@spt-aki/services/BotLootCacheService";
import { BotEquipmentFilterService } from "@spt-aki/services/BotEquipmentFilterService";
import { ItemFilterService } from "@spt-aki/services/ItemFilterService";
import { BotWeaponGeneratorHelper } from "@spt-aki/helpers/BotWeaponGeneratorHelper";
import { IInventoryMagGen } from "@spt-aki/generators/weapongen/IInventoryMagGen";
import { DynamicRouterModService } from "@spt-aki/services/mod/dynamicRouter/DynamicRouterModService"
import { PreAkiModLoader } from "@spt-aki/loaders/PreAkiModLoader";
import { IPostAkiLoadMod } from "@spt-aki/models/external/IPostAkiLoadMod";
import { ApplicationContext } from "@spt-aki/context/ApplicationContext";
import { IStartOfflineRaidRequestData } from "@spt-aki/models/eft/match/IStartOffineRaidRequestData";
import { WeatherController } from "@spt-aki/controllers/WeatherController";
import { ContextVariableType } from "@spt-aki/context/ContextVariableType";
import { LocalisationService } from "@spt-aki/services/LocalisationService";
import { IBotConfig } from "@spt-aki/models/spt/config/IBotConfig";
import { IInventoryConfig } from "@spt-aki/models/spt/config/IInventoryConfig";
import { TraderAssortHelper } from "@spt-aki/helpers/TraderAssortHelper";
import { MathUtil } from "@spt-aki/utils/MathUtil";
import { TimeUtil } from "@spt-aki/utils/TimeUtil";
import { AssortHelper } from "@spt-aki/helpers/AssortHelper";
import { RagfairAssortGenerator } from "@spt-aki/generators/RagfairAssortGenerator";
import { TraderHelper } from "@spt-aki/helpers/TraderHelper";
import { FenceService } from "@spt-aki/services/FenceService";
import { TraderAssortService } from "@spt-aki/services/TraderAssortService";
import { PaymentHelper } from "@spt-aki/helpers/PaymentHelper";
import { ITrader } from "@spt-aki/models/eft/common/tables/ITrader";
import { IRegisterPlayerRequestData } from "@spt-aki/models/eft/inRaid/IRegisterPlayerRequestData";
import { TraderPurchasePersisterService } from "@spt-aki/services/TraderPurchasePersisterService";
import { RagfairServer } from "@spt-aki/servers/RagfairServer";;
import { BotEquipmentModGenerator } from "@spt-aki/generators/BotEquipmentModGenerator";
import { BotModLimits, BotWeaponModLimitService } from "@spt-aki/services/BotWeaponModLimitService";
import { BotHelper } from "@spt-aki/helpers/BotHelper";
import { BotEquipmentModPoolService } from "@spt-aki/services/BotEquipmentModPoolService";
import { BotLootGenerator } from "@spt-aki/generators/BotLootGenerator";
import { IBotBase, Inventory as PmcInventory } from "@spt-aki/models/eft/common/tables/IBotBase";
import { HandbookHelper } from "@spt-aki/helpers/HandbookHelper";
import { BotLevelGenerator } from "@spt-aki/generators/BotLevelGenerator";
import { MinMax } from "@spt-aki/models/common/MinMax";
import { IRandomisedBotLevelResult } from "@spt-aki/models/eft/bot/IRandomisedBotLevelResult";
import { BotGenerationDetails } from "@spt-aki/models/spt/bots/BotGenerationDetails";
import { SeasonalEventService } from "@spt-aki/services/SeasonalEventService";
import { SaveServer } from "@spt-aki/servers/SaveServer";
import { ILocations } from "@spt-aki/models/spt/server/ILocations";

import { Ammo } from "./ammo";
import { AttatchmentBase as AttachmentBase } from "./attatchment_base";
import { AttatchmentStats as AttachmentStats } from "./attatchment_stats";
import { ConfigChecker, Helper } from "./helper"
import { WeaponsGlobals } from "./weapons_globals"
import { CodeGen } from "./code_gen";
import * as _path from 'path';
import { DescriptionGen } from "./description_gen";
import { JsonHandler } from "./json-handler";
import { Arrays } from "./arrays";

const fs = require('fs');
const custFleaBlacklist = require("../db/traders/ragfair/blacklist.json");
const medItems = require("../db/items/med_items.json");
const crafts = require("../db/items/hideout_crafts.json");
const buffs = require("../db/items/buffs.json");
const custProfile = require("../db/profile/profile.json");
const botHealth = require("../db/bots/botHealth.json");
const modConfig = require("../config/config.json");
const airdropLoot = require("../db/airdrops/airdrop_loot.json");
const pmcTypes = require("../db/bots/pmcTypes.json");

class Main implements IPostDBLoadMod, IPostAkiLoadMod {

    private path: { resolve: (arg0: string) => any; };
    private modLoader: PreAkiModLoader;

    public postDBLoad(container: DependencyContainer): void {

        const logger = container.resolve<ILogger>("WinstonLogger");
        const databaseServer = container.resolve<DatabaseServer>("DatabaseServer");
        const tables = databaseServer.getTables();
        const arrays = new Arrays(tables);
        const helper = new Helper(tables, arrays);
        const ammo = new Ammo(logger, tables, modConfig);
        const attachBase = new AttachmentBase(logger, tables, arrays, modConfig);
        const attachStats = new AttachmentStats(logger, tables, modConfig, arrays);
        const weaponsGlobals = new WeaponsGlobals(logger, tables, modConfig);
        const codegen = new CodeGen(logger, tables, modConfig, helper, arrays);

        const descGen = new DescriptionGen(tables);
        const jsonHand = new JsonHandler(tables);

        this.dllChecker(logger, modConfig);


        // codegen.attTemplatesCodeGen();
        // codegen.weapTemplatesCodeGen();
        // codegen.armorTemplatesCodeGen();

        if (modConfig.recoil_attachment_overhaul && ConfigChecker.dllIsPresent == true) {
            attachStats.loadAttStats();
            jsonHand.pushModsToServer();
            jsonHand.pushWeaponsToServer();
            jsonHand.pushArmorToServer();
            descGen.descriptionGen();
            ammo.loadAmmoFirerateChanges();
            attachBase.loadAttCompat();
            weaponsGlobals.loadGlobalWeps();
        }
    }

    public postAkiLoad(container: DependencyContainer) {
        this.modLoader = container.resolve<PreAkiModLoader>("PreAkiModLoader");
    }

    private dllChecker(logger: ILogger, modConfig: any) {
        const plugin = _path.join(__dirname, '../../../../BepInEx/plugins/RealismMod.dll');

        if (fs.existsSync(plugin)) {
            ConfigChecker.dllIsPresent = true;
            if (modConfig.recoil_attachment_overhaul == true) {
                logger.error("RecoilOverhaul.dll is missing form path: " + plugin + ", mod disabled.");
            }
            ConfigChecker.dllIsPresent = false;
        }
    }
}

module.exports = { mod: new Main() }


