using HarmonyLib;
using HKSC.Features;
using HKSC.Managers;
using UnityEngine;

namespace HKSC.Patches;

[HarmonyPatch(typeof(HealthManager))]
public class HealthManagerPatcher
{
    private static readonly PlayerFeatures Feature = FeatureManager.GetFeature<PlayerFeatures>();

    private static readonly AccessTools.FieldRef<HealthManager, int> InitHpField =
        AccessTools.FieldRefAccess<HealthManager, int>("initHp");


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

    [HarmonyPatch("OnStart")]
    [HarmonyPostfix]
    static void OnStart_Postfix(HealthManager __instance)
    {
        // Ignore 1 hp enetites
        if (InitHpField(__instance) <= 1)
            return;

        var enemyInfo = new EnemyManager.EnemyInfo
        {
            GameObject = __instance.gameObject,
            HealthManager = __instance
        };
        
        Debug.Log("Added enemy: " + __instance.gameObject.name);
        EnemyManager.Enemies.Add(enemyInfo);
    }
}