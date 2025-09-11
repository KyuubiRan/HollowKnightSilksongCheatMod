using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class ActionFeature : FeatureBase
{
    public override ModPage Page => ModPage.Player;
    
    public bool EnableInfinityJump { private set; get; }
    public bool EnableNoAttackCd { private set; get; }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Actions");
        EnableInfinityJump = GUILayout.Toggle(EnableInfinityJump, "Infinity Air Jump");
        EnableNoAttackCd = GUILayout.Toggle(EnableNoAttackCd, "No Attack Cooldown");
        UiUtils.EndCategory();
    }
}