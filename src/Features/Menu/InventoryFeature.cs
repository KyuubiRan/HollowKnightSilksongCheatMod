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
        CfgManager.Create("Inventory::EnableEquipAnywhere", false);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Inventory");
        EnableEquipAnywhere.Value = GUILayout.Toggle(EnableEquipAnywhere, "Equip Anywhere");
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        CheatManager.CanChangeEquipsAnywhere = EnableEquipAnywhere;
    }
}