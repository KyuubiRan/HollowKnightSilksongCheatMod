using System.Collections.Generic;
using System.Text;
using HKSC.Misc;
using Newtonsoft.Json.Linq;

namespace HKSC.Managers;

public static class LanguageManager
{
    public static readonly ConfigObject<string> CurrentLang = CfgManager.Create("Generic::CurrentLang", "en_us");

    private static readonly Dictionary<string, Dictionary<string, string>> LangMap = new();

    public static IReadOnlyCollection<string> AvailableLanguages => LangMap.Keys;

    public static string Get(string key)
    {
        if (LangMap.TryGetValue(CurrentLang, out var langDict))
        {
            if (langDict.TryGetValue(key, out var value))
            {
                return value;
            }
        }

        // Fallback to en_us
        if (LangMap.TryGetValue("en_us", out var fallbackDict))
        {
            if (fallbackDict.TryGetValue(key, out var fallbackValue))
            {
                return fallbackValue;
            }
        }

        return key;
    }

    public static string Get(string key, params object[] args)
    {
        var format = Get(key);
        return string.Format(format, args);
    }

    public static string Translate(this string key) => Get(key);
    public static string Translate(this string key, params object[] args) => Get(key, args);

    public static void Init()
    {
#if USE_FILE_LANGUAGE
        var assemblyPath = typeof(CfgManager).Assembly.Location;
        var dir = Path.GetDirectoryName(assemblyPath);
        var langFile = Path.Combine(dir ?? throw new InvalidOperationException(), "language.json");
        var json = File.ReadAllText(langFile);
#else
        var langFileData = ModResources.Language;
        if (langFileData[0] == 0xEF && langFileData[1] == 0xBB && langFileData[2] == 0xBF)
            // Skip BOM
            langFileData = langFileData[3..];
        var json = Encoding.UTF8.GetString(langFileData);
#endif
        var jobj = JObject.Parse(json);
        foreach (var pair in jobj)
        {
            var langDict = new Dictionary<string, string>();
            foreach (var item in (JObject)pair.Value)
            {
                langDict[item.Key] = item.Value.ToString();
            }

            LangMap[pair.Key] = langDict;
        }

        if (!LangMap.ContainsKey(CurrentLang))
        {
            CurrentLang.Value = "en_us";
            CfgManager.FireChanged(CurrentLang);
        }
    }
};