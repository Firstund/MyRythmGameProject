using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DoEventTime
{
    public float minTime;
    public float maxTime;
}

[Serializable]
public class MusicData
{
    public List<KeyCode> doEventBtnKey; // 어떤 키를 눌러야 누름 판정을 할 것인가
    public string musicDataKey; // 코드에서 자동으로 설정한다

    public string eventName;

    public float startDoEventTime; // 이 값이 n일 때 음악 플레이 타임이 n일 경우 밑의 bed great perfect판정 시작

    public DoEventTime badTime; // startDoEventTime에 대한 상대적 Time값 badTime의 max타임이 가장 마지막으로 지날 것 이기에, badTime의 maxTime이 지나면 Miss판정을 한다.
                                // badTime의 minTime은 0으로 고정된다.
    public DoEventTime greatTime;
    public DoEventTime perfectTime;
}

[CreateAssetMenu(menuName = "Asset/ScriptableObject/MusicBoxSO", order = 0)]
public class MusicBoxDataSO : ScriptableObject
{
    public MusicBox musicBox;
    public List<MusicData> dataList;
}
