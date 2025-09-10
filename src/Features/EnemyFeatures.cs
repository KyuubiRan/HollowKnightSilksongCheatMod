using System.Linq;
using HKSC.Managers;
using HKSC.Ui;
using HKSC.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace HKSC.Features;

public class EnemyFeatures : FeatureBase
{
    public override ModPage Page => ModPage.Enemy;

    public bool ShowHp { get; private set; }
    [CanBeNull] private Camera _mainCamera;
    private GameObject _hpCanvesGo;
    private Canvas _canvas;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Info");
        ShowHp = GUILayout.Toggle(ShowHp, "Show Enemy HP");
        UiUtils.EndCategory();
    }

    private void AttachHealthTextComponent(EnemyManager.EnemyInfo info)
    {
        if (_canvas == null)
        {
            _hpCanvesGo = new GameObject("EnemyHpCanvas");
            _canvas = _hpCanvesGo.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _hpCanvesGo.AddComponent<CanvasScaler>();
            _hpCanvesGo.AddComponent<GraphicRaycaster>();
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