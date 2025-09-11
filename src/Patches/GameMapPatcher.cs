using HarmonyLib;
using HKSC.Accessor;
using HKSC.Features.Menu;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(GameMap))]
public class GameMapPatcher
{
    private static readonly MapFeature MapFeature = FeatureManager.GetFeature<MapFeature>();

    [HarmonyPatch("Update")]
    [HarmonyPrefix]
    public static void UpdatePrefix(GameMap __instance)
    {
        if (MapFeature.EnableAlwaysShowPosition)
        {
            GameMapAccessor.DisplayingCompassField(__instance) = true;
        }
    }
}