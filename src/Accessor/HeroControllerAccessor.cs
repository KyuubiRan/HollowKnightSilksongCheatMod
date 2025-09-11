using System.Reflection;
using HarmonyLib;

namespace HKSC.Accessor;

public static class HeroControllerAccessor
{
    public static readonly AccessTools.FieldRef<HeroController, float> AttackCdField = AccessTools.FieldRefAccess<HeroController, float>("attack_cooldown");
    public static readonly MethodInfo StartInvulnerableMethod = AccessTools.Method(typeof(HeroController), "StartInvulnerable", [typeof(float)]);
}