using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundPlayerEditor : EditorWindow
{
    protected readonly string editorSpritesPath = "Sprites/Editor/";

    public MusicBoxDataSO musicBoxDataSO = null;
    protected MusicBoxDataSO prevMusicBoxDataSO = null;

    private Texture playTexture = null;
    private Texture pauseTexture = null;
    private Texture resetTexture = null;

    protected bool soundPlayed = false;
    protected bool playSound = false;
    protected bool playButtonDown = false;
    protected bool resetButtonDown = false;
    protected bool checkPauseTimer = false;

    protected double startSoundTime = 0d;
    protected float prevPlayTimer = 0f;
    protected double pauseSoundTime = 0d;
    protected double soundPlayTimer = 0d;
    protected double pauseSoundTimer = 0d;
    protected float soundLength = 0f;

    protected void OnEnable()
    {
        playTexture = Resources.Load(editorSpritesPath + "SYMB_PLAY") as Texture;
        pauseTexture = Resources.Load(editorSpritesPath + "SYMB_PAUSE") as Texture;
        resetTexture = Resources.Load(editorSpritesPath + "SYMB_REPLAY") as Texture;
    }
    protected void OnLostFocus()
    {
        playSound = false;
        pauseSoundTime = EditorApplication.timeSinceStartup;

        EditorSFX.StopAllClips();
    }

    protected void ParentsOnGUI()
    {
        musicBoxDataSO = EditorGUILayout.ObjectField("Current MusicDataSO", musicBoxDataSO, typeof(MusicBoxDataSO), true) as MusicBoxDataSO;

        if (prevMusicBoxDataSO != musicBoxDataSO)
        {
            EditorSFX.StopAllClips();

            playSound = false;
            playButtonDown = false;

            prevMusicBoxDataSO = musicBoxDataSO;
            soundLength = musicBoxDataSO.musicBox.AudioSource.clip.length;
            soundPlayTimer = 0f;
        }

        PlayButtonCheck();
        PlayButtonDownWork();

        CheckTimer();
    }
    protected void PlayButtonCheck()
    {
        if (musicBoxDataSO != null)
        {
            prevPlayTimer = (float)soundPlayTimer;
            soundPlayTimer = EditorGUILayout.Slider(prevPlayTimer, 0f, soundLength);

            EditorGUILayout.BeginHorizontal();

            if (playSound)
            {
                Repaint();

                playButtonDown = GUILayout.Button(pauseTexture, GUILayout.Width(30), GUILayout.Height(30));
            }
            else
            {
                playButtonDown = GUILayout.Button(playTexture, GUILayout.Width(30), GUILayout.Height(30));
            }

            resetButtonDown = GUILayout.Button(resetTexture, GUILayout.Width(30), GUILayout.Height(30));

            EditorGUILayout.EndHorizontal();
        }
    }
    protected void CheckTimer()
    {
        double upTimer = (EditorApplication.timeSinceStartup - startSoundTime) - soundPlayTimer;

        if (playSound)
        {
            if (prevPlayTimer == (float)soundPlayTimer)
            {
                if (upTimer > 0d)
                {
                    soundPlayTimer += upTimer;
                }
            }
            else
            {
                EditorSFX.PlayClip(musicBoxDataSO.musicBox.AudioSource.clip, 0, false, soundPlayTimer);

                startSoundTime += upTimer;
            }
        }
        else
        {
            if (soundPlayed && checkPauseTimer)
            {
                pauseSoundTimer = EditorApplication.timeSinceStartup - pauseSoundTime;
            }

            if (prevPlayTimer != (float)soundPlayTimer)
            {
                checkPauseTimer = false;
                startSoundTime = EditorApplication.timeSinceStartup - soundPlayTimer;
            }
        }
    }
    protected void PlayButtonDownWork()
    {
        if (playButtonDown)
        {
            if (playSound)
            {
                playSound = false;
                checkPauseTimer = true;
                pauseSoundTime = EditorApplication.timeSinceStartup;

                EditorSFX.StopAllClips();
            }
            else if (musicBoxDataSO != null)
            {
                playSound = true;
                soundPlayed = true;

                if (soundPlayTimer == 0d)
                {
                    startSoundTime = EditorApplication.timeSinceStartup;
                }
                else
                {
                    startSoundTime += pauseSoundTimer;
                }

                pauseSoundTimer = 0d;

                EditorSFX.PlayClip(musicBoxDataSO.musicBox.AudioSource.clip, 0, false, soundPlayTimer);
            }
        }

        if (resetButtonDown)
        {
            playSound = true;
            soundPlayed = true;
            soundPlayTimer = 0d;
            startSoundTime = EditorApplication.timeSinceStartup;

            EditorSFX.PlayClip(musicBoxDataSO.musicBox.AudioSource.clip, 0, false, 0d);
        }
    }
}
