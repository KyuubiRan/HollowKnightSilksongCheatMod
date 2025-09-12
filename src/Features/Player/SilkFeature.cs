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

    public readonly ConfigObject<bool> EnableLockMaxSilk = CfgManager.Create("PlayerSilk::EnableLockMaxSilk", false).CreateToggleHotkey();

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Silk");
        EnableLockMaxSilk.Value = GUILayout.Toggle(EnableLockMaxSilk, "Lock Max Silk");
        if (GUILayout.Button("Refill To Max"))
            Hc?.RefillSilkToMax();
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (EnableLockMaxSilk && Hc.playerData.CurrentSilkMax != Hc.playerData.silk)
            Hc.RefillSilkToMaxSilent();
    }
}