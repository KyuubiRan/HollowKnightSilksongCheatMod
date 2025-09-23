using System.Collections.Generic;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Teleport;

public class DeathTeleport : TeleportFeatureBase
{
    public override int MaxLogCount => 5;
    private readonly ConfigObject<bool> _enableLog = CfgManager.Create("DeathTeleport::EnableLog", false);
    private readonly ConfigObject<List<TeleportPoint>> _queue = CfgManager.Create("DeathTeleport::TeleportPoints", new List<TeleportPoint>());

    private readonly Hotkey _tpToLastDeath = Hotkey.Create(
        "DeathTeleport::TeleportToLastDeath",
        "hotkey.namespace.teleport",
        "hotkey.teleport.teleportToLastDeath",
        KeyCode.None,
        down =>
        {
            if (!down) return;

            var inst = FeatureManager.GetFeature<DeathTeleport>();
            if (inst is { Queue.Count: > 0 })
                inst.Queue[0].Teleport();
        });

    public override bool EnableLog => _enableLog;
    protected override List<TeleportPoint> Queue => _queue.Value;

    protected override void OnLogTeleport(TeleportPoint point)
    {
        _queue.FireChanged();
    }

    private int _deleteCount = 3;
    private float _clickDeleteTimer = 10;

    protected override void OnUpdate()
    {
        _clickDeleteTimer = _deleteCount == 3 ? 3f : _clickDeleteTimer - Time.unscaledDeltaTime;
    }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.teleport.death.title".Translate());

        _enableLog.Value = GUILayout.Toggle(_enableLog, "feature.teleport.death.enable".Translate());

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

        UiUtils.EndCategory();
    }
}