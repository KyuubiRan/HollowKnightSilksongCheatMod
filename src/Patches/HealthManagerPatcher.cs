using HarmonyLib;
using HKSC.Features.Player;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(HealthManager))]
public class HealthManagerPatcher
{
    private static readonly DamageFeature DamageFeature = FeatureManager.GetFeature<DamageFeature>();

    [HarmonyPatch("TakeDamage")]
    [HarmonyPrefix]
    static bool TakeDamage_Prefix(HealthManager __instance, ref HitInstance hitInstance)
    {
        // Filter damage from hero
        if (!hitInstance.IsHeroDamage)
            return true;

        if (DamageFeature.EnableOneHitKill)
        {
            hitInstance.DamageDealt = 99999;
            return true;
        }

        if (DamageFeature.EnableMultiDamage)
        {
            hitInstance.DamageDealt = (int)(hitInstance.DamageDealt * DamageFeature.MultiDamageValue);
            return true;
        }

        return true;
    }
}