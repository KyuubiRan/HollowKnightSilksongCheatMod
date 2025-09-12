using System.Collections.Generic;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Teleport;

public class DeathTeleport : TeleportFeatureBase
{
    public override int MaxLogCount => 3;
    private readonly ConfigObject<bool> _enableLog = CfgManager.Create("DeathTeleport::EnableLog", false);
    private readonly ConfigObject<List<TeleportPoint>> _queue = CfgManager.Create("DeathTeleport::TeleportPoints", new List<TeleportPoint>());

    public override bool EnableLog => _enableLog;
    protected override List<TeleportPoint> Queue => _queue.Value;

    protected override void OnLogTeleport(TeleportPoint point)
    {
        _queue.FireChanged();
    }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Death Teleport");

        _enableLog.Value = GUILayout.Toggle(_enableLog, "Enable Death Log");

        RenderItems();

        UiUtils.EndCategory();
    }
}