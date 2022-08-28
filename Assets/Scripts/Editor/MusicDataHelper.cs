using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataHelper : EditorWindow
{
    public MusicBoxDataSO musicBoxDataSO = null;
    private MusicBoxDataSO prevMusicBoxDataSO = null;

    private Texture playTexture = null;
    private Texture pauseTexture = null;

    private bool playSound = false;
    private bool playButtonDown = false;

    private double startSoundTime = 0d;
    private float prevPlayTimer = 0f;
    private double soundPlayTimer = 0d;
    private float soundLength = 0f;

    [MenuItem("Helper/MusicDataHelper")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MusicDataHelper));
    }
    private void OnEnable()
    {
        playTexture = Resources.Load("Sprites/Editor/SYMB_PLAY") as Texture;
        pauseTexture = Resources.Load("Sprites/Editor/SYMB_PAUSE") as Texture;
    }

    private void OnGUI()
    {
        GUILayout.Label("MusicDataHelper", EditorStyles.boldLabel);

        musicBoxDataSO = EditorGUILayout.ObjectField("Current Texts Script", musicBoxDataSO, typeof(MusicBoxDataSO), true) as MusicBoxDataSO;

        if(prevMusicBoxDataSO != musicBoxDataSO)
        {
            EditorSFX.StopAllClips();

            playSound = false;
            playButtonDown = false;

            prevMusicBoxDataSO = musicBoxDataSO;
            soundLength = musicBoxDataSO.musicBox.audioSource.clip.length;
            soundPlayTimer = 0f;
        }

        if(musicBoxDataSO != null)
        {
            prevPlayTimer = (float)soundPlayTimer;
            soundPlayTimer = EditorGUILayout.Slider(prevPlayTimer, 0f, soundLength);

            if (playSound)
            {
                Repaint();

                playButtonDown = GUILayout.Button(pauseTexture, GUILayout.Width(30), GUILayout.Height(30));

                if (prevPlayTimer == (float)soundPlayTimer)
                {
                    soundPlayTimer = (EditorApplication.timeSinceStartup - startSoundTime);
                }
            }
            else
            {
                playButtonDown = GUILayout.Button(playTexture, GUILayout.Width(30), GUILayout.Height(30));
            }
        }

        if (playButtonDown)
        {
            if(playSound)
            {
                playSound = false;

                EditorSFX.StopAllClips();
            }
            else if (musicBoxDataSO != null)
            {
                playSound = true;
                startSoundTime = EditorApplication.timeSinceStartup;
                soundPlayTimer = 0f;

                EditorSFX.PlayClip(musicBoxDataSO.musicBox.audioSource.clip);
            }
        }
    }

    private void OnDestroy()
    {
        EditorSFX.StopAllClips();
    }
}
