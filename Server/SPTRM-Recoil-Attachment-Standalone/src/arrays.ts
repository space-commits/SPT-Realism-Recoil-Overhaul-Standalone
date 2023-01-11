import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";

export class Arrays {

    constructor(private tables: IDatabaseTables) { }

    public gearParentIDs = [
        "5448e5284bdc2dcb718b4567",
        "5448e54d4bdc2dcc718b4568",
        "57bef4c42459772e8d35a53b",
        "5a341c4086f77401f2541505",
        "5448e53e4bdc2d60728b4567",
        "5a341c4686f77469e155819e",
        "5645bcb74bdc2ded0b8b4578",
        "5b3f15d486f77432d0509248",
        "5448e5724bdc2ddf718b4568"
    ]

    public weaponParentIDs = [
        "5447b5e04bdc2d62278b4567",
        "5447b5f14bdc2d61278b4567",
        "5447b5cf4bdc2d65278b4567",
        "5447b6094bdc2dc3278b4567",
        "5447b6194bdc2d67278b4567",
        "5447b6254bdc2dc3278b4568",
        "5447bee84bdc2dc3278b4569",
        "5447bedf4bdc2d87278b4568",
        "5447bed64bdc2d97278b4568",
        "5447b5fc4bdc2d87278b4567",
        "5447e1d04bdc2dff2f8b4567"
    ]

    public modParentIDs = [
        "550aa4bf4bdc2dd6348b456b",
        "550aa4dd4bdc2dc9348b4569",
        "550aa4cd4bdc2dd8348b456c",
        "555ef6e44bdc2de9068b457e",
        "55818b224bdc2dde698b456f",
        "55818a304bdc2db5418b457d",
        "55818a594bdc2db9688b456a",
        "55818a6f4bdc2db9688b456b",
        "55818acf4bdc2dde698b456b",
        "55818ad54bdc2ddc698b4569",
        "55818add4bdc2d5b648b456f",
        "55818ae44bdc2dde698b456c",
        "55818ac54bdc2d5b648b456e",
        "55818aeb4bdc2ddc698b456a",
        "5448bc234bdc2d3c308b4569",
        "5a74651486f7744e73386dd1",
        "55818af64bdc2d5b648b4570",
        "55818a684bdc2ddd698b456d",
        "56ea9461d2720b67698b456f",
        "55818a104bdc2db9688b4569",
        "55818afb4bdc2dde698b456d",
        "55818b084bdc2d5b648b4571",
        "55818b164bdc2ddc698b456c",
        "610720f290b75a49ff2e5e25",
        "627a137bf21bc425b06ab944"
    ]

    public modTypes = {
        "FlashHider": "550aa4bf4bdc2dd6348b456b",
        "MuzzleCombo": "550aa4dd4bdc2dc9348b4569",
        "Silencer": "550aa4cd4bdc2dd8348b456c",
        "Barrel": "555ef6e44bdc2de9068b457e",
        "Mount": "55818b224bdc2dde698b456f",
        "Receiver": "55818a304bdc2db5418b457d",
        "Stock": "55818a594bdc2db9688b456a",
        "Charge": "55818a6f4bdc2db9688b456b",
        "CompactCollimator": "55818acf4bdc2dde698b456b",
        "Collimator": "55818ad54bdc2ddc698b4569",
        "AssaultScope": "55818add4bdc2d5b648b456f",
        "Scope": "55818ae44bdc2dde698b456c",
        "IronSight": "55818ac54bdc2d5b648b456e",
        "SpecialScope": "55818aeb4bdc2ddc698b456a",
        "Magazine": "5448bc234bdc2d3c308b4569",
        "AuxiliaryMod": "5a74651486f7744e73386dd1",
        "Foregrip": "55818af64bdc2d5b648b4570",
        "PistolGrip": "55818a684bdc2ddd698b456d",
        "Gasblock": "56ea9461d2720b67698b456f",
        "Handguard": "55818a104bdc2db9688b4569",
        "Bipod": "55818afb4bdc2dde698b456d",
        "Flashlight": "55818b084bdc2d5b648b4571",
        "TacticalCombo": "55818b164bdc2ddc698b456c",
        "CylinderMagazine": "610720f290b75a49ff2e5e25",
        "GrenadeLauncherMagazine": "627a137bf21bc425b06ab944"
    };
}