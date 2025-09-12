using System.Linq;
using HKSC.Misc;
using JetBrains.Annotations;
using UnityEngine;

namespace HKSC.Extensions;

public static class ConfigObjectExtensions
{
    public static ConfigObject<bool> CreateToggleHotkey(this ConfigObject<bool> configObject, [CanBeNull] string name = null, KeyCode key = KeyCode.None)
    {
        Hotkey.Create(configObject.Key, string.IsNullOrWhiteSpace(name) ?  configObject.Key : name, key, down =>
        {
            if (down) configObject.Value = !configObject.Value;
        });

        return configObject;
    }
}