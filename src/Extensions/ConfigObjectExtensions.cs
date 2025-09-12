using System.Linq;
using HKSC.Misc;
using UnityEngine;

namespace HKSC.Extensions;

public static class ConfigObjectExtensions
{
    public static ConfigObject<bool> CreateToggleHotkey(this ConfigObject<bool> configObject, KeyCode key = KeyCode.None)
    {
        var split = configObject.Key.Split("::");
        // FIXME: cause exception
        _ = new Hotkey(split.First(), split.Last(), key, down =>
        {
            if (down) configObject.Value = !configObject.Value;
        });

        return configObject;
    }
}