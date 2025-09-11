using HKSC.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace HKSC.Misc;

public class Hotkey
{
    public delegate void HotkeyEvent(bool isKeyDown);

    public event HotkeyEvent KeyEvent;

    public string Name { get; private set; }
    public KeyCode Key;

    public Hotkey(string name, KeyCode key, [CanBeNull] HotkeyEvent @event = null)
    {
        Key = key;
        Name = name;
        if (@event != null) KeyEvent += @event;
        HotkeyManager.RegisterHotkey(this);
    }

    ~Hotkey()
    {
        HotkeyManager.UnregisterHotkey(this);
    }

    public bool IsDown { get; private set; }

    public void OnDown()
    {
        if (Input.GetKeyDown(Key))
        {
            IsDown = true;
            KeyEvent?.Invoke(true);
        }
    }

    public void OnUp()
    {
        if (IsDown && Input.GetKeyUp(Key))
        {
            IsDown = false;
            KeyEvent?.Invoke(false);
        }
    }
}