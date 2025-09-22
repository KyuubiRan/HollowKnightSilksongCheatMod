using System.Collections.Generic;
using System.Linq;
using HKSC.Managers;
using HKSC.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HKSC.Features.Teleport;

public abstract class TeleportFeatureBase : FeatureBase
{
    public override ModPage Page => ModPage.Teleport;

    public virtual int MaxLogCount => 10;
    public abstract bool EnableLog { get; }

    protected abstract List<TeleportPoint> Queue { get; }

    public bool LogTeleport(TeleportPoint teleport)
    {
        if (!EnableLog) return false;
        if (!teleport.Valid) return false;

        while (Queue.Count >= MaxLogCount)
            Queue.RemoveAt(Queue.Count - 1);

        Queue.Insert(0, teleport);
        OnLogTeleport(teleport);

        return true;
    }

    protected void ClearAll()
    {
        Queue.Clear();
        OnLogTeleport(null);
    }

    protected void ClearNearly(float distance)
    {
        if (Queue.Count <= 1) return;

        var set = new HashSet<TeleportPoint>();

        for (var i = 0; i < Queue.Count; i++)
        {
            for (var j = i + 1; j < Queue.Count; j++)
            {
                if (Queue[i].SceneName != Queue[j].SceneName) continue;
                if (Vector2.Distance(Queue[i].Position, Queue[j].Position) > distance) continue;

                set.Add(Queue[j]);
            }
        }

        foreach (var tp in set)
            Queue.Remove(tp);

        if (set.Count > 0)
            OnLogTeleport(null);
    }

    protected virtual void OnLogTeleport(TeleportPoint point)
    {
    }

    private static bool RenderTeleportItem(int index, TeleportPoint point)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label($"#{index} {point.SceneName} {point.Position}");
        if (GUILayout.Button(SceneManager.GetActiveScene().name == point.SceneName
                ? "feature.teleport.teleport".Translate()
                : "feature.teleport.toScene".Translate()))
            point.Teleport();

        var delete = GUILayout.Button("feature.generic.delete".Translate());
        GUILayout.EndHorizontal();
        return delete;
    }

    private readonly List<TeleportPoint> _deleted = [];

    protected void RenderItems()
    {
        if (Queue is null or { Count: 0 })
            return;

        var i = 1;
        var changed = false;
        foreach (var point in Queue.Where(point => RenderTeleportItem(i++, point)))
        {
            _deleted.Add(point);
            changed = true;
        }

        foreach (var point in _deleted)
            Queue.Remove(point);
        _deleted.Clear();

        if (changed)
            OnLogTeleport(null);
    }
}