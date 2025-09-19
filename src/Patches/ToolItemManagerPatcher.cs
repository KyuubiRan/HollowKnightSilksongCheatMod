using HarmonyLib;
using HKSC.Features.Inventory;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(ToolItemManager))]
public class ToolItemManagerPatcher
{
    private static readonly ItemCountFeature Feature = FeatureManager.GetFeature<ItemCountFeature>();

    [HarmonyPrefix]
    [HarmonyPatch("get_IsInfiniteToolUseEnabled")]
    public static bool GetIsInfiniteToolUseEnabled_Prefix(ref bool __result)
    {
        if (!Feature.EnableLockMaxItemUse)
            return true;

        __result = true;
        return false;
    }
}