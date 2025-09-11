using System.Collections.Generic;
using HKSC.Misc;
using JetBrains.Annotations;
using UnityEngine;

namespace HKSC.Managers;

public static class HotkeyManager
{
    private static readonly HashSet<Hotkey> Hotkeys = [];
    [CanBeNull] public static Hotkey EditingHotkey { get; private set; }

    public static void RegisterHotkey(Hotkey hotkey)
    {
        Hotkeys.Add(hotkey);
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
                    EditingHotkey = null;
                    e.Use();
                    break;
                default:
                    EditingHotkey.Key = e.keyCode;
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
}