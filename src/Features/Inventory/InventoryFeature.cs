using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Inventory;

public class InventoryFeature : FeatureBase
{
    public override ModPage Page => ModPage.Inventory;

    public readonly ConfigObject<bool> EnableEquipAnywhere =
        CfgManager.Create("Inventory::EnableEquipAnywhere", false).CreateToggleHotkey("hotkey.namespace.inventory", "hotkey.inventory.toggleEquipAnywhere");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.inventory.title".Translate());
        EnableEquipAnywhere.Value = GUILayout.Toggle(EnableEquipAnywhere, "feature.inventory.equipAnywhere".Translate());
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        CheatManager.CanChangeEquipsAnywhere = EnableEquipAnywhere;
    }
}