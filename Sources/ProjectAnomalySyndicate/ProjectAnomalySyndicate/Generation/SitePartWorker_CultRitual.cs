﻿using RimWorld;
using RimWorld.BaseGen;
using RimWorld.Planet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI.Group;

namespace ProjectAnomalySyndicate.Generation
{
    public class SitePartWorker_CultRitual : SitePartWorker
    {
        public override void Init(Site site, SitePart sitePart)
        {
            base.Init(site, sitePart);
            //site.customLabel = sitePart.def.label.Formatted(site.Faction.Named("FACTION"));
        }

        /* public override string GetPostProcessedThreatLabel(Site site, SitePart sitePart)
         {
             return (string)("Hostiles".Translate() + ": " + "Unknown".Translate().CapitalizeFirst()) + ("\n" + "Contains".Translate() + ": " + "Unknown".Translate().CapitalizeFirst());
         }*/

        private const int SpawnRadius = 20;
        public override void PostMapGenerate(Map map)
        {
            Site site = map.Parent as Site;
            List<Pawn> ritualists = new List<Pawn>() { PawnGenerator.GeneratePawn(DefOfs.Horaxian_Highthrall, Faction.OfHoraxCult), PawnGenerator.GeneratePawn(DefOfs.Horaxian_Underthrall, Faction.OfHoraxCult), PawnGenerator.GeneratePawn(DefOfs.Horaxian_Underthrall, Faction.OfHoraxCult) };
            PawnGroupKindDef pawnGroup = SyndicateUtility.GetAnomalyGroupKindDefBasedOnMonolithLevel().RandomElement();
            float num = 265f - site.ActualThreatPoints;
            List<Pawn> cultists = new List<Pawn>();
            if(num >= Faction.OfHoraxCult.def.MinPointsToGeneratePawnGroup(pawnGroup)){

                cultists.Concat(PawnGroupMakerUtility.GeneratePawns(new PawnGroupMakerParms
                {
                    groupKind = PawnGroupKindDefOf.PsychicRitualSiege,
                    points = num,
                    faction = Faction.OfHoraxCult
                }).ToList());
            }
            DistressCallUtility.SpawnPawns(map, cultists, map.Center, 20);
            Lord lord = LordMaker.MakeNewLord(Faction.OfHoraxCult, new LordJob_DefendBase(Faction.OfHoraxCult, map.Center), map, cultists);
            DistressCallUtility.SpawnPawns(map, ritualists, map.Center, 3);
            Lord lord1 = LordMaker.MakeNewLord(Faction.OfHoraxCult, new LordJob_DefendPoint(map.Center, 5, 5, false, false), map, ritualists);

            foreach (Thing allThing in map.listerThings.AllThings)
            {
                if (allThing.def.category == ThingCategory.Item)
                {
                    CompForbiddable compForbiddable = allThing.TryGetComp<CompForbiddable>();
                    if (compForbiddable != null && !compForbiddable.Forbidden)
                    {
                        allThing.SetForbidden(value: true, warnOnFail: false);

                    }
                }

            }

        }

    }
}
