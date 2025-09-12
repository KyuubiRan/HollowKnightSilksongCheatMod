using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Menu;

public class MapFeature : FeatureBase
{
    private static GameManager Gm => GameManager.UnsafeInstance;
    private static GameMap Map => Gm?.gameMap;

    public override ModPage Page => ModPage.Menu;

    public readonly ConfigObject<bool> EnableAlwaysShowPosition = CfgManager.Create("Map::EnableAlwaysShowPosition", false).CreateToggleHotkey();

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Map");

        EnableAlwaysShowPosition.Value = GUILayout.Toggle(EnableAlwaysShowPosition, "Always Show Player's Position");

        if (GUILayout.Button("Update Map"))
        {
            Map?.UpdateGameMap();
        }

        UiUtils.EndCategory();
    }
}