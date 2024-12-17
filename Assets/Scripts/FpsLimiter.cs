#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]

// scipt per limitare gli fps nell'editor

public static class FpsLimiter
{
    static FpsLimiter()
    {
        Application.targetFrameRate = 120;
    }
}
#endif