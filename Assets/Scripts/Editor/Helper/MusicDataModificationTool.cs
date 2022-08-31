using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicDataModificationTool : SoundPlayerEditor
{
    private Vector2 scollPosition = Vector2.zero;

    private readonly float pixelFocus = 1000f;
    private float maxPixel = 0f; // 10pixel 당 0.01초를 나타낸다
    private float drawedPixels = 0f;

    [MenuItem("Helper/MusicDataModificationTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MusicDataModificationTool));
    }

    private void OnGUI()
    {
        GUILayout.Label("MusicDataModificationTool", EditorStyles.boldLabel);

        ParentsOnGUI();

        if (musicBoxDataSO != null)
        {
            drawedPixels = 0f;
            maxPixel = musicBoxDataSO.musicBox.clipLength * pixelFocus;

            scollPosition = EditorGUILayout.BeginScrollView(scollPosition);

            SetNodes();
            
            EditorGUILayout.EndScrollView();
        }
    }

    private void SetNodes()
    {
        MusicData curData = null;
        double prevNodeTime = 0d;
        float height = 0f;

        for(int i = 0; i < musicBoxDataSO.dataList.Count; i++)
        {
            curData = musicBoxDataSO.dataList[i];
            height = (float)((curData.badTime.minTime + curData.badTime.maxTime) * pixelFocus) > 0 ? (float)((curData.badTime.minTime + curData.badTime.maxTime) * pixelFocus) : 10f;

            DrawVerticlaLine(curData.referenceTime - prevNodeTime - (float)curData.badTime.minTime);
            GUILayout.Button("Node", GUILayout.Width(200), GUILayout.Height(height));

            prevNodeTime = curData.referenceTime;
        }
    }

    private void DrawVerticlaLine(double time)
    {
        GUILayout.BeginVertical();

        // BeginArea를 써서 Timeline을 구현할것

        GUILayout.Label("", GUILayout.Width(1f), GUILayout.Height((float)(time * pixelFocus)));

        GUILayout.EndVertical();
    }
}
