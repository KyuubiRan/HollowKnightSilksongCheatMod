using System;
using GlobalEnums;
using HKSC.Accessor;
using HKSC.Managers;
using HKSC.Ui;

namespace HKSC.Features.Player;

public class GodModeControl : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    private static readonly Lazy<HealthFeature> HealthFeature = FeatureManager.LazyFeature<HealthFeature>();
    private static readonly Lazy<NoclipFeature> NoclipFeature = FeatureManager.LazyFeature<NoclipFeature>();

    public override ModPage Page => ModPage.Player;

    private bool _lastFrameState;

    protected override void OnUpdate()
    {
        if (!Hc) return;

        if (HealthFeature.Value.EnableGodMode || NoclipFeature.Value.IsEnabled)
        {
            Hc.SetDamageMode(DamageMode.NO_DAMAGE);
            _lastFrameState = true;
            return;
        }

        if (_lastFrameState)
        {
            Hc.SetDamageMode(DamageMode.FULL_DAMAGE);
            _lastFrameState = false;
        }
    }
}