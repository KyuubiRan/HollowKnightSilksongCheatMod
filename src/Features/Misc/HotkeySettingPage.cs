using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Misc;

public class HotkeySettingPage : FeatureBase
{
    public override ModPage Page => ModPage.Hotkey;

    private readonly Hotkey _toggleModMenuHotkey = new("Toggle Main Ui", KeyCode.F1, down =>
    {
        if (down) ModMainUi.Instance.ToggleShow();
    });

    private void HotkeyItem(Hotkey hotkey)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(hotkey.Name);
        if (GUILayout.Button(HotkeyManager.EditingHotkey == hotkey ? "..." : hotkey.Key.ToString(), GUILayout.Width(100)))
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
        UiUtils.BeginCategory("Main");
        HotkeyItem(_toggleModMenuHotkey);
        UiUtils.EndCategory();
    }
}