using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source => GetComponent<AudioSource>();
    [SerializeField] AudioClip overWorldTheme;
    [SerializeField] AudioClip arenaTheme;
    [SerializeField] AudioClip arenaThemeTransition;

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

    public void LeaveOverWorld()
    {
        overworldThemePlaybackhead = source.time;
        source.Stop();
        source.clip = arenaTheme;
    }

    public void TransitionToArena()
    {
        source.volume = .25f;
        source.time = 0f;
        source.Play();
    }

    public void LeaveArena()
    {
        source.Stop();
        source.clip = overWorldTheme;
    }

    public void TransitionToOverworld()
    {
        source.volume = .6f;
        source.time = overworldThemePlaybackhead;
        source.Play();
    }

    public void PlayTransitionTheme()
    {
        source.volume = .25f;
        source.PlayOneShot(arenaThemeTransition);
    }
}
