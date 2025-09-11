using System.Collections.Generic;
using HKSC.Ui;
using UnityEngine;

namespace HKSC.Features.Teleport;

public abstract class TeleportFeatureBase : FeatureBase
{
    public override ModPage Page => ModPage.Teleport;

    public virtual int MaxLogCount => 10;
    public bool EnableLog { set; get; }

    protected readonly List<TeleportPoint> Queue = [];

    public void LogTeleport(TeleportPoint teleport)
    {
        if (!EnableLog) return;
        if (!teleport.Valid) return;

        while (Queue.Count >= MaxLogCount)
            Queue.RemoveAt(Queue.Count - 1);

        Queue.Insert(0, teleport);
    }

    private static bool RenderTeleportItem(int index, TeleportPoint point)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label($"#{index} {point.SceneName} {point.Position}");
        if (GUILayout.Button("Teleport"))
            point.Teleport();

        var delete = GUILayout.Button("Delete");
        GUILayout.EndHorizontal();
        return delete;
    }

    private readonly List<TeleportPoint> _deleted = [];

    protected void RenderItems()
    {
        if (Queue.Count == 0)
            return;

        var i = 1;
        foreach (var point in Queue)
        {
            if (RenderTeleportItem(i++, point)) _deleted.Add(point);
        }

        foreach (var point in _deleted)
            Queue.Remove(point);
        _deleted.Clear();
    }
}