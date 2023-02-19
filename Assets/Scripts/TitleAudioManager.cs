using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioManager : MonoBehaviour
{
    public AudioSource audioSource => GetComponent<AudioSource>();

    static TitleAudioManager _instance;
    public static TitleAudioManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<TitleAudioManager>();
            return _instance;
        }
    }
}
