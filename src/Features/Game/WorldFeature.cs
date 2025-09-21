using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Patches;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Game;

public class WorldFeature : FeatureBase
{
    public override ModPage Page => ModPage.Game;

    public readonly ConfigObject<bool> EnableFullBrightInDarknessRegion = CfgManager
        .Create("World::EnableFullBrightInDarknessRegion", false)
        .CreateToggleHotkey("hotkey.namespace.game.world", "hotkey.game.world.toggleFullBrightInDarknessRegion")
        .AddToggleToast("feature.game.world.fullBrightInDarknessRegion")
        .AddOnChangedListener(x => { DarknessRegion.SetDarknessLevel(x ? 0 : DarknessRegionPatcher.LastDarknessLevel); });

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.game.world.title".Translate());
        EnableFullBrightInDarknessRegion.Value =
            GUILayout.Toggle(EnableFullBrightInDarknessRegion, "feature.game.world.fullBrightInDarknessRegion".Translate());
        UiUtils.EndCategory();
    }
}