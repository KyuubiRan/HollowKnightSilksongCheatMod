using System.Reflection;
using HarmonyLib;

namespace HKSC.Accessor;

public class CurrencyObjectBaseAccessor
{
    public static readonly MethodInfo CurrencyTypeGetter = AccessTools.PropertyGetter(typeof(CurrencyObjectBase), "CurrencyType");
}