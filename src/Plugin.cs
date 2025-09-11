using BepInEx;
using HarmonyLib;
using HKSC.Managers;
using HKSC.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        EnemyManager.OnUpdate();
    }

    private void Start()
    {
        var go = new GameObject("HKSC Main UI Controller");
        go.AddComponent<ModMainUi>();

        FeatureManager.Init();
        ModMainUi.Instance.Initialize();
        SceneManager.activeSceneChanged += (oldScene, newScene) => { EnemyManager.OnSceneChanged(newScene); };
    }
}