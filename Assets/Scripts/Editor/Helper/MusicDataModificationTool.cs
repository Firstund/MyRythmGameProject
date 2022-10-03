using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicDataModificationTool : SoundPlayerEditor
{
    private Vector2 scollPosition = Vector2.zero;
    private Vector2 timeLineScrollPosition = Vector2.zero;

    private readonly float pixelFocus = 200f;
    private float maxPixel = 0f; // 10pixel 당 0.01초를 나타낸다
    private float drawedPixels = 0f;

    [MenuItem("Helper/MusicDataModificationTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MusicDataModificationTool));
    }

    protected override void OnGUI()
    {
        GUILayout.Label("MusicDataModificationTool", EditorStyles.boldLabel);

        base.OnGUI();

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

        DrawTimeLine(musicBoxDataSO.musicBox.clipLength);

        for (int i = 0; i < musicBoxDataSO.dataList.Count; i++)
        {
            curData = musicBoxDataSO.dataList[i];
            height = (float)((curData.badTime.minTime + curData.badTime.maxTime) * pixelFocus) > 0 ? (float)((curData.badTime.maxTime - curData.badTime.minTime) * pixelFocus) : 15f;

            GUILayout.BeginVertical();

            GUILayout.Space((float)curData.badTime.minTime * pixelFocus);

            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();

            GUILayout.Space(51f);

            GUILayout.Button("Node", GUILayout.Width(50), GUILayout.Height(height));

            GUILayout.EndHorizontal();

            prevNodeTime = curData.referenceTime;
        }
    }

    private void DrawTimeLine(float time)
    {
        // 최적화작업하자
        Rect rect = new Rect(10, 10, 80, Screen.height + scollPosition.y);

        GUILayout.BeginVertical();
        GUILayout.BeginArea(rect);
        EditorGUILayout.BeginScrollView(scollPosition, new GUIStyle(), new GUIStyle());
        //GUILayout.Box("", GUILayout.Width(rect.width), GUILayout.Height(rect.height));

        // BeginArea를 써서 Timeline을 구현할것

        float ZDZ = 0.0f;
        int Z = 0;
        int ZDZNum = 0;

        GUILayout.Label("0", GUILayout.Width(rect.width), GUILayout.Height(15));

        for (float i = 0f; i < time; i += 0.1f)
        {
            ZDZ += 0.1f;
            ZDZNum++;

            if(ZDZNum >= 10)
            {
                Z++;
                ZDZ = 0f;
                ZDZNum = 0;

                GUILayout.Label(Z + "----", GUILayout.Width(rect.width), GUILayout.Height(15));
            }                                                                                                                                                      
            else
            {
                GUILayout.Label(string.Format("{0:N1}", ZDZ) + "-", GUILayout.Width(rect.width), GUILayout.Height(15));
            }
            //else
            //{
            //    //GUILayout.Label("0.01f", GUILayout.Width(rect.width), GUILayout.Height(15));
            //}
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
        GUILayout.EndVertical();
    }
}
