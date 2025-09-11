using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Menu;

public class MapFeature : FeatureBase
{
    public override ModPage Page => ModPage.Menu;
    
    public bool EnableAlwaysShowPosition { private set; get; }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Map");
        EnableAlwaysShowPosition = GUILayout.Toggle(EnableAlwaysShowPosition, "Always Show Position");
        UiUtils.EndCategory();
    }
}