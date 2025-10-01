using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Inventory;

public class ItemCountFeature : FeatureBase
{
    private static GameManager Gm => GameManager.UnsafeInstance;
    private static CollectableItemManager Cim => CollectableItemManager.UnsafeInstance;

    public override ModPage Page => ModPage.Inventory;

    public readonly ConfigObject<bool> EnableLockMaxItemUse = CfgManager
        .Create("PlayerItem::EnableLockMaxItemUse", false)
        .CreateToggleHotkey("hotkey.namespace.inventory.item", "hotkey.inventory.item.toggleLockMaxItemUse")
        .AddToggleToast("feature.inventory.item.lockMaxItemUse");

    public readonly ConfigObject<bool> EnableAutoReplenishCountItem = CfgManager
        .Create("PlayerItem::EnableAutoReplenishCountItem", false)
        .CreateToggleHotkey("hotkey.namespace.inventory.item", "hotkey.inventory.item.toggleAutoReplenishCountItem")
        .AddToggleToast("feature.inventory.item.autoReplenishCountItem");

    private string _filterText = "";
    private readonly Dictionary<string, string> _itemAddTexts = new();
    private readonly Dictionary<string, StrongBox<int>> _itemAdd = new();
    private bool _onlyShowHas = true;
    
    private static readonly HashSet<string> FilteredItems =
    [
        "Blue Goop Jar", "Slab Key"
    ];

    private void DrawItem()
    {
        UiUtils.BeginCategory("feature.inventory.item.title".Translate());
        EnableLockMaxItemUse.Value = GUILayout.Toggle(EnableLockMaxItemUse, "feature.inventory.item.lockMaxItemUse".Translate());
        EnableAutoReplenishCountItem.Value = GUILayout.Toggle(EnableAutoReplenishCountItem, "feature.inventory.item.autoReplenishCountItem".Translate());
        UiUtils.EndCategory();
    }

    private void DrawItemCount()
    {
        UiUtils.BeginCategory("feature.inventory.item.count.title".Translate());

        GUILayout.BeginHorizontal();
        GUILayout.Label("ui.generic.filter".Translate(), GUILayout.ExpandWidth(false));
        _filterText = GUILayout.TextField(_filterText, GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();

        _onlyShowHas = GUILayout.Toggle(_onlyShowHas, "feature.inventory.item.count.onlyShowHas".Translate());

        if (Gm && Gm.IsGameplayScene() && Cim)
        {
            var list = _onlyShowHas ? CollectableItemManager.GetCollectedItems() : Cim.GetAllCollectables();

            foreach (
                var collectableItem in list
                    .Where(x => !FilteredItems.Contains(x.name))
            )
            {
                var translatedName = collectableItem.GetCollectionName();
                if (!string.IsNullOrWhiteSpace(_filterText))
                {
                    if (!translatedName.Contains(_filterText, StringComparison.OrdinalIgnoreCase) &&
                        !collectableItem.name.Contains(_filterText, StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                GUILayout.BeginHorizontal();
                GUILayout.Label($"{translatedName}: {collectableItem.CollectedAmount}");
                _itemAddTexts.TryAdd(collectableItem.name, "1");
                _itemAdd.TryAdd(collectableItem.name, new StrongBox<int>(1));
                
                _itemAddTexts[collectableItem.name] = UiUtils.InputInt(ref _itemAdd[collectableItem.name].Value, _itemAddTexts[collectableItem.name], fixedWidth: 50);
                
                if (GUILayout.Button("feature.generic.add".Translate(), GUILayout.Width(50)))
                    collectableItem.Collect(_itemAdd[collectableItem.name].Value);
                if (GUILayout.Button("feature.generic.set".Translate(), GUILayout.Width(50)))
                {
                    var to = _itemAdd[collectableItem.name].Value;
                    var current = collectableItem.CollectedAmount;
                    if (to > current)
                        collectableItem.Collect(to - current);
                    else if (to < current)
                        collectableItem.Take(current - to);
                }

                GUILayout.EndHorizontal();
            }
        }

        UiUtils.EndCategory();
    }

    protected override void OnGui()
    {
        DrawItem();
        DrawItemCount();
    }
}