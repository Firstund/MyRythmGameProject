using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private MusicBox curMusic = null;
    public MusicBox CurMusic
    {
        get { return curMusic; }
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

        curMusic.audioSource.clip = newMusic.clip;
        curMusicLength = newMusic.clip.length;
    }
    public void ChangeCurMusic(AudioClip newMusic)
    {
        if(newMusic == null)
        {
            Debug.LogError("The AudioClip '" + newMusic.name + "' is NULL!");

            return;
        }

        curMusic.audioSource.clip = newMusic;
        curMusicLength = newMusic.length;
    }

    public void PlayCurMusic()
    {
        curMusic.PlaySound();
    }
    public void StopCurMusic()
    {
        curMusic.StopSound();
    }
}
