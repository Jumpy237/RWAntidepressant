using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using Verse;
using RimWorld;
using UnityEngine;

namespace RWAntidepressant
{
    
    class RWAntidepressantInit : Mod
    {
        public RWAntidepressantInit(ModContentPack content) : base(content)
        {
            var harmony = HarmonyInstance.Create("net.pardeike.rimworld.mod.camera+");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
    [HarmonyPatch(typeof(IngestionOutcomeDoer_GiveHediff), "DoIngestionOutcomeSpecial")]
    public class Patches : Bullet
    {

        private static void Postfix(IngestionOutcomeDoer_GiveHediff __instance, Pawn pawn, Thing ingested)
        {
            
            ThoughtStage ts = new ThoughtStage()
            {
                label = "took antidepressants",
                description = "I feel calm. LOL",
                baseMoodEffect = -50,

            };
            ThoughtDef td = new ThoughtDef()
            {
                defName = "AteAntiDepressant",
                durationDays = 0.5f,
                stackLimit = 1,
                stages = new List<ThoughtStage>() { ts },
            };
            pawn.needs.mood.thoughts.memories.TryGainMemory(td, null);
            Debug.Log("debug : " + ingested.def.defName + " " + pawn.Name);
        }   
        
    }
}
