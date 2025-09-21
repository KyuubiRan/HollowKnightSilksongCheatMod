using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace HKSC.Patches;

// WTF spamming NRE in console, simple to fix
[HarmonyPatch(typeof(ObjectJitterOnRender))]
public class ObjectJitterOnRenderPatcher
{
    private static readonly AccessTools.FieldRef<ObjectJitterOnRender, GameObject> GoField = AccessTools.FieldRefAccess<ObjectJitterOnRender, GameObject>("go");

    [HarmonyPatch("OnCameraPreCull")]
    [HarmonyPrefix]
    static bool OnCameraPreCull_Prefix(ObjectJitterOnRender __instance)
    {
        return GoField(__instance);
    }

    [HarmonyPatch("OnCameraPostRender")]
    [HarmonyPrefix]
    static bool OnCameraPostRender_Prefix(ObjectJitterOnRender __instance)
    {
        return GoField(__instance);
    }
}