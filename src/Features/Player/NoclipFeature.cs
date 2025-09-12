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
    
    public override ModPage Page => ModPage.Player;
    
    public readonly ConfigObject<bool> IsEnabled = CfgManager.Create("Noclip::Enable", false)
        .CreateToggleHotkey("Toggle Noclip")
        .AddOnChangedListener(x =>
        {
            if (Hc == null) return;
            
            Hc.AffectedByGravity(!x);
            var rigidbody2D = HeroControllerAccessor.Rb2dField(Hc);
            var collider2D = HeroControllerAccessor.Col2dField(Hc);
            if (x)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.isKinematic = true;
                collider2D.enabled = false;
            }
            else
            {
                rigidbody2D.isKinematic = false;
                collider2D.enabled = true;
            }
        });
    
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

    protected override void OnUpdate()
    {
        if (Hc == null)
            return;
        
        if (!IsEnabled)
            return;
        
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (move.sqrMagnitude > 0.01f)
        {
            move.Normalize();
            var delta = move * (Speed * Time.deltaTime);
            Hc.transform.position += (Vector3)delta;
        }
    }
}