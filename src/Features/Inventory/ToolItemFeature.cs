using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Inventory;

public class ToolItemFeature : FeatureBase
{
    public override ModPage Page => ModPage.Inventory;

    public ConfigObject<bool> EnableCustomLuckyDiceTriggerRate = CfgManager
        .Create("Tool::EnableCustomLuckyDiceTriggerRate", false)
        .CreateToggleHotkey("hotkey.namespace.inventory.tool", "hotkey.inventory.tool.toggleCustomLuckyDiceTriggerRate")
        .AddToggleToast("feature.inventory.tool.customLuckyDiceTriggerRate");

    public ConfigObject<int> LuckyDiceTriggerRate = CfgManager
        .Create("Tool::LuckyDiceTriggerRate", 10);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.inventory.tool.title".Translate());
        EnableCustomLuckyDiceTriggerRate.Value =
            GUILayout.Toggle(EnableCustomLuckyDiceTriggerRate, "feature.inventory.tool.customLuckyDiceTriggerRate".Translate());
        if (EnableCustomLuckyDiceTriggerRate)
            LuckyDiceTriggerRate.Value = UiUtils.SliderInt(LuckyDiceTriggerRate, 0, 100,
                valueFormat: "{0:D}%");
        UiUtils.EndCategory();
    }
}