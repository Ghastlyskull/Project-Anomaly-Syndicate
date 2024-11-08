using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ProjectAnomalySyndicate.HarmonyPatches
{
    #region GhoulTabs
    [HarmonyPatch]
    public static class WorkTabGhoulsPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(MainTabWindow_Work), "get_Pawns");
        }
        private static void Postfix(ref IEnumerable<Pawn> __result, MainTabWindow_Schedule __instance)
        {
            __result = __result.Concat(Find.CurrentMap.mapPawns.ColonyMutantsPlayerControlled.Where(c => c.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake)))));
        }
    }
    [HarmonyPatch]
    public static class AssignTabGhoulsPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(MainTabWindow_Assign), "get_Pawns");
        }
        private static void Postfix(ref IEnumerable<Pawn> __result, MainTabWindow_Assign __instance)
        {
            __result = __result.Concat(Find.CurrentMap.mapPawns.ColonyMutantsPlayerControlled.Where(c => c.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake)))));
        }
    }
    #endregion

    #region PoliciesPatches
    [HarmonyPatch]
    public static class GetCurrentOutfitPolicyPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Pawn_OutfitTracker), "get_CurrentApparelPolicy");
        }
        private static void Postfix(ref ApparelPolicy __result, Pawn_OutfitTracker __instance)
        {
            if (__instance.pawn.IsMutant && __instance.pawn.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake))))
            {
                __result = Current.Game.outfitDatabase.DefaultOutfit();

            }
        }
    }
    [HarmonyPatch]
    public static class GetCurrentDrugPolicyPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Pawn_DrugPolicyTracker), "get_CurrentPolicy");
        }
        private static void Postfix(ref DrugPolicy __result, Pawn_DrugPolicyTracker __instance)
        {
            if (__instance.pawn.IsMutant && __instance.pawn.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake))))
            {
                __result = Current.Game.drugPolicyDatabase.DefaultDrugPolicy();
            }
        }
    }
    [HarmonyPatch]
    public static class GetCurrentFoodPolicyPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Pawn_FoodRestrictionTracker), "get_CurrentFoodPolicy");
        }
        private static void Postfix(ref FoodPolicy __result, Pawn_FoodRestrictionTracker __instance)
        {
            if (__instance.pawn.IsMutant && __instance.pawn.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake))))
            {
                __result = Current.Game.foodRestrictionDatabase.DefaultFoodRestriction();
            }
        }
    }
    [HarmonyPatch]
    public static class GetCurrentReadingPolicyPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Pawn_ReadingTracker), "get_CurrentPolicy");
        }
        private static void Postfix(ref ReadingPolicy __result, Pawn_ReadingTracker __instance)
        {
            if (__instance.pawn.IsMutant && __instance.pawn.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake))))
            {
                __result = Current.Game.readingPolicyDatabase.DefaultReadingPolicy();

            }

        }
    }
    #endregion

    #region OrderPatch
    [HarmonyPatch]
    public static class FloatMenuMakerGetOptionsWorkGhoulPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(FloatMenuMakerMap), "AddMutantOrders");
        }
        private static bool Prefix(Vector3 clickPos, Pawn pawn, List<FloatMenuOption> opts)
        {
            if (pawn.IsMutant && pawn.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake))))
            {
                AccessTools.Method(typeof(FloatMenuMakerMap), "AddHumanlikeOrders").Invoke(null, new object[] { clickPos, pawn, opts });
                return false;
            }
            return true;


        }
    }
    #endregion

    #region GearTabPatches
    [HarmonyPatch]
    public static class ITab_Pawn_Gear_CanControlColonistPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(ITab_Pawn_Gear), "get_CanControlColonist");
        }
        private static void Postfix(ITab_Pawn_Gear __instance, ref bool __result)
        {
           
            if (__instance.SelPawnForGear != null && __instance.SelPawnForGear.Spawned && __instance.SelPawnForGear.Faction.IsPlayer && __instance.SelPawnForGear.RaceProps.Humanlike && __instance.SelPawnForGear.IsMutant && __instance.SelPawnForGear.health.hediffSet.hediffs.Any(x => x.def.HasComp(typeof(CompMindWake))))
            {
                __result = true;
            }


        }    
    }
    #endregion


}
