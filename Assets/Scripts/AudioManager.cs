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
        source.Play();
    }

    private void OnDestroy()
    {
        source.Stop();
    }
}
