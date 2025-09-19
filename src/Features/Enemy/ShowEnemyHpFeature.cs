using System.Collections.Generic;
using System.Linq;
using HKSC.Extensions;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HKSC.Features.Enemy;

public class ShowEnemyHpFeature : FeatureBase
{
    public override ModPage Page => ModPage.Enemy;

    public readonly ConfigObject<bool> ShowHp = CfgManager.Create("EnemyInfo::Enable", false)
                                                          .CreateToggleHotkey("hotkey.namespace.enemy", "hotkey.enemy.info.toggleShowEnemyHp");

    private static readonly Dictionary<HealthManager, Text> TextDict = new();

    [CanBeNull] private Camera _mainCamera;
    private GameObject _hpCanvasGo;
    private Canvas _canvas;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.enemy.info.title".Translate());
        ShowHp.Value = GUILayout.Toggle(ShowHp, "feature.enemy.info.showHp".Translate());
        UiUtils.EndCategory();
    }

    private Text AttachHealthTextComponent(HealthManager hm)
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
        TextDict[hm] = textComp;

        return textComp;
    }

    private void UpdateHpText()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;

        if (_mainCamera == null)
            return;

        foreach (var hm in HealthManager.EnumerateActiveEnemies().Where(x => !TextDict.ContainsKey(x)))
        {
            AttachHealthTextComponent(hm);
        }

        foreach (var (hm, text) in TextDict)
        {
            if (!ShowHp)
            {
                text.enabled = false;
                continue;
            }

            if (hm == null)
            {
                Object.Destroy(text.gameObject);
                TextDict.Remove(hm);
                continue;
            }

            text.enabled = !hm.isDead;
            text.text = $"HP: {hm.hp}";
            var screenPos =
                _mainCamera.WorldToScreenPoint(hm.gameObject.transform.position - new Vector3(0, 1f, 0));
            text.transform.position = screenPos;
        }
    }

    protected override void OnStart()
    {
        SceneManager.activeSceneChanged += (_, _) => { TextDict.Clear(); };
    }

    protected override void OnUpdate()
    {
        UpdateHpText();
    }
}