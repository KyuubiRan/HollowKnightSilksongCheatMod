using System.Text.RegularExpressions;
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
        fixedWidth = 150,
    };   
    
    private static readonly GUIStyle InputFloatLabelStyle = new(GUI.skin.label)
    {
        fontSize = 16,
        fixedWidth = 150,
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

    private static readonly Regex IntRegex = new(@"^-?\d*");

    public static string InputInt(ref int value, string text = "0", string label = "", float fixedWidth = 100)
    {
        var labeled = !string.IsNullOrEmpty(label);

        if (labeled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, InputIntLabelStyle);
        }

        var after = GUILayout.TextField(text, new GUIStyle(GUI.skin.textField)
        {
            fontSize = 16,
            fixedWidth = fixedWidth,
        });
        after = IntRegex.Match(after).Value;

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

    private static readonly Regex FloatRegex = new(@"^-?\d*\.?\d*");

    public static string InputFloat(ref float value, string text = "0.0", string label = "")
    {
        var labeled = !string.IsNullOrEmpty(label);

        if (labeled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, InputFloatLabelStyle);
        }

        var after = GUILayout.TextField(text);
        after = FloatRegex.Match(after).Value;

        if (labeled)
        {
            GUILayout.EndHorizontal();
        }

        if (string.IsNullOrEmpty(after))
        {
            value = 0f;
            return "";
        }

        return float.TryParse(after.TrimEnd('.'), out value) ? after : text;
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

    public static int SliderInt(int value, int min, int max, int step = 1, string valueFormat = "{0:0}")
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<", SliderButtonArrowStyle))
        {
            value -= step;
            if (value < min) value = min;
        }

        var after = GUILayout.HorizontalSlider(value, min, max);
        value = (int)after;

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