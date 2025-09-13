using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class SilkFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> EnableLockMaxSilk = CfgManager.Create("PlayerSilk::EnableLockMaxSilk", false)
        .CreateToggleHotkey("hotkey.namespace.silk", "hotkey.silk.toggleLockMaxSilk");

    private readonly Hotkey _refillHotkey =
        Hotkey.Create("PlayerSilk::SetToMax", "hotkey.namespace.silk", "hotkey.silk.refill", KeyCode.None, down =>
        {
            if (down) Hc?.RefillSilkToMax();
        });

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.silk.title".Translate());
        EnableLockMaxSilk.Value = GUILayout.Toggle(EnableLockMaxSilk, "feature.player.silk.lockMaxSilk".Translate());
        if (GUILayout.Button("feature.player.silk.refill".Translate()))
            Hc?.RefillSilkToMax();
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (Hc == null)
            return;

        if (EnableLockMaxSilk && Hc.playerData.CurrentSilkMax != Hc.playerData.silk)
            Hc.RefillSilkToMaxSilent();
    }
}