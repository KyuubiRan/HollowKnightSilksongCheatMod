using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Currency;

public class ShellShardsFeature : FeatureBase
{
    private static HeroController Hc => HeroController.instance;
    public override ModPage Page => ModPage.Currency;

    private int _shellShardsValue = 100;
    private string _shellShardsValueStr = "100";

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Shell Shards");

        _shellShardsValueStr = UiUtils.InputInt(ref _shellShardsValue, _shellShardsValueStr,"Value");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
            Hc?.AddShards(_shellShardsValue);
        if (GUILayout.Button("Remove"))
            Hc?.TakeShards(_shellShardsValue);
        GUILayout.EndHorizontal();

        UiUtils.EndCategory();        
    }
}