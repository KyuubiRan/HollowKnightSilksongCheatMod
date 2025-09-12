using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Currency;

public class ShellShardFeature : FeatureBase
{
    private static HeroController Hc => HeroController.instance;
    public override ModPage Page => ModPage.Currency;

    private int _shellShardsValue = 100;
    private string _shellShardsValueStr = "100";
    public readonly ConfigObject<bool> EnableAutoCollect = CfgManager.Create("ShellShard::EnableAutoCollect", false);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Shell Shards");

        EnableAutoCollect.Value = GUILayout.Toggle(EnableAutoCollect, "Auto Collect");
        _shellShardsValueStr = UiUtils.InputInt(ref _shellShardsValue, _shellShardsValueStr, "Value");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
            Hc?.AddShards(_shellShardsValue);
        if (GUILayout.Button("Remove"))
            Hc?.TakeShards(_shellShardsValue);
        GUILayout.EndHorizontal();

        UiUtils.EndCategory();
    }
}