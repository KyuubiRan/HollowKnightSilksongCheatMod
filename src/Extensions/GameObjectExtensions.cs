using UnityEngine;

namespace HKSC.Extensions;

public static class GameObjectExtensions
{
    public static GameObject Attach(this GameObject thiz, GameObject parent, bool worldPositionStays = false)
    {
        thiz.transform.SetParent(parent.transform, worldPositionStays);
        return thiz;
    }
}