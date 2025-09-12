using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Misc;

public class HotkeySettingPage : FeatureBase
{
    public override ModPage Page => ModPage.Hotkey;

    private static void HotkeyItem(Hotkey hotkey)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(hotkey.Name);
        if (GUILayout.Button(HotkeyManager.EditingHotkey == hotkey ? "..." : hotkey.Key.Value.ToString(), GUILayout.Width(120)))
        {
            if (HotkeyManager.EditingHotkey == hotkey)
                HotkeyManager.StopEditHotkey();
            else
                HotkeyManager.StartEditHotkey(hotkey);
        }

        GUILayout.EndHorizontal();
    }

    protected override void OnGui()
    {
        foreach (var (category, hotkeys) in HotkeyManager.HotkeysByCategory)
        {
            UiUtils.BeginCategory(category);
            foreach (var hotkey in hotkeys) HotkeyItem(hotkey);
            UiUtils.EndCategory();
        }
    }
}