using HarmonyLib;
using HKSC.Features;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(HeroController))]
public class HeroControllerPatcher
{
    private static readonly PlayerFeatures Feature = FeatureManager.GetFeature<PlayerFeatures>();
    
    private static readonly AccessTools.FieldRef<HeroController, float> AttackCdField = AccessTools.FieldRefAccess<HeroController, float>("attack_cooldown");
    
    [HarmonyPatch("CanInfiniteAirJump")]
    [HarmonyPrefix]
    static bool CanInfiniteAirJump_Prefix(ref bool __result)
    {
        if (Feature.EnableInfinityJump)
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
        if (Feature.EnableNoAttackCd)
        {
            AttackCdField(__instance) = 0f;
        }
    }
}