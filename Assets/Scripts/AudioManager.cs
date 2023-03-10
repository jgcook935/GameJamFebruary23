using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    const float DEFAULT_VOLUME = .65f;
    AudioSource source => GetComponent<AudioSource>();
    [SerializeField] AudioClip overWorldTheme;
    [SerializeField] AudioClip arenaTheme;
    [SerializeField] AudioClip arenaThemeTransition;
    [SerializeField] AudioClip notification;
    [SerializeField] AudioClip gate;

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
        source.volume = DEFAULT_VOLUME;
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
        source.volume = DEFAULT_VOLUME;
        source.time = overworldThemePlaybackhead;
        source.Play();
    }

    public void PlayTransitionTheme()
    {
        source.volume = .25f;
        source.PlayOneShot(arenaThemeTransition);
    }

    public void PlayNotificationSound()
    {
        source.PlayOneShot(notification);
    }

    public void PlayGateSound()
    {
        source.volume = .1f;
        source.PlayOneShot(gate);
        StartCoroutine(ReturnToDefaultVolume());
    }

    IEnumerator ReturnToDefaultVolume(float seconds = 3f)
    {
        yield return new WaitForSeconds(seconds);
        source.volume = DEFAULT_VOLUME;
    }
}
