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
        CfgManager.Create("PlayerHealth::EnableGodMode", false).CreateToggleHotkey("hotkey.namespace.health", "hotkey.health.toggleGodMode");

    public readonly ConfigObject<bool> EnableLockMaxHealth =
        CfgManager.Create("PlayerHealth::EnableLockMaxHealth", false).CreateToggleHotkey("hotkey.namespace.health", "hotkey.health.toggleLockMaxHealth");

    private readonly Hotkey _healHotkey =
        Hotkey.Create("PlayerHealth::Heal", "hotkey.namespace.health", "hotkey.health.heal", KeyCode.None, down =>
        {
            if (down) Hc?.RefillHealthToMax();
        });

    public override ModPage Page => ModPage.Player;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.health.title".Translate());
        EnableGodMode.Value = GUILayout.Toggle(EnableGodMode, "feature.player.health.godMode".Translate());
        EnableLockMaxHealth.Value = GUILayout.Toggle(EnableLockMaxHealth, "feature.player.health.lockMaxHealth".Translate());
        if (GUILayout.Button("feature.player.health.heal".Translate()))
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