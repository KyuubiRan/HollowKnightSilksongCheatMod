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
    public static void Update_Prefix(GameMap __instance)
    {
        if (MapFeature.EnableAlwaysShowPosition)
        {
            GameMapAccessor.DisplayingCompassField(__instance) = true;
        }
    }

    [HarmonyPatch("HasMapForScene")]
    [HarmonyPostfix]
    public static void HasMapForScene_Postfix(GameMap __instance, string sceneName, ref bool sceneHasSprite, ref bool __result)
    {
        if (MapFeature.EnableUnlockMap && sceneHasSprite)
        {
            __result = true;
        }
    }

    [HarmonyPatch("HasAnyMapForZone")]
    [HarmonyPrefix]
    public static bool HasAnyMapForZone_Prefix(GameMap __instance, ref bool __result)
    {
        if (MapFeature.EnableUnlockMap)
        {
            __result = true;
            return false;
        }

        return true;
    }

    [HarmonyPatch("GameMap+ParentInfo, Assembly-CSharp.dll", "get_IsUnlocked")]
    [HarmonyPrefix]
    public static bool ParentInfo_GetIsUnlocked_Prefix(ref bool __result)
    {
        if (MapFeature.EnableUnlockMap)
        {
            __result = true;
            return false;
        }

        return true;
    }
}