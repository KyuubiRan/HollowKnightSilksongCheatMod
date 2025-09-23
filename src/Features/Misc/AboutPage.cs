using HKSC.Managers;
using HKSC.Ui;
using HKSC.Utils;
using UnityEngine;

namespace HKSC.Features.Misc;

public class AboutPage : FeatureBase
{
    public override ModPage Page => ModPage.About;

    protected override void OnGui()
    {
        UiUtils.BeginCategory("feature.about.title".Translate());
        GUILayout.Label("feature.about.modVersion".Translate(ModConstants.Version));
        GUILayout.Label("feature.about.author".Translate(ModConstants.Author));
        GUILayout.Label("GitHub: " + ModConstants.ProjectSource);
        if (GUILayout.Button("feature.about.viewOnGithub".Translate()))
        {
            Application.OpenURL(ModConstants.ProjectSource);
        }

        UiUtils.EndCategory();

        UiUtils.BeginCategory("generic.language".Translate());
        GUILayout.Label("generic.language.current".Translate());

        var langs = LanguageManager.AvailableLanguages;
        const int langPerRow = 5;
        var total = langs.Count;
        var i = 0;
        GUILayout.BeginHorizontal();

        foreach (var lang in langs)
        {
            i++;
            if (GUILayout.Button(lang + " | " + LanguageManager.GetLanguageName(lang)))
                LanguageManager.CurrentLang.Value = lang;

            if (i % langPerRow != 0 || i == total) continue;

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
        }

        GUILayout.EndHorizontal();


        UiUtils.EndCategory();
    }
}