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

    public readonly ConfigObject<bool> EnableGodMode = CfgManager
        .Create("PlayerHealth::EnableGodMode", false)
        .CreateToggleHotkey("hotkey.namespace.health", "hotkey.health.toggleGodMode")
        .AddToggleToast("feature.player.health.godMode");

    public readonly ConfigObject<bool> EnableGodModeOnDash = CfgManager
        .Create("PlayerHealth::EnableGodModeOnDash", false)
        .CreateToggleHotkey("hotkey.namespace.health", "hotkey.health.toggleGodModeOnDash")
        .AddToggleToast("feature.player.health.godModeOnDash");

    public readonly ConfigObject<bool> EnableLockMaxHealth = CfgManager
        .Create("PlayerHealth::EnableLockMaxHealth", false)
        .CreateToggleHotkey("hotkey.namespace.health", "hotkey.health.toggleLockMaxHealth")
        .AddToggleToast("feature.player.health.lockMaxHealth");

    public readonly ConfigObject<bool> EnableLockBlueHealthMode = CfgManager
        .Create("PlayerHealth::EnableLockBlueHealthState", false)
        .CreateToggleHotkey("hotkey.namespace.health", "hotkey.health.toggleLockBlueHealthState")
        .AddToggleToast("feature.player.health.lockBlueHealthState");

    private readonly Hotkey _healHotkey =
        Hotkey.Create("PlayerHealth::Heal", "hotkey.namespace.health", "hotkey.health.heal", KeyCode.None, down =>
        {
            if (down) Hc?.MaxHealth();
        });

    private readonly Hotkey _enterBlueHealthStateHotkey =
        Hotkey.Create("PlayerHealth::EnterBlueHealthState", "hotkey.namespace.health", "hotkey.health.enterBlueHealthState", KeyCode.None, down =>
        {
            if (down) Hc?.HitMaxBlueHealth();
        });

    public override ModPage Page => ModPage.Player;

    private static void EnterBlueHealthState()
    {
        if (!Hc) return;

        Hc.HitMaxBlueHealth();
        Hc.HitMaxBlueHealthBurst();
    }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.health.title".Translate());
        EnableGodMode.Value = GUILayout.Toggle(EnableGodMode, "feature.player.health.godMode".Translate());
        EnableGodModeOnDash.Value = GUILayout.Toggle(EnableGodModeOnDash, "feature.player.health.godModeOnDash".Translate());

        EnableLockMaxHealth.Value = GUILayout.Toggle(EnableLockMaxHealth, "feature.player.health.lockMaxHealth".Translate());
        if (GUILayout.Button("feature.player.health.heal".Translate()))
            Hc?.MaxHealth();

        EnableLockBlueHealthMode.Value = GUILayout.Toggle(EnableLockBlueHealthMode, "feature.player.health.lockBlueHealthState".Translate());
        if (GUILayout.Button("feature.player.health.enterBlueHealthState".Translate()))
            EnterBlueHealthState();

        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (!Hc)
            return;

        if (EnableLockMaxHealth || EnableGodMode)
            Hc.playerData.health = Hc.playerData.maxHealth;

        if (EnableLockBlueHealthMode && !Hc.IsInLifebloodState && !Hc.playerData.atBench)
            EnterBlueHealthState();
    }
}