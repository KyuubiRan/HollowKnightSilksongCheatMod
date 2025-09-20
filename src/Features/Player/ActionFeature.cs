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

    public readonly ConfigObject<bool> EnableInfinityJump = CfgManager
        .Create("PlayerAction::EnableInfinityJump", false)
        .CreateToggleHotkey("hotkey.namespace.action", "hotkey.action.toggleInfinityAirJump")
        .AddToggleToast("feature.player.action.infinityAirJump");

    public readonly ConfigObject<bool> EnableNoAttackCd = CfgManager
        .Create("PlayerAction::EnableNoAttackCd", false)
        .CreateToggleHotkey("hotkey.namespace.action", "hotkey.action.toggleNoAttackCd")
        .AddToggleToast("feature.player.action.noAttackCd");

    public readonly ConfigObject<bool> EnableNoDashCd = CfgManager
        .Create("PlayerAction::EnableNoDashCd", false)
        .CreateToggleHotkey("hotkey.namespace.action", "hotkey.action.toggleNoDashCd")
        .AddToggleToast("feature.player.action.noDashCd");

    public readonly ConfigObject<bool> EnableCanInfinityDashOnAir = CfgManager
        .Create("PlayerAction::EnableCanInfinityDashOnAir", false)
        .CreateToggleHotkey("hotkey.namespace.action", "hotkey.action.toggleInfinityAirDash")
        .AddToggleToast("feature.player.action.infinityAirDash");


    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.action.title".Translate());
        EnableInfinityJump.Value = GUILayout.Toggle(EnableInfinityJump, "feature.player.action.infinityAirJump".Translate());
        EnableNoAttackCd.Value = GUILayout.Toggle(EnableNoAttackCd, "feature.player.action.noAttackCd".Translate());
        EnableNoDashCd.Value = GUILayout.Toggle(EnableNoDashCd, "feature.player.action.noDashCd".Translate());
        EnableCanInfinityDashOnAir.Value = GUILayout.Toggle(EnableCanInfinityDashOnAir, "feature.player.action.infinityAirDash".Translate());
        UiUtils.EndCategory();
    }
}