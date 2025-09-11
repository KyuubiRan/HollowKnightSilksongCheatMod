using HarmonyLib;
using HKSC.Accessor;
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

    [HarmonyPatch("OnStart")]
    [HarmonyPostfix]
    static void OnStart_Postfix(HealthManager __instance)
    {
        // Ignore 1 hp entities
        if (HealthManagerAccessor.InitHpField(__instance) <= 1)
            return;

        var enemyInfo = new EnemyManager.EnemyInfo
        {
            GameObject = __instance.gameObject,
            HealthManager = __instance
        };

        // Debug.Log("Added enemy: " + __instance.gameObject.name);
        EnemyManager.Enemies.Add(enemyInfo);
    }
}