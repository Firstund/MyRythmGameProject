using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class NodeDataModificationTool : EditorWindow
{
    public static MusicData musicData = null;

    private void OnGUI()
    {
        if(musicData == null)
        {
            GUILayout.Label("No Music Data!", EditorStyles.boldLabel);

            return;
        }

        // 이제 여기서 MusicData를 수정할 수 있게 해주면 된다

        GUILayout.BeginVertical();

        musicData.badTime.minTime = EditorGUILayout.DoubleField("Bad_MinTime", musicData.badTime.minTime);

        GUILayout.EndVertical();
    }

    public static void SetMusicData(MusicData md)
    {
        musicData = md;
    }


}
