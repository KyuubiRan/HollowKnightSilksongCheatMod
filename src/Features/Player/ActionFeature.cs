using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class ActionFeature : FeatureBase
{
    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> EnableInfinityJump = CfgManager.Create("PlayerAction::EnableInfinityJump", false)
        .CreateToggleHotkey("Toggle Infinity Jump");

    public readonly ConfigObject<bool> EnableNoAttackCd = CfgManager.Create("PlayerAction::EnableNoAttackCd", false)
        .CreateToggleHotkey("Toggle No Attack Cooldown");

    public readonly ConfigObject<bool> EnableNoDashCd = CfgManager.Create("PlayerAction::EnableNoDashCd", false)
        .CreateToggleHotkey("Toggle No Dash Cooldown");

    public readonly ConfigObject<bool> EnableCanInfinityDashOnAir = CfgManager.Create("PlayerAction::EnableCanInfinityDashOnAir", false)
        .CreateToggleHotkey("Toggle Can Infinity Dash On Air");


    protected override void OnGui()
    {
        UiUtils.BeginCategory("Actions");
        EnableInfinityJump.Value = GUILayout.Toggle(EnableInfinityJump, "Infinity Air Jump");
        EnableNoAttackCd.Value = GUILayout.Toggle(EnableNoAttackCd, "No Attack Cooldown");
        EnableNoDashCd.Value = GUILayout.Toggle(EnableNoDashCd, "No Dash Cooldown");
        EnableCanInfinityDashOnAir.Value = GUILayout.Toggle(EnableCanInfinityDashOnAir, "Can Infinity Dash On Air");
        UiUtils.EndCategory();
    }
}