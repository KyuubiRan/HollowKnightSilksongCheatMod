using System.Reflection;
using HarmonyLib;

namespace HKSC.Accessor;

public static class GameManagerAccessor
{
    public static readonly MethodInfo SetPausedStateMethod = AccessTools.Method(typeof(GameManager), "SetPausedState", [typeof(bool)]);
    public static readonly AccessTools.FieldRef<GameManager, SceneLoad> SceneLoadField = AccessTools.FieldRefAccess<GameManager, SceneLoad>("sceneLoad");
}