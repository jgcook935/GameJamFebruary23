using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source => GetComponent<AudioSource>();
    [SerializeField] AudioClip overWorldTheme;

    private void Awake()
    {
        source.clip = overWorldTheme;
        // source.time
        // make sure to save the audio clip's time and be able to go back to where we were before
        source.Play();
    }

    private void OnDestroy()
    {
        source.Stop();
    }
}
