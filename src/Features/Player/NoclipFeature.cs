using HKSC.Accessor;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class NoclipFeature : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    private static InputHandler Ih => InputHandler.UnsafeInstance;

    public override ModPage Page => ModPage.Player;

    public readonly ConfigObject<bool> IsEnabled = CfgManager.Create("Noclip::Enable", false)
        .CreateToggleHotkey("Toggle Noclip");

    public readonly ConfigObject<float> Speed = CfgManager.Create("Noclip::Speed", 5f);

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Noclip");
        IsEnabled.Value = GUILayout.Toggle(IsEnabled, "Enable");
        if (IsEnabled)
        {
            Speed.Value = UiUtils.Slider(Speed, 5f, 100f, 5f, valueFormat: "Speed: {0:0.0}");
        }

        UiUtils.EndCategory();
    }

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    protected override void OnUpdate()
    {
        if (Hc == null || Ih == null)
            return;

        _rigidbody2D ??= HeroControllerAccessor.Rb2dField(Hc);
        _collider2D ??= HeroControllerAccessor.Col2dField(Hc);

        if (IsEnabled)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.isKinematic = true;
            _collider2D.enabled = false;
        }
        else
        {
            _rigidbody2D.isKinematic = false;
            _collider2D.enabled = true;
        }

        if (!IsEnabled) return;

        var vertical = 0f;
        var horizontal = 0f;
        if (Ih.inputActions.Up.IsPressed || Ih.inputActions.RsUp.IsPressed)
            vertical = 1f;
        else if (Ih.inputActions.Down.IsPressed || Ih.inputActions.RsDown.IsPressed)
            vertical = -1f;
        if (Ih.inputActions.Left.IsPressed || Ih.inputActions.RsLeft.IsPressed)
            horizontal = -1f;
        else if (Ih.inputActions.Right.IsPressed || Ih.inputActions.RsRight.IsPressed)
            horizontal = 1f;

        var move = new Vector2(horizontal, vertical);
        if (move.sqrMagnitude > 0.01f)
        {
            move.Normalize();
            var delta = move * (Speed * Time.deltaTime);
            Hc.transform.position += (Vector3)delta;
        }
    }
}