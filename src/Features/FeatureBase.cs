using HKSC.Ui;

namespace HKSC.Features;

public abstract class FeatureBase
{
    public abstract ModPage Page { get; }

    public FeatureBase()
    {
        ModMainUi.Instance.OnUpdate += OnUpdate;
        ModMainUi.Instance.OnInitialize += OnStart;
    }

    protected virtual void OnGui()
    {
    }

    protected virtual void OnStart()
    {
        ModMainUi.Instance.AddRender(Page, OnGui);
    }

    protected virtual void OnUpdate()
    {
    }
}