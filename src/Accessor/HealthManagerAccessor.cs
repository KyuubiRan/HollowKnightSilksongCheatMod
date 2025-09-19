using System.Reflection;
using HarmonyLib;

namespace HKSC.Accessor;

public static class HealthManagerAccessor
{
    public static readonly AccessTools.FieldRef<HealthManager, int> InitHpField =
        AccessTools.FieldRefAccess<HealthManager, int>("initHp");    
    
    public static readonly MethodInfo TakeDamageMethod = AccessTools.Method(typeof(HealthManager), "TakeDamage", [typeof(HitInstance)]);
}