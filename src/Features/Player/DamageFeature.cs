using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class DamageFeature : FeatureBase
{
    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> EnableMultiDamage = CfgManager.Create("PlayerDamage::EnableMultiDamage", false);
    public readonly ConfigObject<float> MultiDamageValue = CfgManager.Create("PlayerDamage::MultiDamageValue", 2f);
    public readonly ConfigObject<bool> EnableOneHitKill = CfgManager.Create("PlayerDamage::EnableOneHitKill", false);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Damage");
        EnableOneHitKill.Value = GUILayout.Toggle(EnableOneHitKill, "One Hit Kill");
        EnableMultiDamage.Value = GUILayout.Toggle(EnableMultiDamage, "Multi Damage");
        if (EnableMultiDamage)
            MultiDamageValue.Value = UiUtils.Slider(MultiDamageValue, 0.0f, 10.0f, 0.25f);

        UiUtils.EndCategory();
    }
}