using System;
using System.Collections.Generic;
using System.Linq;
using HKSC.Features;
using HKSC.Features.Currency;
using HKSC.Features.Enemy;
using HKSC.Features.Game;
using HKSC.Features.Inventory;
using HKSC.Features.Menu;
using HKSC.Features.Misc;
using HKSC.Features.Player;
using HKSC.Features.Teleport;
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

    public static Lazy<T> LazyFeature<T>() where T : FeatureBase => new(() => Features.FirstOrDefault(x => x is T) as T);

    public static void Init()
    {
        // Player
        AddFeature<HealthFeature>();
        AddFeature<SilkFeature>();
        AddFeature<NoclipFeature>();
        AddFeature<DamageFeature>();
        AddFeature<CrestFeature>();
        AddFeature<ActionFeature>();
        AddFeature<EnhancedAttack>();
        AddFeature<KillAuraFeature>();

        // Inventory
        AddFeature<InventoryFeature>();
        AddFeature<ItemCountFeature>();

        // Currency
        AddFeature<ShellShardFeature>();
        AddFeature<GeoFeature>();

        // Enemy
        AddFeature<ShowEnemyHpFeature>();

        // Menu
        AddFeature<MapFeature>();

        // Game
        AddFeature<TimeScaleFeature>();
        AddFeature<FpsLimiterFeature>();

        // Teleport
        AddFeature<CurrentSceneDetails>();
        AddFeature<DeathTeleport>();
        AddFeature<CustomTeleport>();

        // About
        AddFeature<AboutPage>();

        // Hotkey
        AddFeature<HotkeySettingPage>();
    }
}