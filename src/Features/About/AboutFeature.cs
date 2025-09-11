using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.About;

public class AboutFeature : FeatureBase
{
    public override ModPage Page => ModPage.About;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("About");
        GUILayout.Label("Mod Version: " + ModConstants.Version);
        GUILayout.Label("Author: " + ModConstants.Author);
        GUILayout.Label("GitHub: " + ModConstants.ProjectSource);
        if (GUILayout.Button("View project on GitHub"))
        {
            Application.OpenURL(ModConstants.ProjectSource);
        }

        UiUtils.EndCategory();
    }
}