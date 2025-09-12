using System;
using System.IO;
using HKSC.Misc;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace HKSC.Managers;

public static class CfgManager
{
    private static bool _changed;
    private static float _delta;
    private const float SaveInterval = 2.0f;
    private static JObject _configData = new();
    private static string _configDataPath;

    public static void Load()
    {
        try
        {
            var assemblyPath = typeof(CfgManager).Assembly.Location;
            var dir = Path.GetDirectoryName(assemblyPath);
            var configPath = Path.Combine(dir ?? throw new InvalidOperationException(), "config.json");
            _configDataPath = configPath;
            if (!File.Exists(configPath))
            {
                Log.LogInfo("Config file not found, creating a new one.");
                File.WriteAllText(configPath, "{}");
            }

            var json = File.ReadAllText(configPath);
            _configData = JObject.Parse(json);
            Log.LogInfo("Config loaded successfully.");
        }
        catch (Exception e)
        {
            Log.LogError($"Failed to load config: {e}");
        }
    }

    public static void Save()
    {
        WriteToFile();
        _changed = false;
        _delta = 0.0f;
    }

    public static void FireChanged<T>(ConfigObject<T> config)
    {
        if (config == null) return;
        _changed = true;
        _delta = 0.0f;
        _configData[config.Key] = JToken.FromObject(config.Value);
    }

    private static void WriteToFile()
    {
        try
        {
            if (string.IsNullOrEmpty(_configDataPath))
            {
                Log.LogError("[HKSC] Config path is not set. Cannot save config.");
                return;
            }

            var json = _configData.ToString();
            File.WriteAllText(_configDataPath, json);
            // Log.LogInfo("Config saved successfully.");
        }
        catch (Exception e)
        {
            Log.LogError($"Failed to save config: {e}");
        }
    }

    public static void OnUpdate()
    {
        if (!_changed)
        {
            _delta = 0.0f;
            return;
        }

        _delta += Time.deltaTime;
        if (_delta >= SaveInterval)
        {
            Save();
        }
    }

    public static ConfigObject<T> Create<T>(string key, T defValue = default)
    {
        var obj = new ConfigObject<T>(key);

        if (!_configData.TryGetValue(key, out var token))
        {
            Log.LogDebug($"Config key '{key}' not found, using default: '{defValue}'");
            return obj;
        }

        try
        {
            obj._value = token.ToObject<T>();
        }
        catch (Exception e)
        {
            Log.LogError($"Failed to parse config key '{key}': {e}");
        }

        return obj;
    }
}