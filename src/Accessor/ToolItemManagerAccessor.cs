using System.Reflection;
using HarmonyLib;

namespace HKSC.Accessor;

public static class ToolItemManagerAccessor
{
    public static readonly MethodInfo GetIsInfiniteToolUseEnabledMethod = AccessTools.Method(typeof(ToolItemManager), "get_IsInfiniteToolUseEnabled", []);
}