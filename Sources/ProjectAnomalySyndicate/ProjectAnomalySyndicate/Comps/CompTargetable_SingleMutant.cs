using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ProjectAnomalySyndicate
{
    public class CompTargetable_SingleMutant : CompTargetable
    {
        protected override bool PlayerChoosesTarget => true;

        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetSubhumans = true,
                canTargetBuildings = false
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }
    }
}
