using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public static class EditorSFX
{
    public static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false, double startTime = 0)
    {
        StopAllClips();

        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayPreviewClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
            null
        );

        MethodInfo method2 = audioUtilClass.GetMethod("SetPreviewClipSamplePosition",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { typeof(AudioClip), typeof(int) },
            null
        );

        Debug.Log(method);

        int sampleStart = (int)Math.Ceiling(clip.samples * (startTime / clip.length));//do your calculation

        method.Invoke(
            null,
            new object[] { clip, startSample, loop }
        );

        method2.Invoke(null, new object[] { clip, sampleStart });
    }

    public static void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllPreviewClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { },
            null
        );

        Debug.Log(method);
        method.Invoke(
            null,
            new object[] { }
        );
    }
}