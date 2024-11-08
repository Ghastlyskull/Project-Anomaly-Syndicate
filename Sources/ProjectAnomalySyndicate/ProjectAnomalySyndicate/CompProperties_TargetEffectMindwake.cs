using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ProjectAnomalySyndicate
{
    public class CompProperties_TargetEffectMindwake : CompProperties
    {
        public ThingDef moteDef;

        public bool withSideEffects = true;

        public HediffDef addsHediff;

        public CompProperties_TargetEffectMindwake()
        {
            compClass = typeof(CompTargetEffect_Mindwake);
        }
    }
}
