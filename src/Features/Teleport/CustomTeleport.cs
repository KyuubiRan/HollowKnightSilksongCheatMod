using System.Collections.Generic;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Teleport;

public class CustomTeleport : TeleportFeatureBase
{
    public override int MaxLogCount => int.MaxValue;
    public override bool EnableLog => true;
    private readonly ConfigObject<List<TeleportPoint>> _queue = CfgManager.Create("CustomTeleport::TeleportPoints", new List<TeleportPoint>());
    protected override List<TeleportPoint> Queue => _queue.Value;

    private readonly Hotkey _logHotkey = Hotkey.Create("CustomTeleport::LogCurrentPositionHotkey", "hotkey.namespace.teleport",
        "hotkey.teleport.logCurrentPosition", KeyCode.None, down =>
        {
            if (!down) return;

            var inst = FeatureManager.GetFeature<CustomTeleport>();
            if (inst == null) return;
            var current = TeleportPoint.Current;
            if (inst.LogTeleport(current))
                ModMainUi.Instance.ShowToast("ui.toast.logCurrentTeleportPointInfo".Translate(current), 5f);
        });

    private string _clearNearlyText = "10.0";
    private float _clearNearly = 10.0f;

    private int _deleteCount = 3;
    private float _clickDeleteTimer = 5f;

    protected override void OnLogTeleport(TeleportPoint point)
    {
        _queue.FireChanged();
    }
    
    protected override void OnUpdate()
    {
        _clickDeleteTimer = _deleteCount == 3 ? 3f : _clickDeleteTimer - Time.unscaledDeltaTime;
    }


    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.teleport.custom.title".Translate());
        if (GUILayout.Button("feature.teleport.custom.log".Translate()))
            LogTeleport(TeleportPoint.Current);

        RenderItems();
        
        if (_clickDeleteTimer < 0) _deleteCount = 3;
        if (GUILayout.Button("feature.teleport.clearAll".Translate(_deleteCount)))
        {
            if (--_deleteCount < 1)
            {
                ClearAll();
                _deleteCount = 3;
            }
        }

        GUILayout.BeginHorizontal();
        _clearNearlyText = UiUtils.InputFloat(ref _clearNearly, _clearNearlyText, "feature.teleport.clearNearly".Translate());
        if (GUILayout.Button("feature.generic.execute".Translate()))
            ClearNearly(_clearNearly);
        GUILayout.EndHorizontal();

        UiUtils.EndCategory();
    }
}