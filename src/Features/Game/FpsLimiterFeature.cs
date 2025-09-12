using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Game;

public class FpsLimiterFeature : FeatureBase
{
    public override ModPage Page => ModPage.Game;

    public readonly ConfigObject<bool> EnableFpsLimit = CfgManager.Create("FpsLimiter::Enable", false).CreateToggleHotkey("Toggle Enable");
    public readonly ConfigObject<int> FpsLimit = CfgManager.Create("FpsLimiter::FpsLimit", 60);
    public readonly ConfigObject<bool> Unlimited = CfgManager.Create("FpsLimiter::Unlimited", false);

    private int? _lastFpsLimit;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("FPS Limiter");
        EnableFpsLimit.Value = GUILayout.Toggle(EnableFpsLimit, "Enable FPS Limiter");
        if (EnableFpsLimit && !Unlimited) FpsLimit.Value = UiUtils.SliderInt(FpsLimit, 30, 360, 10);
        if (EnableFpsLimit) Unlimited.Value = GUILayout.Toggle(Unlimited, "Unlimited FPS");
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