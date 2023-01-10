import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "../types/models/spt/utils/ILogger";
import { Arrays } from "./arrays";

export class AttatchmentBase {

    constructor(private logger: ILogger, private tables: IDatabaseTables, private arrays: Arrays, private modConf) { }

    private itemDB = this.tables.templates.items;

    public loadAttCompat() {
        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._parent === "55818a104bdc2db9688b4569") {
                for (let slot in serverItem._props.Slots) {
                    if (serverItem._props.Slots[slot]._name === "mod_scope" || serverItem._props.Slots[slot]._name === "mod_tactical") {

                        var serverArr = serverItem._props.Slots[slot]._props.filters[0].Filter;
                        var compatMods = [
                            "57fd23e32459772d0805bcf1",
                            "544909bb4bdc2d6f028b4577",
                            "5d10b49bd7ad1a1a560708b0",
                            "5c06595c0db834001a66af6c",
                            "5a7b483fe899ef0016170d15",
                            "61605d88ffa6e502ac5e7eeb",
                            "5c5952732e2216398b5abda2"
                        ];

                        serverItem._props.Slots[slot]._props.filters[0].Filter = compatMods.concat(serverArr);
                    }
                }
            }
            
            if (serverItem._parent === "55818a304bdc2db5418b457d") {
                for (let slot in serverItem._props.Slots) {
                    if (serverItem._props.Slots[slot]._name === "mod_sight_rear") {
                        var serverArr = serverItem._props.Slots[slot]._props.filters[0].Filter;
                        var compatMods = ["5649a2464bdc2d91118b45a8"];
                        serverItem._props.Slots[slot]._props.filters[0].Filter = compatMods.concat(serverArr);
                    }
                }
            }

            let cantedMountConfWeaps: string[] = ["5926bb2186f7744b1c6c6e60", "5d2f0d8048f0356c925bc3b0", "5e00903ae9dc277128008b87", "5de7bd7bfd6b4e6e2276dc25"];

            for (let item in cantedMountConfWeaps) {
                if (serverItem._id === cantedMountConfWeaps[item]) {
                    serverItem._props.ConflictingItems.push("5649a2464bdc2d91118b45a8");
                }
            }
            if (serverItem._props.weapClass === "pistol") {
                serverItem._props.ConflictingItems.push("5649a2464bdc2d91118b45a8");
            }
        }
    }

}