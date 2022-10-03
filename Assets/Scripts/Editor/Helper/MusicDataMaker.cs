using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataMaker : SoundPlayerEditor
{
    private Texture dataDrawTexture = null;

    private bool dataDrawButtonDown = false;

    [MenuItem("Helper/MusicDataMaker")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MusicDataMaker));
    }

    protected override void OnGUI()
    {
        GUILayout.Label("MusicDataMaker", EditorStyles.boldLabel);

        base.OnGUI();

        ButtonDownCheck();
        ButtonDownWork();
    }
    private void ButtonDownCheck()
    {
        if (musicBoxDataSO != null)
        {
            dataDrawButtonDown = GUILayout.Button(dataDrawTexture, GUILayout.Width(300), GUILayout.Height(300));
        }
    }

    private void ButtonDownWork()
    {
        if (dataDrawButtonDown)
        {
            MusicData data = new MusicData();
            data.referenceTime = soundPlayTimer;

            bool found = false;

            for (int i = 0; i < musicBoxDataSO.dataList.Count; i++)
            {
                if (musicBoxDataSO.dataList[i].referenceTime == soundPlayTimer)
                {
                    found = true;

                    break;
                }
            }

            if (found)
            {
                Debug.Log(soundPlayTimer + "초엔 이미 MusicData가 존재합니다.");
            }
            else
            {
                musicBoxDataSO.dataList.Add(data);

                Debug.Log(soundPlayTimer + "초에 MusicData를 기록했습니다.");
            }
        }
    }
}
