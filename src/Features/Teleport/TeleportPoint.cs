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
        Valid = Gm?.IsGameplayScene() == true,
        GateName = Gm?.entryGateName ?? "dreamGate"
    };

    public string SceneName { get; set; }
    public Vector2 Position { get; set; }
    public bool Valid { get; set; }
    public string GateName { get; set; }

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
            Gm.BeginSceneTransition(new GameManager.SceneLoadInfo
            {
                SceneName = SceneName,
                EntryGateName = GateName,
                PreventCameraFadeOut = true,
                WaitForSceneTransitionCameraFade = false,
                AlwaysUnloadUnusedAssets = true,
                Visualization = GameManager.SceneLoadVisualizations.Default,
            });
        }

        Hc.transform.position = Position;
        Hc.ResetState();
        Hc.AcceptInput();

        // After teleport
        Hc.CrossStitchInvuln();
        return true;
    }
}