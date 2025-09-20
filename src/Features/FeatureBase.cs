using System;
using HKSC.Managers;
using HKSC.Ui;

namespace HKSC.Features;

public abstract class FeatureBase
{
    public abstract ModPage Page { get; }

    public FeatureBase()
    {
        ModMainUi.Instance.OnUpdate += OnUpdate;
        ModMainUi.Instance.OnFixedUpdate += OnFixedUpdate;
        ModMainUi.Instance.OnInitialize += Start;
    }

    protected virtual void OnGui()
    {
    }

    private void Start()
    {
        ModMainUi.Instance.AddRender(Page, OnGui);
        OnStart();
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnFixedUpdate()
    {
    }

    public static T Instance<T>() where T : FeatureBase => FeatureManager.GetFeature<T>();
    public static Lazy<T> LazyInstance<T>() where T : FeatureBase => FeatureManager.LazyFeature<T>();
}