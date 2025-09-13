using System.Collections.Generic;
using HKSC.Misc;
using HKSC.Ui;
using JetBrains.Annotations;
using UnityEngine;

namespace HKSC.Managers;

public static class HotkeyManager
{
    private static readonly HashSet<Hotkey> Hotkeys = [];

    private static readonly Dictionary<string, List<Hotkey>> HotkeyDict = new();

    public static IReadOnlyCollection<Hotkey> AllHotkeys => Hotkeys;
    public static IReadOnlyDictionary<string, List<Hotkey>> HotkeysByCategory => HotkeyDict;

    [CanBeNull] public static Hotkey EditingHotkey { get; private set; }

    public static void RegisterHotkey(Hotkey hotkey)
    {
        if (!Hotkeys.Add(hotkey))
            return;

        if (!HotkeyDict.ContainsKey(hotkey.Namespace))
            HotkeyDict[hotkey.Namespace] = [];
        HotkeyDict[hotkey.Namespace].Add(hotkey);
    }

    public static void UnregisterHotkey(Hotkey hotkey)
    {
        Hotkeys.Remove(hotkey);
    }

    public static void StartEditHotkey(Hotkey hotkey)
    {
        EditingHotkey = hotkey;
    }

    public static void StopEditHotkey()
    {
        EditingHotkey = null;
    }

    public static void OnUpdate()
    {
        if (EditingHotkey != null)
        {
            var e = Event.current;
            if (e is not { isKey: true }) return;

            switch (e.keyCode)
            {
                case KeyCode.None:
                    break;
                case KeyCode.Escape:
                    EditingHotkey.Key.Value = KeyCode.None;
                    EditingHotkey.Key.FireChanged();
                    EditingHotkey = null;
                    e.Use();
                    break;
                default:
                    EditingHotkey.Key.Value = e.keyCode;
                    EditingHotkey.Key.FireChanged();
                    EditingHotkey = null;
                    e.Use();
                    break;
            }

            return;
        }

        foreach (var hotkey in Hotkeys)
        {
            hotkey.OnDown();
            hotkey.OnUp();
        }
    }

    public static void Init()
    {
        Hotkeys.Clear();
        HotkeyDict.Clear();
        EditingHotkey = null;

        RegisterHotkey(Hotkey.Create("General::ToggleMainUi", "hotkey.namespace.generic", "hotkey.generic.toggleMainUi", KeyCode.F1, down =>
        {
            if (down) ModMainUi.Instance.ToggleShow();
        }));
    }
}