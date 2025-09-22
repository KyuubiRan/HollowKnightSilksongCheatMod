using System.Collections;
using GlobalEnums;
using HKSC.Accessor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

namespace HKSC.Features.Teleport;

public class TeleportPoint
{
    private const float TeleportInvulnerableTime = 5.0f;
    private static HeroController Hc => HeroController.UnsafeInstance;
    private static GameManager Gm => GameManager.UnsafeInstance;

    public static TeleportPoint Current => new()
    {
        SceneName = SceneManager.GetActiveScene().name,
        Position = Hc?.transform.position ?? Vector2.zero,
        Valid = Gm?.IsGameplayScene() == true,
        GateName = Gm?.entryGateName ?? "dreamGate"
    };

    public string SceneName { get; set; }
    public Vector2 Position { get; set; }
    public bool Valid { get; set; }
    public string GateName { get; set; }


    private static Coroutine _unpauseCoroutine;

    private static IEnumerator UnpauseOnSceneLoaded()
    {
        if (GameManager.IsWaitingForSceneReady)
            yield return null;

        yield return new WaitForSecondsRealtime(0.3f);
        if (Gm.ui.menuState == MainMenuState.PAUSE_MENU)
            Gm.ui.HideMenuInstant(Gm.ui.pauseMenuScreen);
        Gm.isPaused = false;
        Gm.inputHandler.PreventPause();
        Gm.ui.SetState(UIState.PLAYING);
        GameManagerAccessor.SetPausedStateMethod.Invoke(Gm, [false]);
        Gm.SetState(GameState.PLAYING);
        Hc.UnPause();
        MenuButtonList.ClearAllLastSelected();
        yield return new WaitForSecondsRealtime(0.3f);
        Gm.inputHandler.AllowPause();

        _unpauseCoroutine = null;
    }

    public override string ToString() => $"{SceneName} | X={Position.x:F2},Y={Position.y:F2}";

    public bool Teleport()
    {
        if (!Valid) return false;
        if (!Gm) return false;
        if (!Hc) return false;
        if (!Gm.IsGameplayScene()) return false;
        if (GameManager.IsWaitingForSceneReady) return false;

        Hc.EnterWithoutInput(false);
        var curScene = SceneManager.GetActiveScene();
        if (curScene.name != SceneName)
        {
            if (_unpauseCoroutine != null)
                Gm.StopCoroutine(_unpauseCoroutine);

            Gm.BeginSceneTransition(new GameManager.SceneLoadInfo
            {
                SceneName = SceneName,
                EntryGateName = GateName,
                PreventCameraFadeOut = true,
                WaitForSceneTransitionCameraFade = false,
                AlwaysUnloadUnusedAssets = true,
                Visualization = GameManager.SceneLoadVisualizations.Default,
            });

            _unpauseCoroutine = Gm.StartCoroutine(UnpauseOnSceneLoaded());
        }

        Hc.transform.position = Position;
        GameManagerAccessor.SetPausedStateMethod.Invoke(Gm, [false]);
        Hc.ResetState();

        HeroControllerAccessor.StartInvulnerableMethod.Invoke(Hc, [TeleportInvulnerableTime]);
        return true;
    }
}