using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HKSC.Managers;

public static class EnemyManager
{
    private static GameManager Gm => GameManager.instance;

    public static List<EnemyInfo> Enemies { get; private set; } = [];
    private static readonly HashSet<int> EnemyLayers = [11, 17];

    public struct EnemyInfo
    {
        public GameObject GameObject;
        public HealthManager HealthManager;

        public bool IsDead => HealthManager.isDead;
    }

    public static void OnSceneChanged(Scene scene)
    {
        if (Gm == null)
            return;

        var rootGameObjects = scene.GetRootGameObjects();

        Enemies = rootGameObjects.Where(x => EnemyLayers.Contains(x.layer) || x.tag == "Boss")
            .Select(x => new EnemyInfo
            {
                GameObject = x,
                HealthManager = x.GetComponent<HealthManager>()
            })
            .ToList();
    }
}