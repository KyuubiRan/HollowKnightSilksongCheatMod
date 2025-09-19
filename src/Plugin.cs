using BepInEx;
using HarmonyLib;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyLogger = HKSC.Misc.Logger;

namespace HKSC;

[BepInPlugin(ModConstants.Namespace, ModConstants.ModName, ModConstants.Version)]
public class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Harmony = new(ModConstants.Namespace);

    private void OnEnable()
    {
        Harmony.PatchAll();
    }

    private void Update()
    {
        HotkeyManager.OnUpdate();
        CfgManager.OnUpdate();
    }

    private void Start()
    {
        MyLogger.PluginLogger = Logger;
        CfgManager.Load();
        LanguageManager.Init();
        HotkeyManager.Init();

        var go = new GameObject("HKSC Main UI Controller");
        DontDestroyOnLoad(go);
        go.AddComponent<ModMainUi>();

        FeatureManager.Init();
        ModMainUi.Instance.Initialize();
    }
}