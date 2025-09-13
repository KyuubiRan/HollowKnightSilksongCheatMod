using System.Linq;
using HKSC.Misc;
using JetBrains.Annotations;
using UnityEngine;

namespace HKSC.Extensions;

public static class ConfigObjectExtensions
{
    public static ConfigObject<bool> CreateToggleHotkey(this ConfigObject<bool> configObject, string @namespace, string name, KeyCode key = KeyCode.None)
    {
        Hotkey.Create(configObject.Key, @namespace, name, key, down =>
        {
            if (down) configObject.Value = !configObject.Value;
        });

        return configObject;
    }
}