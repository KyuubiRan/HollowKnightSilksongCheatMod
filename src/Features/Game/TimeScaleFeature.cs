using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Game;

public class TimeScaleFeature : FeatureBase
{
    public override ModPage Page => ModPage.Game;

    public readonly ConfigObject<bool> EnableTimeScale = CfgManager.Create("TimeScale::Enable", false).CreateToggleHotkey();
    public readonly ConfigObject<float> TimeScale = CfgManager.Create("TimeScale::TimeScale", 1f);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Time Scale");
        EnableTimeScale.Value = GUILayout.Toggle(EnableTimeScale, "Enable Time Scale");
        if (EnableTimeScale) TimeScale.Value = UiUtils.Slider(TimeScale, 0f, 5f, 0.1f);
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        Time.timeScale = EnableTimeScale ? TimeScale : 1f;
    }
}