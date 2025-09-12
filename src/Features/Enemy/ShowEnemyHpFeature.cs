using System.Linq;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace HKSC.Features.Enemy;

public class ShowEnemyHpFeature : FeatureBase
{
    public override ModPage Page => ModPage.Enemy;

    public readonly ConfigObject<bool> ShowHp = CfgManager.Create("ShowEnemyHp::Enable", false).CreateToggleHotkey();

    [CanBeNull] private Camera _mainCamera;
    private GameObject _hpCanvasGo;
    private Canvas _canvas;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Info");
        ShowHp.Value = GUILayout.Toggle(ShowHp, "Show Enemy HP");
        UiUtils.EndCategory();
    }

    private void AttachHealthTextComponent(EnemyManager.EnemyInfo info)
    {
        if (_canvas == null)
        {
            _hpCanvasGo = new GameObject("EnemyHpCanvas");
            _canvas = _hpCanvasGo.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _hpCanvasGo.AddComponent<CanvasScaler>();
            _hpCanvasGo.AddComponent<GraphicRaycaster>();
        }

        var hpGo = new GameObject("HP");
        hpGo.transform.SetParent(_canvas.transform, false);
        var textComp = hpGo.AddComponent<Text>();
        textComp.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComp.fontSize = 20;
        textComp.fontStyle = FontStyle.Bold;
        textComp.color = Color.white;
        textComp.alignment = TextAnchor.MiddleCenter;
        info.HpTextComp = textComp;
    }

    private void UpdateHpText()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        if (_mainCamera == null)
            return;

        if (ShowHp)
        {
            foreach (var info in EnemyManager.Enemies.Where(info => info.GameObject != null && info.HealthManager != null))
            {
                if (info.HpTextComp == null)
                {
                    AttachHealthTextComponent(info);
                }

                info.HpTextComp.enabled = !info.HealthManager.isDead;
                info.HpTextComp.text = $"HP: {info.HealthManager.hp} ";
                var screenPos =
                    _mainCamera.WorldToScreenPoint(info.GameObject.transform.position - new Vector3(0, 1f, 0));
                info.HpTextComp.transform.position = screenPos;
            }
        }
        else
        {
            foreach (var info in EnemyManager.Enemies.Where(info => info.HpTextComp != null))
            {
                info.HpTextComp.enabled = false;
            }
        }
    }

    protected override void OnUpdate()
    {
        UpdateHpText();
    }
}