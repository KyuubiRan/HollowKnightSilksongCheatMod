using HKSC.Ui;

namespace HKSC.Features;

public abstract class FeatureBase
{
    public abstract ModPage Page { get; }

    public FeatureBase()
    {
        ModMainUi.Instance.OnGui += OnGuiRender;
        ModMainUi.Instance.OnUpdate += OnUpdate;
        ModMainUi.Instance.OnInitialize += OnStart;
    }

    private void OnGuiRender()
    {
        if (ModMainUi.Instance.CurrentPage == Page)
            OnGui();
    }

    protected virtual void OnGui()
    {
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate()
    {
    }
}