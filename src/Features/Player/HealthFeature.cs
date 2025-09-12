using GlobalEnums;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class HealthFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;

    public readonly ConfigObject<bool> EnableGodMode =
        CfgManager.Create("PlayerHealth::EnableGodMode", false).CreateToggleHotkey("Toggle God Mode");

    public readonly ConfigObject<bool> EnableLockMaxHealth =
        CfgManager.Create("PlayerHealth::EnableLockMaxHealth", false).CreateToggleHotkey("Toggle Lock Max Health");

    public override ModPage Page => ModPage.Player;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Health");
        EnableGodMode.Value = GUILayout.Toggle(EnableGodMode, "God Mode");
        EnableLockMaxHealth.Value = GUILayout.Toggle(EnableLockMaxHealth, "Lock Max Health");
        if (GUILayout.Button("Heal"))
            Hc?.RefillHealthToMax();
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (Hc == null)
            return;

        if (!Hc.exitedQuake)
            Hc.SetDamageMode(EnableGodMode ? DamageMode.NO_DAMAGE : DamageMode.FULL_DAMAGE);

        if (EnableLockMaxHealth || EnableGodMode)
            Hc.playerData.health = Hc.playerData.maxHealth;
    }
}