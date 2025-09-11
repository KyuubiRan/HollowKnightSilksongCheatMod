using HarmonyLib;
using HKSC.Accessor;
using HKSC.Features.Currency;
using HKSC.Managers;

namespace HKSC.Patches;

[HarmonyPatch(typeof(CurrencyObjectBase))]
public class CurrencyObjectBasePatch
{
    private static readonly GeoFeature GeoFeature = FeatureManager.GetFeature<GeoFeature>();
    private static readonly ShellShardFeature ShellShardFeature = FeatureManager.GetFeature<ShellShardFeature>();

    [HarmonyPatch("OnFixedUpdate")]
    [HarmonyPrefix]
    public static void GetCurrencyValue_Postfix(CurrencyObjectBase __instance)
    {
        if (GeoFeature.EnableAutoCollect && (CurrencyType?)CurrencyObjectBaseAccessor.CurrencyTypeGetter.Invoke(__instance, []) == CurrencyType.Money ||
            ShellShardFeature.EnableAutoCollect && (CurrencyType?)CurrencyObjectBaseAccessor.CurrencyTypeGetter.Invoke(__instance, []) == CurrencyType.Shard)
        {
            __instance.DoCollect();
        }
    }
}