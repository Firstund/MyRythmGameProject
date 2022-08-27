using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataHelper : EditorWindow
{
    public MusicBoxDataSO musicBoxDataSO = null;

    [MenuItem("Helper/MusicDataHelper")]
    public static void ShowStageCheatWindow()
    {
        GetWindow(typeof(MusicDataHelper));
    }

}
