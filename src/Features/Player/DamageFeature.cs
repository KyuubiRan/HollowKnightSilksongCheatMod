using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class DamageFeature : FeatureBase
{
    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> EnableMultiDamage = CfgManager
        .Create("PlayerDamage::EnableMultiDamage", false).CreateToggleHotkey("hotkey.namespace.damage", "hotkey.damage.toggleDamageMultiplier")
        .AddToggleToast("feature.player.damage.multiplier");

    public readonly ConfigObject<float> MultiDamageValue = CfgManager.Create("PlayerDamage::MultiDamageValue", 2f);

    public readonly ConfigObject<bool> EnableOneHitKill = CfgManager
        .Create("PlayerDamage::EnableOneHitKill", false)
        .CreateToggleHotkey("hotkey.namespace.damage", "hotkey.damage.toggleOneHitKill")
        .AddToggleToast("feature.player.damage.oneHitKill");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.damage.title".Translate());
        EnableOneHitKill.Value = GUILayout.Toggle(EnableOneHitKill, "feature.player.damage.oneHitKill".Translate());
        EnableMultiDamage.Value = GUILayout.Toggle(EnableMultiDamage, "feature.player.damage.multiplier".Translate());
        if (EnableMultiDamage)
            MultiDamageValue.Value = UiUtils.Slider(MultiDamageValue, 0.0f, 10.0f, 0.25f);

        UiUtils.EndCategory();
    }
}