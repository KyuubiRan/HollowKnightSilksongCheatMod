using GlobalEnums;
using HKSC.Accessor;
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
        Position = Hc?.transform.position ?? Vector2.zero
    };

    public string SceneName { get; set; }
    public Vector2 Position { get; set; }

    public bool Teleport()
    {
        if (Gm == null) return false;
        if (Hc == null) return false;
        if (!Gm.IsGameplayScene()) return false;

        var curScene = SceneManager.GetActiveScene();
        if (curScene.name != SceneName)
        {
            Gm.NoLongerFirstGame();
            Gm.SaveLevelState();
            Gm.SetState(GameState.EXITING_LEVEL);

            Hc.LeaveScene();
            Gm.LoadScene(SceneName);
            Hc.EnterSceneDreamGate();
        }

        Hc.transform.position = Position;
        Hc.ResetState();

        // After teleport
        HeroControllerAccessor.StartInvulnerableMethod.Invoke(Hc, [TeleportInvulnerableTime]);
        return true;
    }
}