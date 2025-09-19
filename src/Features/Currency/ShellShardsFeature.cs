using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Currency;

public class ShellShardFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    public override ModPage Page => ModPage.Currency;

    private int _shellShardsValue = 100;
    private string _shellShardsValueStr = "100";

    public readonly ConfigObject<bool> EnableAutoCollect = CfgManager.Create("ShellShard::EnableAutoCollect", false)
                                                                     .CreateToggleHotkey("hotkey.namespace.currency", "hotkey.currency.toggleAutoCollectShards");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.currency.shards.title".Translate());

        EnableAutoCollect.Value = GUILayout.Toggle(EnableAutoCollect, "feature.currency.autoCollect".Translate());
        _shellShardsValueStr = UiUtils.InputInt(ref _shellShardsValue, _shellShardsValueStr, "feature.generic.value".Translate());

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("feature.generic.add".Translate()))
            Hc?.AddShards(_shellShardsValue);
        if (GUILayout.Button("feature.generic.reduce".Translate()))
            Hc?.TakeShards(_shellShardsValue);
        GUILayout.EndHorizontal();

        UiUtils.EndCategory();
    }
}