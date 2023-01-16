import { IDatabaseTables } from "@spt-aki/models/spt/server/IDatabaseTables";
import { ILogger } from "../types/models/spt/utils/ILogger";

const mechGunsmith = require("../db/traders/quests/mechanic/gunsmith.json");


export class Quests {
    constructor(private logger: ILogger, private tables: IDatabaseTables, private modConf) { }

    private questDB = this.tables.templates.quests;
    private locales = this.tables.locales.global["en"];
    


    public removeFIRQuestRequire() {
        for (let quest in this.questDB) {
            let availForFin = this.questDB[quest].conditions.AvailableForFinish;
            for (let requirement in availForFin) {
                if (availForFin[requirement]._props.onlyFoundInRaid) {
                    availForFin[requirement]._props.onlyFoundInRaid = false;
                }
            }
        }

        if (this.modConf.logEverything == true) {
            this.logger.info("FIR Requirements Removed");
        }
    }

    public fixMechancicQuests() {

        var ergo = 0;
        var recoil = 0;
        var weight = 0;
        var descID = "";

        var questDescs: Array<Record<string, string>> = [];

        for (let quest in this.questDB) {
            for (let myQuest in mechGunsmith) {
                if (this.questDB[quest]._id === mechGunsmith[myQuest]._id) {
                    this.questDB[quest] = mechGunsmith[myQuest];
                    ergo = mechGunsmith[myQuest].conditions.AvailableForFinish[0]._props.ergonomics.value;
                    recoil = mechGunsmith[myQuest].conditions.AvailableForFinish[0]._props.recoil.value;
                    weight = mechGunsmith[myQuest].conditions.AvailableForFinish[0]._props.weight.value;
                    descID = mechGunsmith[myQuest].description;

                    if (this.modConf.unstuck_GS) {
                        ergo *= 0.25;
                        recoil *= 2;
                        weight *= 2;
                    }

                    this.questDescHelper(questDescs, descID, "5ac23c6186f7741247042bad description", "Hello, mercenary. Yes, I am Mechanic. Let's get to the point, did you want to help? I have a lot of work to do today. There’s so little time, and the programmer unit for PLC100 is out of order. And those weapon orders... Anyway, here's why I'm telling you this. I build various weapons with modifications for my clients, and I'll need some help with it. I need you to assemble an MP-133, I won’t have time to build it myself, and I don't have parts for it either anyway. This gun should have more than " + ergo + " ergonomics, a laser designator, an extended mag, and less than " + recoil + " recoil sum. And for the love of God, don't put one of those stockless pistol grips on it, the customer isn't some drunken gopnik! Build one for me, and then we can talk about more work.");
                    this.questDescHelper(questDescs, descID, "5ac2426c86f774138762edfe description", "You have not failed me with the shotgun, so you may know your way around tools. That didn’t save me much time though. All exchanges are on fire, hope it won’t start a panic. Nevertheless, I think it's not for long, so the plan remains the same. Okay, I need to work. And you, try to get hold of an AKS-74U. With a Zenit B-11 handguard, 60-round extended mag. Don't give it to me with the stock folded, even Scavs know not to use rifles without their stock. Ergonomics over " + ergo + ", recoil sum less than " + recoil + ", sighting range 100 or more. And, of course, as light as possible, " + weight + "kg or less. Yes, indexes here have nothing to do with that. I hope you've remembered everything. See you soon.");
                    this.questDescHelper(questDescs, descID, "5ac2428686f77412450b42bf description", "When I was a child, people used to say that it is not the destination that matters, but the journey itself. But what if the destination is yourself, what you know, how good you use your reflexes, and how many rounds you have in the magazine? This feels particularly true when you're supposed to survive, clinging to this world with your teeth. Well, to the point: I need an MP5. With a 50-round extended mag, silencer, tactical flashlight, and sighting range of more than 200. Ergonomics above " + ergo + ", recoil sum less than " + recoil + ". Maximum weight of " + weight + " kg. And decide for yourself what is more important to you, the journey or the destination.");
                    this.questDescHelper(questDescs, descID, "5ac244eb86f7741356335af1 description", "Sometimes I think it's good that I got stuck here in Tarkov. The whole world feels like it’s descending into an abyss, a gaping, cold abyss. Everything has lost its meaning. People, their values, their thoughts, and their goals. They don't know anymore why they should take joy, work, have children. Everyone is tired from the illusion of choice they were offered all this time, from having to work and buy either blue or red cigarettes. It’s happening everywhere, in every corner of the world. But here in Tarkov, we are honest with ourselves. We see it all in an already exaggerated way, without false values, when you don't just decide what laundry detergent is better at removing stains, but how to keep warm at night and where to find ammo to deal with the next lowlife who wants to kill you. Only in such an honest society, people show their true selves. What do you think? Okay, thanks for hearing me out. Some folks requested a fully packed M4. They need a silencer, 60-round extended mag, ergonomics above " + ergo + ", and recoil sum less than " + recoil + ". And mind the weight, no more than " + weight + " kg, with a sighting range of 800 or beyond.");
                    this.questDescHelper(questDescs, descID, "5ac242ab86f77412464f68b4 description", "I’ve said too much last time. It happens when I’m alone with my thoughts for a while. A lot of stuff comes to mind while I sit here calibrating the sights. That M4 turned out splendid, got taken off my hands in a moment. A BEAR dropped by recently, goes by the name of Sniper or something. He needs a Lobaev DVL Saboteur sniper rifle, with a scope and multi-laser device. With recoil sum less than " + recoil + ", ergonomics above " + ergo + ", and weight below " + weight + "kg. He said it needs to be a 2000m shot. The guy is serious, so do your best to make a solid build.");
                    this.questDescHelper(questDescs, descID, "5ac244c486f77413e12cf945 description", "Are you good at hiding? Not good enough for Sniper not to find you, I think. I don't envy the guy, what’s his name, Brendan I think. Interesting, all those heroes of the past who we admired so much, where have they all gone? Why has everybody become so empty? Why now, with this chaos around, such folk as Sniper became heroes for us? Why have we been ignoring such war dogs, and now we’re looking forward to being friends with them? What do you think? I have another order, for an R11 RSASS. With a sighting range of 1500 or above. Weight " + weight + " kg or less. Also needs a silencer, ergonomics more than " + ergo + ", and recoil sum less than " + recoil + ".");
                    this.questDescHelper(questDescs, descID, "5ae3267986f7742a413592fe description", "Just when I finally sorted out the stable Internet connection and power supply for it, the Government stepped up the game and started blocking half the IP addresses that handled most of the data flow in Tarkov. All that because TerraGroup allegedly uses \"Ebambium\" ISP to further destabilize the situation in the city and wreak havoc. Sounds like that cultist gibberish about the Ragnarok coming, don't you think? By the way about the cultists and weapons — one of them, with a lightning symbol tattooed on his face, asked for a Remington 870 shotgun. Equipped with a tactical device, Magpul MOE handguard, and a tactical foregrip. Ergonomics above " + ergo + " and recoil sum less than " + recoil + ".");
                    this.questDescHelper(questDescs, descID, "5ae3270f86f77445ba41d4dd description", "How do you think, is all the suffering that befell the inhabitants of our city the result of the actions of the society, or a controlled process, and this is just the beginning? I've been thinking about this all the time after that guy with the lightning tattoo. Although I decided for myself that even if it’s someone's invisible hand, our society has deserved it. We have, by our actions, by buying sneakers or even beet salad, supported those who build laboratories and manufacturers, and then use them to plunge us into the abyss. Okay, I’m rambling again, back to business. Get me an AKM with a Fortis Shift grip, silencer, and 30-round Magpul mag. Ergonomics over " + ergo + ", recoil sum less than " + recoil + ", sighting range of 800 meters or more.");
                    this.questDescHelper(questDescs, descID, "5ae3277186f7745973054106 description", "I have two neural networks, one scans the faces on the cameras and compares them with the database, and the other one monitors cryptocurrency trading and reacts to it. Today I’ve decided to create one more, which will monitor the temperature, humidity, pressure, and other parameters of my hideout, and among other things — respond to my mood. Let's see how it turns out when I’m done. There is an order for AKS-74N with Zenit parts. Ergonomics should be over " + ergo + ", recoil sum less than " + recoil + ", and don't try to give it to me with the stock folded like last time! And most importantly, with an RK-3 pistol grip, PT-3 stock, B-33 dust cover, and DTK-1 muzzle brake by Zenit, Zenit all around. Perhaps, a gift for a former Saint Petersburg citizen.");
                    this.questDescHelper(questDescs, descID, "5ae327c886f7745c7b3f2f3f description", "I called her Lusya - the neural network, I mean. So, when I keep silent, it does well and even reminds me to get some sleep when necessary, but as soon as I start talking to myself, it attempts to distract me, putting on music or something like that. However, I trained it with movies and phrases from them. Apparently, such a reaction is presented to us in the movies as the most appropriate, but not for me. I'll have to think about what stimuli are the best to mimic real people, rather than society and its stereotypes. But I digress, assemble an AK-105 now, with a silencer and 60-round mag. As for parameters: sighting range over 800, ergonomics above " + ergo + ", recoil sum less than " + recoil + ", weight " + weight + " kg or less. You should know by now to give it to me wit the stock unfolded.");
                    this.questDescHelper(questDescs, descID, "5ae3280386f7742a41359364 description", "While I was busy with the network, I started to wonder how are we different from it, and when we can replace our short-lived brains with those kinds of systems. What do you think, will this time come someday? I think it will, very soon. We already don't know what processes occur inside neural networks, there is only an entry signal and the result of its actions, and how it has learned with its own internal rules, wiring up its own neural links inside itself. How is it different from us? Would you love living in a world where your actions get limited only by your mind and imagination? The truth is, I think, living in such a world would be no better than in our own if there is no goal. And where can a goal come from if you're immortal and omnipotent... This time we need an AS VAL. GL-SHOCK buttstock, AN/PEQ-15 tactical device, NSPU-M night scope, and a 30-round magazine. Ergonomics above " + ergo + " and recoil sum less than " + recoil + ".");
                    this.questDescHelper(questDescs, descID, "5b47749f86f7746c5d6a5fd4 description", "Quite long ago, I once met the owner of KIBA Arms, we even went to a sauna together then, though I’m not particularly into it. He even broke his leg trying to jump-dive from the pier that day. Jolly fellow dreamt of setting up an airsoft field all the time. It seems his wish came true in Tarkov, the best and largest airsoft field in the world, with an only minor difference: here, you die and lose everything from a single stray bullet. Put together an AK-102, with a Rotor 43 silencer, Magpul AFG OD tactical grip, and the Zenit Klesch-2U flashlight. Ergonomics should be over " + ergo + ", recoil sum less than " + recoil + ", sighting range 500 or more. As always, stock unfolded, the customer isn't the type to run around the Factory with a faceshield like some juggernaut");
                    this.questDescHelper(questDescs, descID, "5b47799d86f7746c5d6a5fd8 description", "Okay, we have an urgent order here, don't have much time. Some guy stopped by, said wants to sort it out with Reshala, who is operating somewhere around dorms together with folks from the factory. Build him an MPX, with the Tactical Dynamics Skeletonized pistol grip, Annihilator flash hider, and Steiner LAS/TAC 2 tactical flashlight. Aim for a sighting range of 300 or more, ergonomics above " + ergo + ", weight less than " + weight + " kg, the less than " + recoil + " recoil sum. And don't tell anyone why you need this gun. If Reshala hears about it, he won’t be too happy, and his guys are quite a mean bunch, all served in the military before working at the factory.");
                    this.questDescHelper(questDescs, descID, "5b477b6f86f7747290681823 description", "I want to try teaching Lusya to tell if someone's lying, if the result will be at least 90% accurate, it will be interesting to listen to all those politicians and TerraGroup whales, who’ve been expatiating upon their ideals and noble goals just yesterday, assured of the benefits of our economic zone and a bright future ahead. How much of this was a concentrated lie? I think, all of it. There’s a request for a MOE edition AKMN. I.e. the handguard, stock, and pistol grip have to be MOE. All in black. Additionally, a Rotor 43 muzzle brake and Magpul M-LOK AFG Tactical grip (Black), recoil sum lower than " + recoil + ", ergonomics above " + ergo + ", and weight of " + weight + " kg or less with sighting range of 500.");
                    this.questDescHelper(questDescs, descID, "5b477f7686f7744d1b23c4d2 description", "This one is for Sniper, it seems that he’s about to set out to the woods somewhere to hunt down villains for a week. Judging from the fact that he packed one hell of a backpack with an MRE, firesteel, batteries, and water. Left it at my place for now. He needs an M1A, fitted with an UltiMAK M8 Forward Optic mount, Nightforce ATACR 7-35x56 scope, Micro T-1 reflex sight, and a Holosun LS321 tactical device. Ergonomics above " + ergo + ", recoil sum below " + recoil + ", and a total weight of " + weight + " kg or less.");
                    this.questDescHelper(questDescs, descID, "5b47825886f77468074618d3 description", "What do you think, how much does human life cost? Not much, I guess. But what’s even more surprising for me, it’s the contemporary art market. The value of art pieces by renowned artists grows every year. They are definitely talented, but the more you look at it, the more it seems that anything can be art. It can be a Rothko painting, a displaced urinal, or a bunch of garbage. However, in my opinion, the most amazing pieces of art are weapons, starting from the melee - which is already universally recognized as such and can be found in many museums - to an old AK or M4, which are beautiful as they are. But a weapon only transcends to true art when it becomes unique, one-of-a-kind, in the hands of someone who knows how to use it, who modifies it for his own needs, creating a masterpiece worthy of being displayed in the best of museums. Bring me one such masterpiece, an M4A1 with a KAC QDSS NT-4 (FDE) silencer, Alexander Arms MK10 rifle-length handguard, REAP-IR thermal scope, and a 60-round magazine. Ergonomics should be not less than " + ergo + ", the recoil sum less than " + recoil + ", and the weight of " + weight + " kg or less.");
                }
            }
        }

        for (let desc in questDescs) {
            this.locales[questDescs[desc].descId] = questDescs[desc].desc;
        }

        if (this.modConf.logEverything == true) {
            this.logger.info("Mechanic Quests Fixed");
        }
    }

    private questDescHelper(array: Array<Record<string, string>>, descId: string, targetId: string, desc: string) {
        if (descId === targetId) {
            array.push({ descId, desc });
        }
    }


}
