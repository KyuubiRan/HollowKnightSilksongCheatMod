using UnityEngine;

namespace HKSC.Utils;

public static class UiUtils
{
    private static readonly GUIStyle CenterTitleStyle = new(GUI.skin.label)
    {
        alignment = TextAnchor.MiddleCenter,
        fontSize = 18,
        fontStyle = FontStyle.Bold
    };

    private static readonly GUIStyle CenterSliderValueStyle = new(GUI.skin.label)
    {
        alignment = TextAnchor.MiddleCenter,
        fontSize = 16,
    };

    private static readonly GUIStyle InputIntLabelStyle = new(GUI.skin.label)
    {
        fontSize = 16,
        fixedWidth = 100,
    };

    private static readonly GUIStyle SliderButtonArrowStyle = new(GUI.skin.button)
    {
        fontSize = 16,
        fixedWidth = 30,
    };    

    public static void BeginCategory(string title)
    {
        GUILayout.Label($"· {title} ·", CenterTitleStyle);
    }

    public static void EndCategory()
    {
        GUILayout.Space(10);
    }

    public static string InputInt(ref int value, string text = "0", string label = "")
    {
        var labeled = !string.IsNullOrEmpty(label);

        if (labeled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, InputIntLabelStyle);
        }

        var after = GUILayout.TextField(text);

        if (labeled)
        {
            GUILayout.EndHorizontal();
        }

        if (string.IsNullOrEmpty(after))
        {
            value = 0;
            return "";
        }

        return int.TryParse(after, out value) ? after : text;
    }

    public static float Slider(float value, float min, float max, float step = 1f, string valueFormat = "{0:0.00}")
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", SliderButtonArrowStyle))
        {
            value -= step;
            if (value < min) value = min;
        }

        var after = GUILayout.HorizontalSlider(value, min, max);
        value = after;
        
        if (GUILayout.Button(">", SliderButtonArrowStyle))
        {
            value += step;
            if (value > max) value = max;
        }

        GUILayout.EndHorizontal();
        GUILayout.Label(string.Format(valueFormat, value), CenterSliderValueStyle);
        return value;
    }
}