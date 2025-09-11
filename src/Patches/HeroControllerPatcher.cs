using HarmonyLib;
using HKSC.Accessor;
using HKSC.Features;
using HKSC.Features.Player;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(HeroController))]
public class HeroControllerPatcher
{
    private static readonly HealthFeature HealthFeature = FeatureManager.GetFeature<HealthFeature>();
    private static readonly ActionFeature ActionFeature = FeatureManager.GetFeature<ActionFeature>();
    
    
    [HarmonyPatch("CanInfiniteAirJump")]
    [HarmonyPrefix]
    static bool CanInfiniteAirJump_Prefix(ref bool __result)
    {
        if (ActionFeature.EnableInfinityJump)
        {
            __result = true;
            return false;
        }

        return true;
    }
    
    [HarmonyPatch("CanAttack")]
    [HarmonyPrefix]
    static void CanAttack_Prefix(HeroController __instance)
    {
        if (ActionFeature.EnableNoAttackCd)
        {
            HeroControllerAccessor.AttackCdField(__instance) = 0f;
        }
    }
}