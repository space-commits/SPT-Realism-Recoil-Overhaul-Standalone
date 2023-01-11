import { DependencyContainer } from "tsyringe";
import { IPostDBLoadMod } from "@spt-aki/models/external/IPostDBLoadMod";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { ILogger } from "../types/models/spt/utils/ILogger";
import { PreAkiModLoader } from "@spt-aki/loaders/PreAkiModLoader";
import { IPostAkiLoadMod } from "@spt-aki/models/external/IPostAkiLoadMod";


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

        this.dllChecker(logger, modConfig);

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
    }

    public postAkiLoad(container: DependencyContainer) {
        this.modLoader = container.resolve<PreAkiModLoader>("PreAkiModLoader");
    }

    private dllChecker(logger: ILogger, modConfig: any) {
        const plugin = _path.join(__dirname, '../../../../BepInEx/plugins/RealismMod.dll');

        if (!fs.existsSync(plugin)) {

            ConfigChecker.dllIsPresent = false;
        } else {
            ConfigChecker.dllIsPresent = true;
        }
    }
}

module.exports = { mod: new Main() }

