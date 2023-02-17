using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source => GetComponent<AudioSource>();
    [SerializeField] AudioClip overWorldTheme;
    [SerializeField] AudioClip arenaTheme;

    private float overworldThemePlaybackhead = 0f;

    static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<AudioManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        source.clip = overWorldTheme;
        source.volume = .6f;
        source.Play();
    }

    private void OnDestroy()
    {
        source.Stop();
    }

    public void TransitionToArena()
    {
        overworldThemePlaybackhead = source.time;
        source.Stop();
        source.clip = arenaTheme;
        source.volume = .25f;
        source.time = 0f;
        source.Play();
    }

    public void TransitionToOverworld()
    {
        source.Stop();
        source.clip = overWorldTheme;
        source.volume = .6f;
        source.time = overworldThemePlaybackhead;
        source.Play();
    }
}
