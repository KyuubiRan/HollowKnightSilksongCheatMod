using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class ActionFeature : FeatureBase
{
    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> EnableInfinityJump = CfgManager.Create("PlayerAction::EnableInfinityJump", false);
    public readonly ConfigObject<bool> EnableNoAttackCd = CfgManager.Create("PlayerAction::EnableNoAttackCd", false);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Actions");
        EnableInfinityJump.Value = GUILayout.Toggle(EnableInfinityJump, "Infinity Air Jump");
        EnableNoAttackCd.Value = GUILayout.Toggle(EnableNoAttackCd, "No Attack Cooldown");
        UiUtils.EndCategory();
    }
}