using System;
using System.Collections.Generic;
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
    private static readonly Dictionary<Type, FeatureBase> Features = [];

    private static void AddFeature<T>() where T : FeatureBase, new()
    {
        Features.Add(typeof(T), new T());
    }

    [CanBeNull]
    public static T GetFeature<T>() where T : FeatureBase => Features.TryGetValue(typeof(T), out var feature) ? feature as T : null;

    public static Lazy<T> LazyFeature<T>() where T : FeatureBase => new(() => Features.TryGetValue(typeof(T), out var feature) ? feature as T : null);

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
        AddFeature<ToolItemFeature>();
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
        AddFeature<WorldFeature>();

        // Teleport
        AddFeature<CurrentSceneDetails>();
        AddFeature<DeathTeleport>();
        AddFeature<CustomTeleport>();

        // About
        AddFeature<AboutPage>();

        // Hotkey
        AddFeature<HotkeySettingPage>();

        // Low priority
        AddFeature<GodModeControl>();
    }
}