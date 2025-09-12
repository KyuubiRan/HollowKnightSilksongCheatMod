using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Currency;

public class GeoFeature : FeatureBase
{
    private static HeroController Hc => HeroController.instance;
    public override ModPage Page => ModPage.Currency;

    private int _geoValue = 100;
    private string _geoValueStr = "100";
    public readonly ConfigObject<bool> EnableAutoCollect = CfgManager.Create("Rosaries::EnableAutoCollect", false);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Rosaries");

        EnableAutoCollect.Value = GUILayout.Toggle(EnableAutoCollect, "Auto Collect");
        _geoValueStr = UiUtils.InputInt(ref _geoValue, _geoValueStr, "Value");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
            Hc?.AddGeo(_geoValue);
        if (GUILayout.Button("Remove"))
            Hc?.TakeGeo(_geoValue);
        GUILayout.EndHorizontal();

        UiUtils.EndCategory();
    }
}