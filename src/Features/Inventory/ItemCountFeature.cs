using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;

namespace HKSC.Features.Inventory;

public class ItemCountFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;

    public override ModPage Page => ModPage.Inventory;

    private readonly ConfigObject<bool> _enableLockMaxItemUse = CfgManager.Create("PlayerItem::EnableLockMaxItemUse", false)
                                                                          .CreateToggleHotkey("hotkey.namespace.item", "hotkey.item.toggleLockMaxItemUse");

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feat.inventory.item".Translate());
        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (Hc == null) return;

        if (_enableLockMaxItemUse)
        {
        }
    }
}