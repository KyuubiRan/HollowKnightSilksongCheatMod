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

    public readonly ConfigObject<bool> EnableTimeScale = CfgManager.Create("TimeScale::Enable", false).CreateToggleHotkey("hotkey.namespace.game", "hotkey.game.toggleTimeScale");
    public readonly ConfigObject<float> TimeScale = CfgManager.Create("TimeScale::TimeScale", 1f);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.game.speed.title".Translate());
        EnableTimeScale.Value = GUILayout.Toggle(EnableTimeScale, "feature.generic.enable".Translate());
        if (EnableTimeScale) TimeScale.Value = UiUtils.Slider(TimeScale, 0f, 5f, 0.1f, "feature.game.speed.format".Translate());
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        Time.timeScale = EnableTimeScale ? TimeScale : 1f;
    }
}