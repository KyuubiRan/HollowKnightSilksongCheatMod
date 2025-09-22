using HarmonyLib;
using HKSC.Features.Game;
using HKSC.Managers;
using UnityEngine;

namespace HKSC.Patches;

[HarmonyPatch(typeof(TimeManager))]
public class TimeManagerPatcher
{
    private static readonly TimeScaleFeature Feature = FeatureManager.GetFeature<TimeScaleFeature>();

    [HarmonyPatch("get_TimeScale")]
    [HarmonyPostfix]
    static void GetTimeScale_Prefix(ref float __result)
    {
        if (!Feature.EnableTimeScale)
            return;

        if (Mathf.Approximately(__result, 0f))
            return;

        __result = Feature.TimeScale;
    }
}