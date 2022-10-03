using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioSource AudioSource
    {
        get 
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            return audioSource; 
        }
    }
    public float clipLength
    {
        get { return audioSource.clip.length; }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
}
