using HarmonyLib;

namespace HKSC.Accessor;

public static class GameMapAccessor
{
    public static readonly AccessTools.FieldRef<GameMap, bool> DisplayingCompassField = AccessTools.FieldRefAccess<GameMap, bool>("displayingCompass");
}