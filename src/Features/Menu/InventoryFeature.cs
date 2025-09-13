using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Menu;

public class InventoryFeature : FeatureBase
{
    public override ModPage Page => ModPage.Menu;

    public readonly ConfigObject<bool> EnableEquipAnywhere =
        CfgManager.Create("Inventory::EnableEquipAnywhere", false).CreateToggleHotkey("hotkey.namespace.menu", "hotkey.menu.toggleEquipAnywhere");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.menu.inventory.title".Translate());
        EnableEquipAnywhere.Value = GUILayout.Toggle(EnableEquipAnywhere, "feature.menu.inventory.equipAnywhere".Translate());
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        CheatManager.CanChangeEquipsAnywhere = EnableEquipAnywhere;
    }
}