import { DependencyContainer } from "tsyringe";
import { IPostDBLoadMod } from "@spt-aki/models/external/IPostDBLoadMod";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { ILogger } from "../types/models/spt/utils/ILogger";
import { PreAkiModLoader } from "@spt-aki/loaders/PreAkiModLoader";
import { IPostAkiLoadMod } from "@spt-aki/models/external/IPostAkiLoadMod";
import { DynamicRouterModService } from "@spt-aki/services/mod/dynamicRouter/DynamicRouterModService"
import { JsonUtil } from "@spt-aki/utils/JsonUtil";

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
const modConfig = require("../config/config.json");

class Main implements IPostDBLoadMod, IPostAkiLoadMod {

    private path: { resolve: (arg0: string) => any; };
    private modLoader: PreAkiModLoader;

    public preAkiLoad(container: DependencyContainer): void {
        const jsonUtil = container.resolve<JsonUtil>("JsonUtil");

        const router = container.resolve<DynamicRouterModService>("DynamicRouterModService");
        this.path = require("path");

        router.registerDynamicRouter(
            "loadRes",
            [
                {
                    url: "/RecoilStandalone/GetInfo",
                    action: (url, info, sessionId, output) => {
                        return jsonUtil.serialize(this.path.resolve(this.modLoader.getModPath("SPTRM-Recoil-Attachment-Standalone")));
                    }
                }
            ],
            "RecoilStandalone"
        )
    }

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
        const descGen = new DescriptionGen(tables);
        const jsonHand = new JsonHandler(tables, logger);

        this.dllChecker(logger);

        if (ConfigChecker.dllIsPresent == true) {
            attachStats.loadAttStats();
            jsonHand.pushModsToServer();
            jsonHand.pushWeaponsToServer();
            jsonHand.pushArmorToServer();
            descGen.descriptionGen();
            ammo.loadAmmoFirerateChanges();
            attachBase.loadAttCompat();
            weaponsGlobals.loadGlobalWeps();
        }

        logger.warning(""+tables.templates.items["5447a9cd4bdc2dbd208b4567"]._props.Weight);
    }

    public postAkiLoad(container: DependencyContainer) {
        this.modLoader = container.resolve<PreAkiModLoader>("PreAkiModLoader");
    }

    private dllChecker(logger: ILogger) {
        const dll = _path.join(__dirname, '../../../../BepInEx/plugins/RecoilStandalone.dll');
        const realismdll = _path.join(__dirname, '../../../../BepInEx/plugins/RealismMod.dll');

        if (!fs.existsSync(dll)) {
            ConfigChecker.dllIsPresent = false;
            logger.error("RecoilStandalone.dll is missing form path: " + dll + ", mod disabled.");
        }
        else if (fs.existsSync(realismdll)) {
            ConfigChecker.dllIsPresent = false;
            logger.error("RealismMod.dll is present at path: " + realismdll + ", either use this standalone mod or use Realism Mod. Mod disabled.");

        }
        else { ConfigChecker.dllIsPresent = true; 
        }
    }
}

module.exports = { mod: new Main() }


