using System.Collections.Generic;
using System.Linq;
using HKSC.Features;
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
        AddFeature<PlayerFeatures>();
        AddFeature<CurrencyFeatures>();
        AddFeature<EnemyFeatures>();
    }
}