using System.Collections.Generic;
using HKSC.Managers;
using HKSC.Misc;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Teleport;

public class CustomTeleport : TeleportFeatureBase
{
    public override int MaxLogCount => int.MaxValue;
    public override bool EnableLog => true;
    private readonly ConfigObject<List<TeleportPoint>> _queue = CfgManager.Create("CustomTeleport::TeleportPoints", new List<TeleportPoint>());

    protected override List<TeleportPoint> Queue => _queue.Value;

    protected override void OnLogTeleport(TeleportPoint point)
    {
        _queue.FireChanged();
    }

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Custom Teleport");
        if (GUILayout.Button("Log")) LogTeleport(TeleportPoint.Current);

        RenderItems();

        UiUtils.EndCategory();
    }
}