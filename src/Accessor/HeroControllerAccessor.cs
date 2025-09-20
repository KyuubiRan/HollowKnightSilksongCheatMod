using System.Reflection;
using GlobalEnums;
using HarmonyLib;
using UnityEngine;

namespace HKSC.Accessor;

public static class HeroControllerAccessor
{
    public static readonly AccessTools.FieldRef<HeroController, float> AttackCdField = AccessTools.FieldRefAccess<HeroController, float>("attack_cooldown");

    public static readonly AccessTools.FieldRef<HeroController, float> DashCooldownTimerField =
        AccessTools.FieldRefAccess<HeroController, float>("dashCooldownTimer");

    public static readonly AccessTools.FieldRef<HeroController, bool> AirDashedField = AccessTools.FieldRefAccess<HeroController, bool>("airDashed");

    public static readonly AccessTools.FieldRef<HeroController, float> HarpoonDashCooldownField =
        AccessTools.FieldRefAccess<HeroController, float>("harpoonDashCooldown");

    public static readonly AccessTools.FieldRef<HeroController, Rigidbody2D> Rb2dField =
        AccessTools.FieldRefAccess<HeroController, Rigidbody2D>("rb2d");

    public static readonly AccessTools.FieldRef<HeroController, Collider2D> Col2dField =
        AccessTools.FieldRefAccess<HeroController, Collider2D>("col2d");   
    
    public static readonly AccessTools.FieldRef<HeroController, HeroController.ReaperCrestStateInfo> ReaperStateField =
        AccessTools.FieldRefAccess<HeroController, HeroController.ReaperCrestStateInfo>("reaperState");   
    
    public static readonly MethodInfo StartInvulnerableMethod = AccessTools.Method(typeof(HeroController), "StartInvulnerable", [typeof(float)]);
    public static readonly MethodInfo AttackMethod = AccessTools.Method(typeof(HeroController), "Attack", [typeof(AttackDirection)]);
}