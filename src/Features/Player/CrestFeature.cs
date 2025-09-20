using GlobalSettings;
using HKSC.Accessor;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class CrestFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;

    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> EnableForceReaperMode = CfgManager.Create("PlayerCrest::ForceReaperMode", false)
        .CreateToggleHotkey("hotkey.namespace.crest", "hotkey.crest.toggleForceReaperMode");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.crest.title".Translate());
        EnableForceReaperMode.Value = GUILayout.Toggle(EnableForceReaperMode, "feature.player.crest.forceReaperMode".Translate());
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (!Hc) return;

        if (EnableForceReaperMode && Gameplay.ReaperCrest.IsEquipped)
        {
            HeroControllerAccessor.ReaperStateField(Hc) = new HeroController.ReaperCrestStateInfo
            {
                IsInReaperMode = true,
                ReaperModeDurationLeft = 1f
            };
        }
    }
}