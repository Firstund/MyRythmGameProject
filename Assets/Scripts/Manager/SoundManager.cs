using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private MusicBox curMusic = null;
    public MusicBox CurMusic
    {
        get 
        { 
            if(curMusic == null)
            {
                curMusic = FindObjectOfType<MusicBox>();

                if (curMusic == null)
                {
                    GameObject temp = new GameObject("CurMusicBox");
                    curMusic = temp.AddComponent<MusicBox>();
                }
            }

            return curMusic;
        }
    }

    private float curMusicLength = 0f;
    public float CurMusicLength
    {
        get { return curMusicLength; }
    }

    public void ChangeCurMusic(AudioSource newMusic)
    {
        if(newMusic.clip == null)
        {
            Debug.LogError("The AudioSource '"+ newMusic.name + "' has no clip!");

            return;
        }

        CurMusic.AudioSource.clip = newMusic.clip;
        curMusicLength = newMusic.clip.length;
    }
    public void ChangeCurMusic(AudioClip newMusic)
    {
        if(newMusic == null)
        {
            Debug.LogError("The AudioClip '" + newMusic.name + "' is NULL!");

            return;
        }

        CurMusic.AudioSource.clip = newMusic;
        curMusicLength = newMusic.length;
    }

    public void PlayCurMusic()
    {
        CurMusic.PlaySound();
    }
    public void StopCurMusic()
    {
        CurMusic.StopSound();
    }
}
