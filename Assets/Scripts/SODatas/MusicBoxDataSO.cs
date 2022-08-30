using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DoEventTime
{
    public double minTime;
    public double maxTime;
}

[Serializable]
public class MusicData
{
    public List<KeyCode> doEventBtnKey; // 어떤 키를 눌러야 누름 판정을 할 것인가
    public string musicDataKey; // 코드에서 자동으로 설정한다

    public string eventName;

    public double referenceTime; // 이 값은 체크의 기준이 된다. min값은 이 값에서 - 연산을 하여 체크하고, max값은 이 값에서 + 연산을 하여 체크한다.

    public DoEventTime badTime; // startDoEventTime에 대한 상대적 Time값 badTime의 max타임이 가장 마지막으로 지날 것 이기에, badTime의 maxTime이 지나면 Miss판정을 한다.
    // referenceTime에서 badTime의 min값을 빼면 checkStartTime이 된다.
    public DoEventTime greatTime;
    public DoEventTime perfectTime;
}

[CreateAssetMenu(menuName = "Asset/ScriptableObject/MusicBoxSO", order = 0)]
public class MusicBoxDataSO : ScriptableObject
{
    public MusicBox musicBox;
    public List<MusicData> dataList;
}
