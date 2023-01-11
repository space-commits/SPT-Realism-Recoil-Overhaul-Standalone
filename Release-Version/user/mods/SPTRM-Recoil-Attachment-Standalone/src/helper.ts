
import { IPmcData } from "@spt-aki/models/eft/common/IPmcData";
import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { Arrays } from "./arrays";

const fs = require('fs');

export class Helper {

    constructor(private tables: IDatabaseTables, private arrays: Arrays) { }

    public pickRandNumInRange(min: number, max: number): number {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    public pickRandNumOneInTen(): number {
        return Math.floor(Math.random() * (10 - 1 + 1)) + 1;
    }

    public saveToJSONFile(data: any, filePath: string) {

        let dir = __dirname;
        let dirArray = dir.split("\\");
        let modFolder = (`${dirArray[dirArray.length - 4]}/${dirArray[dirArray.length - 3]}/${dirArray[dirArray.length - 2]}/`);
        fs.writeFile(modFolder + filePath, JSON.stringify(data, null, 4), function (err) {
            if (err) throw err;
        });
    }

}

export class ConfigChecker {
    static dllIsPresent: boolean = false;
}

