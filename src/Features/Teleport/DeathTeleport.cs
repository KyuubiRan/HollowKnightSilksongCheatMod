using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Teleport;

public class DeathTeleport : TeleportFeatureBase
{
    public override int MaxLogCount => 3;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("Death Teleport");

        EnableLog = GUILayout.Toggle(EnableLog, "Enable Death Log");

        RenderItems();

        UiUtils.EndCategory();
    }
}