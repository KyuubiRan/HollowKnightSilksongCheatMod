using GlobalEnums;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

namespace HKSC.Features.Teleport;

public class TeleportPoint
{
    private const float TeleportInvulnerableTime = 3.0f;
    private static HeroController Hc => HeroController.UnsafeInstance;
    private static GameManager Gm => GameManager.UnsafeInstance;

    public static TeleportPoint Current => new()
    {
        SceneName = SceneManager.GetActiveScene().name,
        Position = Hc?.transform.position ?? Vector2.zero,
        Valid = Gm?.IsGameplayScene() == true
    };

    public string SceneName { get; set; }
    public Vector2 Position { get; set; }
    public bool Valid { get; private set; }

    public bool Teleport()
    {
        if (!Valid) return false;
        if (Gm == null) return false;
        if (Hc == null) return false;
        if (!Gm.IsGameplayScene()) return false;
        if (GameManager.IsWaitingForSceneReady) return false;

        Hc.EnterWithoutInput(false);
        var curScene = SceneManager.GetActiveScene();
        if (curScene.name != SceneName)
        {
            Gm.NoLongerFirstGame();
            Gm.SaveLevelState();
            Hc.proxyFSM.SendEvent("HeroCtrl-LeavingScene");
            Hc.LeaveScene();
            Gm.SetState(GameState.EXITING_LEVEL);
            Gm.cameraCtrl.FreezeInPlace();
            Gm.LoadScene(SceneName);
            Hc.EnterSceneDreamGate();

            Hc.transform.position = Position;
        }

        Hc.transform.position = Position;
        Hc.ResetState();
        Hc.AcceptInput();

        // After teleport
        Hc.CrossStitchInvuln();
        return true;
    }
}