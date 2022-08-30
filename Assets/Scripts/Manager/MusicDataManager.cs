using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataManager : MonoSingleton<MusicDataManager>
{
    [SerializeField]
    private MusicBoxDataSO currentMusicBoxDataSO = null;

    public Dictionary<double, string> timeToEventIdDict = new Dictionary<double, string>();
    public Dictionary<string, MusicData> stringToMusicDataDict = new Dictionary<string, MusicData>();

    public List<MusicData> checkingDataList = new List<MusicData> ();

    private bool startMusicBoxDataCheck = false;

    public bool StartMusicBoxDataCheck
    {
        set
        {
            if(!startMusicBoxDataCheck)
            {
                if(value)
                {
                    // 다시 뮤직관련 체크를 시작하는 시점, 즉 게임이 시작하는 시점

                    checkingDataList.Clear();
                    checkTimer = 0f;
                }
            }

            startMusicBoxDataCheck = value;
        }
    }

    public float checkTimer = 0f;

    public void ChangeCurrentMusicBoxDataSO(MusicBoxDataSO musicBoxDataSO)
    {
        currentMusicBoxDataSO = musicBoxDataSO;

        timeToEventIdDict.Clear();
        stringToMusicDataDict.Clear();

        foreach (var item in musicBoxDataSO.dataList)
        {
            timeToEventIdDict.Add(item.referenceTime - item.badTime.minTime, item.musicDataKey);
            stringToMusicDataDict.Add(item.musicDataKey, item);
        }
    }
    private void Start()
    {
        ChangeCurrentMusicBoxDataSO(currentMusicBoxDataSO);

        StartCurMusic();
    }

    private void Update()
    {
        string curMusicDataKey;
        MusicData musicData;

        if (startMusicBoxDataCheck)
        {
            if(timeToEventIdDict.TryGetValue(checkTimer, out curMusicDataKey))
            {
                if(stringToMusicDataDict.TryGetValue(curMusicDataKey, out musicData))
                {
                    checkingDataList.Add(musicData);
                }
            }

            for(int i = 0; i < checkingDataList.Count; i++)
            {
                if(checkTimer > checkingDataList[i].badTime.maxTime)
                {
                    // Miss 판정

                    checkingDataList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < checkingDataList.Count; i++ )
            {
                MusicData musicDataForCheck = checkingDataList[i];

                if (musicDataForCheck.doEventBtnKey.Count < 1)
                {
                    Debug.LogWarning("The MusicData '" + musicDataForCheck.musicDataKey + "' has no doEventBtnKey");

                    return;
                }

                if (checkTimer < musicDataForCheck.referenceTime - musicDataForCheck.badTime.minTime)
                {
                    // Miss판정

                    return;
                }

                bool keyInput = true;

                for (int j = 0; j < musicDataForCheck.doEventBtnKey.Count; j++)
                {
                    if (!Input.GetKey(musicDataForCheck.doEventBtnKey[j]))
                    {
                        keyInput = false;
                    }
                }

                if (keyInput)
                {
                    double timeForCheck = checkTimer - (musicDataForCheck.referenceTime - musicDataForCheck.badTime.minTime);

                    if (timeForCheck >= musicDataForCheck.referenceTime - musicDataForCheck.perfectTime.minTime && timeForCheck <= musicDataForCheck.referenceTime + musicDataForCheck.perfectTime.maxTime)
                    {
                        // perfect 판정

                        Debug.Log("Perfect");
                    }
                    else if (timeForCheck >= musicDataForCheck.referenceTime - musicDataForCheck.greatTime.minTime && timeForCheck <= musicDataForCheck.referenceTime + musicDataForCheck.greatTime.maxTime)
                    {
                        // great 판정

                        Debug.Log("Great");
                    }
                    else if (timeForCheck >= musicDataForCheck.referenceTime - musicDataForCheck.badTime.minTime && timeForCheck <= musicDataForCheck.referenceTime + musicDataForCheck.badTime.maxTime)
                    {
                        // bad 판정

                        Debug.Log("Bad");
                    }

                    checkingDataList.RemoveAt(i);
                    i--;

                    //EventManager.TriggerEvent(musicDataForCheck.eventName);
                }
            }

            checkTimer += Time.deltaTime;

            checkTimer = float.Parse(string.Format("{0:N2}", checkTimer));
        }
    }

    public void StartCurMusic()
    {
        SoundManager.Instance.ChangeCurMusic(currentMusicBoxDataSO.musicBox.AudioSource);
        SoundManager.Instance.PlayCurMusic();

        StartMusicBoxDataCheck = true;
    }

    public void StopCurMusic()
    {
        SoundManager.Instance.StopCurMusic();

        StartMusicBoxDataCheck = false;
    }

}
