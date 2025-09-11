using HarmonyLib;

namespace HKSC.Accessor;

public class HeroControllerAccessor
{
    public static readonly AccessTools.FieldRef<HeroController, float> AttackCdField = AccessTools.FieldRefAccess<HeroController, float>("attack_cooldown");
}