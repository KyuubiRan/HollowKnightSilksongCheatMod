using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Menu;

public class MapFeature : FeatureBase
{
    private static GameManager Gm => GameManager.UnsafeInstance;
    private static GameMap Map => Gm?.gameMap;

    public override ModPage Page => ModPage.Menu;

    public bool EnableAlwaysShowPosition { private set; get; }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Map");

        EnableAlwaysShowPosition = GUILayout.Toggle(EnableAlwaysShowPosition, "Always Show Player's Position");

        if (GUILayout.Button("Update Map"))
        {
            Map?.UpdateGameMap();
        }

        UiUtils.EndCategory();
    }
}