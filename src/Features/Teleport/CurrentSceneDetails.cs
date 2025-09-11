using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HKSC.Features.Teleport;

public class CurrentSceneDetails : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;

    private Vector2 _playerPos = Vector2.zero;
    private string _sceneName = string.Empty;

    public override ModPage Page => ModPage.Teleport;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Scene Info");
        
        GUILayout.Label($"Name: {_sceneName}");
        GUILayout.Label($"Player Pos: {_playerPos}");

        UiUtils.EndCategory();
    }

    protected override void OnUpdate()
    {
        if (Hc == null)
        {
            _playerPos = Vector2.zero;
            return;
        }
        
        _playerPos = Hc.transform.position;
    }

    protected override void OnStart()
    {
        SceneManager.activeSceneChanged += (_, newScene) => { _sceneName = newScene.name; };
    }
}