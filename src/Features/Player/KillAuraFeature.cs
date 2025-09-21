using System.Linq;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace HKSC.Features.Player;

public class KillAuraFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> IsEnabled = CfgManager
        .Create("KillAura::Enable", false)
        .CreateToggleHotkey("hotkey.namespace.killAura", "hotkey.generic.toggle")
        .AddToggleToast("feature.player.killAura.title");

    public readonly ConfigObject<int> Damage = CfgManager.Create("KillAura::Damage", 10);
    public readonly ConfigObject<float> Range = CfgManager.Create("KillAura::Range", 10f);
    public readonly ConfigObject<float> AttackInterval = CfgManager.Create("KillAura::AttackInterval", 1f);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.player.killAura.title".Translate());
        IsEnabled.Value = GUILayout.Toggle(IsEnabled, "feature.generic.enable".Translate());
        if (IsEnabled)
        {
            Damage.Value = UiUtils.SliderInt(Damage, 0, 500, 5, valueFormat: "feature.player.killAura.damage.format".Translate());
            Range.Value = UiUtils.Slider(Range, 5f, 30f, .5f, valueFormat: "feature.player.killAura.range.format".Translate());
            AttackInterval.Value = UiUtils.Slider(AttackInterval, 0.1f, 5f, 0.1f, valueFormat: "feature.player.killAura.interval.format".Translate());
        }

        UiUtils.EndCategory();
    }

    private float _lastAttackTime;

    protected override void OnUpdate()
    {
        if (!IsEnabled) return;
        if (!Hc) return;

        _lastAttackTime += Time.deltaTime;
        if (_lastAttackTime < AttackInterval) return;

        var hcPos = Hc.transform.position;
        var attacked = false;
        foreach (
            var enemy in
            from enemy in HealthManager.EnumerateActiveEnemies().ToList()
            where enemy
            where !enemy.isDead
            let transform = enemy.gameObject.transform
            where transform
            where Vector2.Distance(hcPos, transform.position) <= Range
            select enemy
        )
        {
            enemy.Hit(
                new HitInstance
                {
                    Source = Hc.gameObject,
                    DamageDealt = Damage,
                    IsHeroDamage = true,
                    AttackType = AttackTypes.Generic,
                    Multiplier = 1f
                });
            attacked = true;
        }

        if (attacked) _lastAttackTime = 0;
    }
}