#nullable enable

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HKSC.Managers;

public static class EnemyManager
{
    private static GameManager Gm => GameManager.UnsafeInstance;

    public static readonly HashSet<EnemyInfo> Enemies = [];

    public delegate void OnEnemiesLoaded(IReadOnlyCollection<EnemyInfo> info);

    public static event OnEnemiesLoaded? OnEnemiesLoadedEvent;

    public static void OnUpdate()
    {
        Enemies.RemoveWhere(x => x.GameObject == null);
    }

    public class EnemyInfo
    {
        public GameObject? GameObject;
        public HealthManager? HealthManager;

        public Text HpTextComp = null!;
    }

    public static void OnSceneChanged(Scene scene)
    {
        if (Gm == null)
            return;

        Enemies.Clear();
        OnEnemiesLoadedEvent?.Invoke(Enemies);
    }
}