using GlobalEnums;
using HKSC.Accessor;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Player;

public class EnhancedAttack : FeatureBase
{
    private static HeroController Hc => HeroController.UnsafeInstance;
    public override ModPage Page => ModPage.Player;

    private readonly ConfigObject<bool> _enableEnhancedAttack = CfgManager.Create("EnhancedAttack::EnableEnhancedAttack", false)
                                                                          .CreateToggleHotkey("hotkey.namespace.player.enhancedAttack", "hotkey.generic.toggle");

    private readonly Hotkey _attackForwardKey = Hotkey.Create("EnhancedAttack::AttackForwardKey",
                                                              "hotkey.namespace.player.enhancedAttack", "hotkey.player.enhancedAttack.attackForward", KeyCode.None,
                                                              down =>
                                                              {
                                                                  if (!down || Hc == null) return;
                                                                  HeroControllerAccessor.AttackMethod.Invoke(Hc, [AttackDirection.normal]);
                                                              });

    private readonly Hotkey _attackDownwardKey = Hotkey.Create("EnhancedAttack::AttackDownwardKey",
                                                               "hotkey.namespace.player.enhancedAttack", "hotkey.player.enhancedAttack.attackDownward", KeyCode.None,
                                                               down =>
                                                               {
                                                                   if (!down || Hc == null) return;
                                                                   HeroControllerAccessor.AttackMethod.Invoke(Hc, [AttackDirection.downward]);
                                                               });

    private readonly Hotkey _attackUpwardKey = Hotkey.Create("EnhancedAttack::AttackUpwardKey",
                                                             "hotkey.namespace.player.enhancedAttack", "hotkey.player.enhancedAttack.attackUpward", KeyCode.None,
                                                             down =>
                                                             {
                                                                 if (!down || Hc == null) return;
                                                                 HeroControllerAccessor.AttackMethod.Invoke(Hc, [AttackDirection.upward]);
                                                             });

    private readonly Hotkey _attackBackwardKey = Hotkey.Create("EnhancedAttack::AttackBackwardKey",
                                                               "hotkey.namespace.player.enhancedAttack", "hotkey.player.enhancedAttack.attackBackward", KeyCode.None,
                                                               down =>
                                                               {
                                                                   if (!down || Hc == null) return;
                                                                   if (Hc.cState.facingRight)
                                                                   {
                                                                       Hc.FaceLeft();
                                                                   }
                                                                   else
                                                                   {
                                                                       Hc.FaceRight();
                                                                   }

                                                                   HeroControllerAccessor.AttackMethod.Invoke(Hc, [AttackDirection.normal]);
                                                               });

    private readonly Hotkey _attackRightKey = Hotkey.Create("EnhancedAttack::AttackRightKey",
                                                            "hotkey.namespace.player.enhancedAttack", "hotkey.player.enhancedAttack.attackRight", KeyCode.None,
                                                            down =>
                                                            {
                                                                if (!down || Hc == null) return;
                                                                if (!Hc.cState.facingRight)
                                                                {
                                                                    Hc.FaceRight();
                                                                }

                                                                HeroControllerAccessor.AttackMethod.Invoke(Hc, [AttackDirection.normal]);
                                                            });

    private readonly Hotkey _attackLeftKey = Hotkey.Create("EnhancedAttack::AttackLeftKey",
                                                           "hotkey.namespace.player.enhancedAttack", "hotkey.player.enhancedAttack.attackLeft", KeyCode.None, down =>
                                                           {
                                                               if (!down || Hc == null) return;
                                                               if (Hc.cState.facingRight)
                                                               {
                                                                   Hc.FaceLeft();
                                                               }

                                                               HeroControllerAccessor.AttackMethod.Invoke(Hc, [AttackDirection.normal]);
                                                           });

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feat.player.enhancedAttack.title".Translate());
        _enableEnhancedAttack.Value = GUILayout.Toggle(_enableEnhancedAttack, "feature.generic.enable".Translate());
        if (_enableEnhancedAttack)
            GUILayout.Label("feat.player.enhancedAttack.desc".Translate());
        UiUtils.EndCategory();
    }
}