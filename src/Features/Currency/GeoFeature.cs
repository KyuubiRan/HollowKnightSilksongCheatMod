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

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Shell Shards");
        
        UiUtils.BeginCategory("Geo");

        _geoValueStr = UiUtils.InputInt(ref _geoValue, _geoValueStr,"Value");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
            Hc?.AddGeo(_geoValue);
        if (GUILayout.Button("Remove"))
            Hc?.TakeGeo(_geoValue);
        GUILayout.EndHorizontal();
        
        UiUtils.EndCategory();
    }
}