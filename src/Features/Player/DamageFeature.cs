using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class DamageFeature : FeatureBase
{
    public override ModPage Page => ModPage.Player;

    public bool EnableMultiDamage { private set; get; }
    public float MultiDamageValue { private set; get; } = 2f;
    public bool EnableOneHitKill { private set; get; }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Damage");
        EnableOneHitKill = GUILayout.Toggle(EnableOneHitKill, "One Hit Kill");
        EnableMultiDamage = GUILayout.Toggle(EnableMultiDamage, "Multi Damage");
        if (EnableMultiDamage)
            MultiDamageValue = UiUtils.Slider(MultiDamageValue, 0.0f, 10.0f, 0.25f);

        UiUtils.EndCategory();
    }
}