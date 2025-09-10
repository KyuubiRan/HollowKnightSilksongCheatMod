using GlobalEnums;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features;

public class PlayerFeatures : FeatureBase
{
    private static HeroController Hc => HeroController.instance;
    private static GameManager Gm => GameManager.instance;
    
    public bool EnableGodMode { private set; get; }
    public bool EnableLockMaxHealth { private set; get; }
    public bool EnableInfinityJump { private set; get; }
    public bool EnableNoAttackCd { private set; get; }
    public bool EnableLockMaxSilk { private set; get; }
    public bool EnableMultiDamage { private set; get; }
    public float MultiDamageValue { private set; get; } = 2f;
    public bool EnableOneHitKill { private set; get; }

    public override ModPage Page => ModPage.Player;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Health");
        EnableGodMode = GUILayout.Toggle(EnableGodMode, "God Mode");
        EnableLockMaxHealth = GUILayout.Toggle(EnableLockMaxHealth, "Lock Max Health");
        if (GUILayout.Button("Heal"))
            Hc?.AddHealth(999);
        UiUtils.EndCategory();

        UiUtils.BeginCategory("Silk");
        EnableLockMaxSilk = GUILayout.Toggle(EnableLockMaxSilk, "Lock Max Silk");
        if (GUILayout.Button("Refill To Max"))
            Hc?.RefillSilkToMax();
        UiUtils.EndCategory();

        UiUtils.BeginCategory("Damage");
        EnableOneHitKill = GUILayout.Toggle(EnableOneHitKill, "One Hit Kill");
        EnableMultiDamage = GUILayout.Toggle(EnableMultiDamage, "Multi Damage");
        if (EnableMultiDamage)
            MultiDamageValue = UiUtils.Slider(MultiDamageValue, 0.0f, 10.0f);

        UiUtils.EndCategory();

        UiUtils.BeginCategory("Actions");
        EnableInfinityJump = GUILayout.Toggle(EnableInfinityJump, "Infinity Air Jump");
        EnableNoAttackCd = GUILayout.Toggle(EnableNoAttackCd, "No Attack Cooldown");
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (Hc == null)
            return;

        if (!Hc.exitedQuake)
            Hc.SetDamageMode(EnableGodMode ? DamageMode.NO_DAMAGE : DamageMode.FULL_DAMAGE);

        if (EnableLockMaxHealth)
            Hc.playerData.health = Hc.playerData.maxHealth;

        if (EnableLockMaxSilk && Hc.playerData.CurrentSilkMax != Hc.playerData.silk)
            Hc.RefillSilkToMaxSilent();
    }
}