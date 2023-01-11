import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "../types/models/spt/utils/ILogger";
import { ParentClasses } from "./enums";

export class Ammo {
    constructor(private logger: ILogger, private tables: IDatabaseTables, private modConf) { }

    private itemDB = this.tables.templates.items;

    public loadAmmoFirerateChanges() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._parent === ParentClasses.AMMO) {
                serverItem._props.casingMass = Math.min(1.05, (serverItem._props.ammoRec / 500) + 1);
            }
        }
        if (this.modConf.logEverything == true) {
            this.logger.info("Ammo Firerate Stats Loaded");
        }
    }
}
