using HKSC.Managers;
using HKSC.Ui;

namespace HKSC.Features;

public abstract class FeatureBase
{
    public abstract ModPage Page { get; }

    public FeatureBase()
    {
        ModMainUi.Instance.OnUpdate += OnUpdate;
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

    public static T Instance<T>() where T : FeatureBase => FeatureManager.GetFeature<T>();
}