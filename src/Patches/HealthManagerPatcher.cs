using HarmonyLib;
using HKSC.Features;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(HealthManager))]
public class HealthManagerPatcher
{
    private static readonly PlayerFeatures Feature = FeatureManager.GetFeature<PlayerFeatures>();

    [HarmonyPatch("TakeDamage")]
    [HarmonyPrefix]
    static bool TakeDamage_Prefix(HealthManager __instance, ref HitInstance hitInstance)
    {
        // Filter damage from hero
        if (!hitInstance.IsHeroDamage)
            return true;

        if (Feature.EnableOneHitKill)
        {
            hitInstance.DamageDealt = 99999;
            return true;
        }

        if (Feature.EnableMultiDamage)
        {
            hitInstance.DamageDealt = (int)(hitInstance.DamageDealt * Feature.MultiDamageValue);
            return true;
        }

        return true;
    }
}