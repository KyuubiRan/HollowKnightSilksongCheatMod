using System.Collections.Generic;
using System.Linq;
using HKSC.Features;
using HKSC.Features.Currency;
using HKSC.Features.Enemy;
using HKSC.Features.Misc;
using HKSC.Features.Player;
using JetBrains.Annotations;

namespace HKSC.Managers;

public static class FeatureManager
{
    private static readonly HashSet<FeatureBase> Features = [];

    private static void AddFeature<T>() where T : FeatureBase, new()
    {
        Features.Add(new T());
    }

    [CanBeNull]
    public static T GetFeature<T>() where T : FeatureBase => Features.FirstOrDefault(x => x is T) as T;

    public static void Init()
    {
        // Player
        AddFeature<HealthFeature>();
        AddFeature<SilkFeature>();
        AddFeature<DamageFeature>();
        AddFeature<ActionFeature>();

        // Currency
        AddFeature<ShellShardsFeature>();
        AddFeature<GeoFeature>();

        // Enemy
        AddFeature<ShowEnemyHpFeature>();
        
        // About
        AddFeature<AboutPage>();
        
        // Hotkey
        AddFeature<HotkeySettingPage>();
    }
}