using HarmonyLib;

namespace HKSC.Accessor;

public static class HealthManagerAccessor
{
    public static readonly AccessTools.FieldRef<HealthManager, int> InitHpField =
        AccessTools.FieldRefAccess<HealthManager, int>("initHp");
}