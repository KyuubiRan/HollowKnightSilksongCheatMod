using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Currency;

public class GeoFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    public override ModPage Page => ModPage.Currency;

    private int _geoValue = 100;
    private string _geoValueStr = "100";

    public readonly ConfigObject<bool> EnableAutoCollect = CfgManager
        .Create("Rosaries::EnableAutoCollect", false)
        .CreateToggleHotkey("hotkey.namespace.currency", "hotkey.currency.toggleAutoCollectGeo")
        .AddToggleToast("feature.currency.autoCollectRosaries");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.currency.rosaries.title".Translate());

        EnableAutoCollect.Value = GUILayout.Toggle(EnableAutoCollect, "feature.currency.autoCollect".Translate());
        _geoValueStr = UiUtils.InputInt(ref _geoValue, _geoValueStr, "feature.generic.value".Translate());

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("feature.generic.add".Translate()))
            Hc?.AddGeo(_geoValue);
        if (GUILayout.Button("feature.generic.reduce".Translate()))
            Hc?.TakeGeo(_geoValue);
        GUILayout.EndHorizontal();

        UiUtils.EndCategory();
    }
}