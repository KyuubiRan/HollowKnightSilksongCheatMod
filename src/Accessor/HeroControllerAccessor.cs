using System.Reflection;
using HarmonyLib;

namespace HKSC.Accessor;

public static class HeroControllerAccessor
{
    public static readonly AccessTools.FieldRef<HeroController, float> AttackCdField = AccessTools.FieldRefAccess<HeroController, float>("attack_cooldown");
    public static readonly AccessTools.FieldRef<HeroController, float> DashCooldownTimerField = AccessTools.FieldRefAccess<HeroController, float>("dashCooldownTimer");
    public static readonly AccessTools.FieldRef<HeroController, bool> AirDashedField = AccessTools.FieldRefAccess<HeroController, bool>("airDashed");
    public static readonly AccessTools.FieldRef<HeroController, float> HarpoonDashCooldownField = AccessTools.FieldRefAccess<HeroController, float>("harpoonDashCooldown");

    public static readonly MethodInfo StartInvulnerableMethod = AccessTools.Method(typeof(HeroController), "StartInvulnerable", [typeof(float)]);
}