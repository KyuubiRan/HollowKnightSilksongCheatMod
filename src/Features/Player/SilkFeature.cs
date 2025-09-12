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
        .CreateToggleHotkey("Toggle Lock Max Silk");

    private readonly Hotkey _refillHotkey =
        Hotkey.Create("PlayerSilk::SetToMax", "Refill", KeyCode.None, down =>
        {
            if (down) Hc?.RefillSilkToMax();
        });

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Silk");
        EnableLockMaxSilk.Value = GUILayout.Toggle(EnableLockMaxSilk, "Lock Max Silk");
        if (GUILayout.Button("Refill"))
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