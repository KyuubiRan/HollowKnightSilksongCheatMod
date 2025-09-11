using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Game;

public class FpsLimiterFeature : FeatureBase
{
    public override ModPage Page => ModPage.Game;

    public bool EnableFpsLimit { private set; get; }
    public int FpsLimit { private set; get; } = 60;
    public bool Unlimited { private set; get; }

    private int? _lastFpsLimit;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("FPS Limiter");
        EnableFpsLimit = GUILayout.Toggle(EnableFpsLimit, "Enable FPS Limiter");
        if (EnableFpsLimit && !Unlimited) FpsLimit = UiUtils.SliderInt(FpsLimit, 30, 360, 10);
        if (EnableFpsLimit) Unlimited = GUILayout.Toggle(Unlimited, "Unlimited FPS");
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (EnableFpsLimit)
        {
            _lastFpsLimit ??= Application.targetFrameRate;

            if (Unlimited)
                Application.targetFrameRate = -1;
            else
                Application.targetFrameRate = FpsLimit;
        }
        else
        {
            if (_lastFpsLimit == null) return;

            Application.targetFrameRate = _lastFpsLimit.Value;
            _lastFpsLimit = null;
        }
    }
}