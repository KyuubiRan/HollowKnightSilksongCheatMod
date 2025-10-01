using HarmonyLib;
using HKSC.Accessor;
using HKSC.Features.Inventory;
using HKSC.Features.Player;
using HKSC.Features.Teleport;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(HeroController))]
public class HeroControllerPatcher
{
    private static readonly ActionFeature ActionFeature = FeatureManager.GetFeature<ActionFeature>();
    private static readonly DeathTeleport DeathTeleport = FeatureManager.GetFeature<DeathTeleport>();
    private static readonly ItemCountFeature ItemCountFeature = FeatureManager.GetFeature<ItemCountFeature>();
    private static readonly HealthFeature HealthFeature = FeatureManager.GetFeature<HealthFeature>();

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
            HeroControllerAccessor.AttackCdField(__instance) = 0f;
    }

    [HarmonyPatch("CanDash")]
    [HarmonyPrefix]
    static void CanDash_Prefix(HeroController __instance)
    {
        if (ActionFeature.EnableNoDashCd)
            HeroControllerAccessor.DashCooldownTimerField(__instance) = 0f;

        if (ActionFeature.EnableCanInfinityDashOnAir)
            HeroControllerAccessor.AirDashedField(__instance) = false;
    }

    [HarmonyPatch("HeroDash")]
    [HarmonyPostfix]
    static void HeroDash_Postfix(HeroController __instance)
    {
        if (!HealthFeature.EnableGodModeOnDash)
            return;

        HeroControllerAccessor.StartInvulnerableMethod.Invoke(__instance, [HeroControllerAccessor.DashTimerField(__instance) + 0.25f]);
    }

    [HarmonyPatch("CanHarpoonDash")]
    [HarmonyPrefix]
    static void CanHarpoonDash_Prefix(HeroController __instance)
    {
        if (ActionFeature.EnableNoDashCd)
            HeroControllerAccessor.HarpoonDashCooldownField(__instance) = 0f;
    }

    [HarmonyPatch("Awake")]
    [HarmonyPostfix]
    static void Awake_Postfix(HeroController __instance)
    {
        __instance.OnDeath += () => { DeathTeleport.LogTeleport(TeleportPoint.Current); };
    }

    [HarmonyPatch("ThrowTool")]
    [HarmonyPostfix]
    static void ThrowTool_Postfix(HeroController __instance)
    {
        if (!ItemCountFeature.EnableAutoReplenishCountItem)
            return;

        ToolItemManager.TryReplenishTools(true, ToolItemManager.ReplenishMethod.QuickCraft);
    }
}