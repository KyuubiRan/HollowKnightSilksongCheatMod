using System.Linq;
using HKSC.Accessor;
using HKSC.Managers;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class KillAuraFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    public override ModPage Page => ModPage.Player;
    public bool IsEnabled { get; set; }
    public int Damage { get; set; } = 10;
    public float Range { get; set; } = 8f;
    public float AttackInterval { get; set; } = 1f;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Kill Aura");
        IsEnabled = GUILayout.Toggle(IsEnabled, "Enable");
        if (IsEnabled)
        {
            Damage = UiUtils.SliderInt(Damage, 1, 1000, 10, valueFormat: "Damage: {0:0}");
            Range = UiUtils.Slider(Range, 3f, 30f, .5f, valueFormat: "Range: {0:0.0}");
            AttackInterval = UiUtils.Slider(AttackInterval, 0.1f, 5f, 0.1f, valueFormat: "Attack Interval: {0:0.0}");
        }

        UiUtils.EndCategory();
    }

    private float _lastAttackTime;

    protected override void OnUpdate()
    {
        if (!IsEnabled) return;
        if (Hc == null) return;
        
        _lastAttackTime += Time.deltaTime;
        if (_lastAttackTime < AttackInterval) return;

        var hcPos = Hc.transform.position;
        var attacked = false;
        foreach (var enemy in from enemy in EnemyManager.Enemies
                 where enemy.HealthManager != null
                 where enemy.GameObject != null
                 let enemyPos = enemy.GameObject.transform.position
                 where !(Vector2.Distance(hcPos, enemyPos) > Range)
                 select enemy)
        {
            enemy.HealthManager.Hit(
                new HitInstance
                {
                    Source = Hc.gameObject,
                    DamageDealt = Damage,
                    IsHeroDamage = true,
                    AttackType = AttackTypes.Spell,
                    Multiplier = 1f
                });
            attacked = true;
        }

        if (attacked) _lastAttackTime = 0;
    }
}