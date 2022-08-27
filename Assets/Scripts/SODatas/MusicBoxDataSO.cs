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
    public List<KeyCode> doEventBtnKey; // � Ű�� ������ ���� ������ �� ���ΰ�
    public string musicDataKey; // �ڵ忡�� �ڵ����� �����Ѵ�

    public string eventName;

    public float startDoEventTime; // �� ���� n�� �� ���� �÷��� Ÿ���� n�� ��� ���� bed great perfect���� ����

    public DoEventTime badTime; // startDoEventTime�� ���� ����� Time�� badTime�� maxŸ���� ���� ���������� ���� �� �̱⿡, badTime�� maxTime�� ������ Miss������ �Ѵ�.
                                // badTime�� minTime�� 0���� �����ȴ�.
    public DoEventTime greatTime;
    public DoEventTime perfectTime;
}

[CreateAssetMenu(menuName = "Asset/ScriptableObject/MusicBoxSO", order = 0)]
public class MusicBoxDataSO : ScriptableObject
{
    public MusicBox musicBox;
    public List<MusicData> dataList;
}
