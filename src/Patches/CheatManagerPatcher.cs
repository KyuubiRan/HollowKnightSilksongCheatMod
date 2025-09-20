using System;
using HarmonyLib;
using HKSC.Features.Player;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(CheatManager))]
public class CheatManagerPatcher
{
    private static readonly Lazy<HealthFeature> HealthFeature = FeatureManager.LazyFeature<HealthFeature>();
    private static readonly Lazy<NoclipFeature> NoclipFeature = FeatureManager.LazyFeature<NoclipFeature>();

    [HarmonyPatch("get_IsCheatsEnabled")]
    [HarmonyPrefix]
    static bool GetIsCheatsEnabled_Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }

    [HarmonyPatch("get_Invincibility")]
    [HarmonyPrefix]
    static bool GetInvincibility_Prefix(ref CheatManager.InvincibilityStates __result)
    {
        if (!(HealthFeature.Value.EnableGodMode || NoclipFeature.Value.IsEnabled))
            return true;

        __result = CheatManager.InvincibilityStates.FullInvincible;
        return false;
    }
}