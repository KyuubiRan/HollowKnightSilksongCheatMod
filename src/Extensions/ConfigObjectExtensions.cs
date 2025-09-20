using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
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

    public static ConfigObject<bool> AddToggleToast(this ConfigObject<bool> configObject, string text)
    {
        configObject.OnChanged += config =>
        {
            var status = config ? "feature.generic.enable".Translate() : "feature.generic.disable".Translate();
            ModMainUi.Instance.ShowToast($"{text.Translate()}: {status}");
        };
        return configObject;
    }
}