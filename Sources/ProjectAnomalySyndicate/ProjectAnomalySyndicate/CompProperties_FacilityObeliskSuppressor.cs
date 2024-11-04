using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAnomalySyndicate
{
    public class CompProperties_FacilityObeliskSuppressor : CompProperties_Facility
    {
        public float activityDecreasePerTick = 1f;
        public CompProperties_FacilityObeliskSuppressor()
        {
            compClass = typeof(CompFacilityObeliskSuppressor);
        }


    }
}
