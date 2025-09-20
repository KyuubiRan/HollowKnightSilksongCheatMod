using HarmonyLib;
using HKSC.Features.Inventory;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(ToolItem))]
public class ToolItemPatcher
{
    private static readonly ItemCountFeature Feature = FeatureManager.GetFeature<ItemCountFeature>();

    [HarmonyPatch("OnWasUsed")]
    [HarmonyPostfix]
    static void OnWasUsed_Postfix(ToolItem __instance)
    {
        if (!Feature.EnableAutoReplenishCountItem)
            return;
        
        ToolItemManager.TryReplenishTools(true, ToolItemManager.ReplenishMethod.QuickCraft);
    }
}