using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Inventory;

public class ItemCountFeature : FeatureBase
{
    public override ModPage Page => ModPage.Inventory;

    public readonly ConfigObject<bool> EnableLockMaxItemUse = CfgManager.Create("PlayerItem::EnableLockMaxItemUse", false)
                                                                        .CreateToggleHotkey("hotkey.namespace.inventory.item", "hotkey.inventory.item.toggleLockMaxItemUse");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.inventory.item.title".Translate());
        EnableLockMaxItemUse.Value = GUILayout.Toggle(EnableLockMaxItemUse, "feature.inventory.item.lockMaxItemUse".Translate());
        UiUtils.EndCategory();
    }
}