using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Game;

public class TimeScaleFeature : FeatureBase
{
    public override ModPage Page => ModPage.Game;

    public bool EnableTimeScale { private set; get; }
    public float TimeScale { private set; get; } = 1f;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Time Scale");
        EnableTimeScale = GUILayout.Toggle(EnableTimeScale, "Enable Time Scale");
        if (EnableTimeScale) TimeScale = UiUtils.Slider(TimeScale, 0f, 5f, 0.1f);
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        Time.timeScale = EnableTimeScale ? TimeScale : 1f;
    }
}