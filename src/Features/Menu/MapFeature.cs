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

    public readonly ConfigObject<bool> EnableAlwaysShowPosition =
        CfgManager.Create("Map::EnableAlwaysShowPosition", false).CreateToggleHotkey("hotkey.namespace.menu","hotkey.menu.toggleAlwaysShowPosition");    
    
    // public readonly ConfigObject<bool> EnableUnlockMap =
    //     CfgManager.Create("Map::EnableUnlockMap", false).CreateToggleHotkey("hotkey.namespace.menu","hotkey.menu.toggleEnableUnlockMap");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.menu.map.title".Translate());

        EnableAlwaysShowPosition.Value = GUILayout.Toggle(EnableAlwaysShowPosition, "feature.menu.map.alwaysShowPosition".Translate());
        // EnableUnlockMap.Value = GUILayout.Toggle(EnableUnlockMap, "feature.menu.map.unlockMap".Translate());

        // if (GUILayout.Button("Update Map"))
        // {
        //     Map?.UpdateGameMap();
        // }

        UiUtils.EndCategory();
    }
}