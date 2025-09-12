using HarmonyLib;

namespace HKSC.Patches;

[HarmonyPatch(typeof(CheatManager))]
public class CheatManagerPatcher
{
    [HarmonyPatch("get_IsCheatsEnabled")]
    [HarmonyPrefix]
    static bool IsCheatsEnabled_Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }
}