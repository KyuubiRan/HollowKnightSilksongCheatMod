using HKSC.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace HKSC.Misc;

public class Hotkey
{
    public delegate void HotkeyEvent(bool isKeyDown);

    public event HotkeyEvent KeyEvent;

    public string Name { get; private set; }
    public string Id { get; private set; }
    public string Namespace { get; private set; }
    public readonly ConfigObject<KeyCode> Key;

    public Hotkey(string id, string @namespace, string name, KeyCode key, [CanBeNull] HotkeyEvent @event = null)
    {
        Id = id;
        Namespace = @namespace;
        Name = name;
        Key = CfgManager.Create($"Hotkey::{id}", key);
        if (@event != null) KeyEvent += @event;
        HotkeyManager.RegisterHotkey(this);
    }

    public static Hotkey Create(string id, string @namespace, string name, KeyCode key, [CanBeNull] HotkeyEvent @event = null) =>
        new(id, @namespace, name, key, @event);

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