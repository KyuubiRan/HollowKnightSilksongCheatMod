using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class SilkFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    private static GameManager Gm => GameManager.UnsafeInstance;
    public override ModPage Page => ModPage.Player;
    
    public bool EnableLockMaxSilk { private set; get; }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Silk");
        EnableLockMaxSilk = GUILayout.Toggle(EnableLockMaxSilk, "Lock Max Silk");
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