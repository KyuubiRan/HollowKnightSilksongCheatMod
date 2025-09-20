using HarmonyLib;
using HKSC.Features.Game;
using HKSC.Managers;
using UnityEngine;

namespace HKSC.Patches;

[HarmonyPatch(typeof(TimeManager))]
public class TimeManagerPatcher
{
    private static readonly TimeScaleFeature Feature = FeatureManager.GetFeature<TimeScaleFeature>();
    private static float _lastTimeScale = 1f;

    [HarmonyPatch("set_TimeScale")]
    [HarmonyPrefix]
    static void SetTimeScale_Prefix(ref float value)
    {
        _lastTimeScale = value;
    }

    [HarmonyPatch("get_TimeScale")]
    [HarmonyPrefix]
    static bool GetTimeScale_Prefix(ref float __result)
    {
        if (!Feature.EnableTimeScale)
            return true;

        if (!Mathf.Approximately(_lastTimeScale, 1f)) return true;
        __result = Feature.TimeScale;
        return false;
    }
}