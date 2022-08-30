using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataHelper : EditorWindow
{
    private readonly string editorSpritesPath = "Sprites/Editor/";

    public MusicBoxDataSO musicBoxDataSO = null;
    private MusicBoxDataSO prevMusicBoxDataSO = null;

    private Texture playTexture = null;
    private Texture pauseTexture = null;
    private Texture resetTexture = null;

    private bool soundPlayed = false;
    private bool playSound = false;
    private bool playButtonDown = false;
    private bool resetButtonDown = false;

    private double startSoundTime = 0d;
    private double pauseSoundTime = 0d;
    private float prevPlayTimer = 0f;
    private double soundPlayTimer = 0d;
    private double pauseSoundTimer = 0d;
    private float soundLength = 0f;

    [MenuItem("Helper/MusicDataHelper")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MusicDataHelper));
    }
    private void OnEnable()
    {
        playTexture = Resources.Load(editorSpritesPath + "SYMB_PLAY") as Texture;
        pauseTexture = Resources.Load(editorSpritesPath + "SYMB_PAUSE") as Texture;
        resetTexture = Resources.Load(editorSpritesPath + "SYMB_REPLAY") as Texture;
    }

    private void OnGUI()
    {
        GUILayout.Label("MusicDataHelper", EditorStyles.boldLabel);

        musicBoxDataSO = EditorGUILayout.ObjectField("Current Texts Script", musicBoxDataSO, typeof(MusicBoxDataSO), true) as MusicBoxDataSO;

        if (prevMusicBoxDataSO != musicBoxDataSO)
        {
            EditorSFX.StopAllClips();

            playSound = false;
            playButtonDown = false;

            prevMusicBoxDataSO = musicBoxDataSO;
            soundLength = musicBoxDataSO.musicBox.audioSource.clip.length;
            soundPlayTimer = 0f;
        }

        ButtonDownCheck();
        ButtonDownWork();
    }

    private void ButtonDownCheck()
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

            CheckTimer();

            resetButtonDown = GUILayout.Button(resetTexture, GUILayout.Width(30), GUILayout.Height(30));

            EditorGUILayout.BeginVertical();
        }
    }

    private void CheckTimer()
    {
        if (playSound)
        {
            double upTimer = (EditorApplication.timeSinceStartup - startSoundTime) - soundPlayTimer;

            if (prevPlayTimer == (float)soundPlayTimer)
            {
                if (upTimer > 0d)
                {
                    soundPlayTimer += upTimer;
                }
            }
            else
            {
                EditorSFX.PlayClip(musicBoxDataSO.musicBox.audioSource.clip, 0, false, soundPlayTimer);

                startSoundTime += upTimer;
            }
        }
        else
        {
            if (soundPlayed)
            {
                pauseSoundTimer = EditorApplication.timeSinceStartup - pauseSoundTime;
            }
        }
    }

    private void ButtonDownWork()
    {
        if (playButtonDown)
        {
            if (playSound)
            {
                playSound = false;
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

                EditorSFX.PlayClip(musicBoxDataSO.musicBox.audioSource.clip, 0, false, soundPlayTimer);
            }
        }

        if (resetButtonDown)
        {
            playSound = true;
            soundPlayed = true;
            soundPlayTimer = 0d;
            startSoundTime = EditorApplication.timeSinceStartup;

            EditorSFX.PlayClip(musicBoxDataSO.musicBox.audioSource.clip, 0, false, 0d);
        }
    }

    private void OnDestroy()
    {
        playSound = false;

        EditorSFX.StopAllClips();
    }
}
