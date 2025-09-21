using HarmonyLib;
using HKSC.Features.Game;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(DarknessRegion))]
public class DarknessRegionPatcher
{
    private static readonly WorldFeature WorldFeature = FeatureManager.GetFeature<WorldFeature>();

    public static int LastDarknessLevel { get; private set; }

    [HarmonyPrefix]
    [HarmonyPatch("SetDarknessLevel")]
    static bool SetDarknessLevel_Prefix(DarknessRegion __instance, ref int darknessLevel)
    {
        LastDarknessLevel = darknessLevel;

        if (!WorldFeature.EnableFullBrightInDarknessRegion)
            return true;

        darknessLevel = 0;
        return false;
    }
}