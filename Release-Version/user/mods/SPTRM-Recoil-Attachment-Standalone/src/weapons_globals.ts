import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "../types/models/spt/utils/ILogger";

export class WeaponsGlobals {
    constructor(private logger: ILogger, private tables: IDatabaseTables, private modConf) { }

    private globalDB = this.tables.globals.config;
    private itemDB = this.tables.templates.items;

    public loadGlobalWeps() {


        this.globalDB.Aiming.AimProceduralIntensity = this.modConf.procedural_intensity;

        for (let i in this.itemDB) {
            let serverItem = this.itemDB[i];
            if (serverItem._props.weapClass === "smg"
                || serverItem._props.weapClass === "shotgun"
                || serverItem._props.weapClass === "assaultCarbine"
                || serverItem._props.weapClass === "sniperRifle"
                || serverItem._props.weapClass === "assaultRifle"
                || serverItem._props.weapClass === "machinegun"
                || serverItem._props.weapClass === "marksmanRifle"
                || serverItem._props.weapClass === "assaultRifle") {
                serverItem._props.Ergonomics = 80;
            }

            if (serverItem._props.weapClass) {
                serverItem._props.Ergonomics *= this.modConf.ergo_multi;
                serverItem._props.RecoilForceUp *= this.modConf.vert_recoil_multi;
                serverItem._props.RecoilForceBack *= this.modConf.horz_recoil_multi;
                serverItem._props.Convergence *= this.modConf.convergence_multi;
                serverItem._props.RecolDispersion *= this.modConf.dispersion_multi;

            }
        }

        this.globalDB.Stamina.AimDrainRate = 0.27;
        this.globalDB.Stamina.AimConsumptionByPose["x"] = 0.05;
        this.globalDB.Stamina.AimConsumptionByPose["y"] = 0.3;
        this.globalDB.Stamina.AimConsumptionByPose["z"] = 1; 
        
        this.globalDB.Aiming.RecoilXIntensityByPose["x"] = 0.67;
        this.globalDB.Aiming.RecoilXIntensityByPose["y"] = 0.7;
        this.globalDB.Aiming.RecoilXIntensityByPose["z"] = 1;

        this.globalDB.Aiming.RecoilYIntensityByPose["x"] = 0.65;
        this.globalDB.Aiming.RecoilYIntensityByPose["y"] = 1.2;
        this.globalDB.Aiming.RecoilYIntensityByPose["z"] = 1;

        this.globalDB.Aiming.RecoilZIntensityByPose["x"] = 0.5;
        this.globalDB.Aiming.RecoilZIntensityByPose["y"] = 1.35;
        this.globalDB.Aiming.RecoilZIntensityByPose["z"] = 1;

        this.globalDB.Aiming.ProceduralIntensityByPose["x"] = 0.05;
        this.globalDB.Aiming.ProceduralIntensityByPose["y"] = 0.4;

        this.globalDB.Aiming.RecoilCrank =  this.modConf.recoil_crank;

        if(this.modConf.logEverything == true) {
            this.logger.info("Weapons Globals Loaded");
        }
    }
}
   






