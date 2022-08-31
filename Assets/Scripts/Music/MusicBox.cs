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
    public float clipLength = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        clipLength = audioSource.clip.length;
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
