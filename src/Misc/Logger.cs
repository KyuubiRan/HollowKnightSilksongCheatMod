using BepInEx.Logging;

namespace HKSC.Misc;

public static class Logger
{
    public static ManualLogSource PluginLogger;

    public static void LogDebug(object data) => PluginLogger.LogDebug(data);
    public static void LogInfo(object data) => PluginLogger.LogInfo(data);
    public static void LogWarning(object data) => PluginLogger.LogWarning(data);
    public static void LogError(object data) => PluginLogger.LogError(data);
    public static void LogFatal(object data) => PluginLogger.LogFatal(data);
}