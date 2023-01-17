import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "../types/models/spt/utils/ILogger";

const mechGunsmith = require("../db/traders/quests/mechanic/gunsmith.json");


export class Quests {
    constructor(private tables: IDatabaseTables) { }

    private questDB = this.tables.templates.quests;


    public fixMechancicQuests() {

        for (let quest in this.questDB) {

            if (this.questDB[quest].QuestName.includes("Gunsmith")) {
                let conditions = this.questDB[quest].conditions.AvailableForFinish[0];
                conditions._props.ergonomics.value *= 0.1;
                conditions._props.recoil.value *= 10;
                conditions._props.weight.value *= 10;
                conditions._props.width.compareMethod = ">=";
                conditions._props.width.value = 0;
            }
        }
    }
}
