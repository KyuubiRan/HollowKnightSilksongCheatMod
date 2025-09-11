using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Teleport;

public class CustomTeleport : TeleportFeatureBase
{
    public override int MaxLogCount => int.MaxValue;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Custom Teleport");
        if (GUILayout.Button("Log")) LogTeleport(TeleportPoint.Current);

        RenderItems();

        UiUtils.EndCategory();
    }
}