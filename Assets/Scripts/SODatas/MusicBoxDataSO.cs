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
    public List<KeyCode> doEventBtnKey; // � Ű�� ������ ���� ������ �� ���ΰ�
    public string musicDataKey; // �ڵ忡�� �ڵ����� �����Ѵ�

    public string eventName;

    public double referenceTime; // �� ���� üũ�� ������ �ȴ�. min���� �� ������ - ������ �Ͽ� üũ�ϰ�, max���� �� ������ + ������ �Ͽ� üũ�Ѵ�.

    public DoEventTime badTime; // startDoEventTime�� ���� ����� Time�� badTime�� maxŸ���� ���� ���������� ���� �� �̱⿡, badTime�� maxTime�� ������ Miss������ �Ѵ�.
    // referenceTime���� badTime�� min���� ���� checkStartTime�� �ȴ�.
    public DoEventTime greatTime;
    public DoEventTime perfectTime;
}

[CreateAssetMenu(menuName = "Asset/ScriptableObject/MusicBoxSO", order = 0)]
public class MusicBoxDataSO : ScriptableObject
{
    public MusicBox musicBox;
    public List<MusicData> dataList;
}
