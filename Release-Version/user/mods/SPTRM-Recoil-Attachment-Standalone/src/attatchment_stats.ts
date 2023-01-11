import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "../types/models/spt/utils/ILogger";
import { Arrays } from "./arrays";

export class AttatchmentStats {

    constructor(private logger: ILogger, private tables: IDatabaseTables, private modConf, private arrays: Arrays) { }

    private itemDB = this.tables.templates.items;


    public loadAttStats() {



        if (this.modConf.logEverything == true) {
            this.logger.info("Attatchment Stats Loaded");
        }
    }
}