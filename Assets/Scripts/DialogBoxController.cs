using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxController : MonoBehaviour, IClickable
{
    [SerializeField] AudioClip[] clips; // in the future it could be cool to grab these from a folder or something
    AudioSource source => GetComponent<AudioSource>();
    public bool isOpen = false;
    int textIndex = 0;
    List<string> text = new List<string>();

    Animator animator => GetComponentInChildren<Animator>();

    Action DialogCloseAction { get; set; }

    static DialogBoxController _instance;
    public static DialogBoxController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<DialogBoxController>();
            return _instance;
        }
    }

    void Awake()
    {
        isOpen = true;
        CharacterManager.Instance.SetControlsEnabled(false);
    }

    // i tried having the paper sound play when the first box opens, but we tend to do other sounds when it opens so i think it's fine to just have page turning make a sound
    //void Start()
    //{
    //    PlayRandomPaperSound();
    //}

    void Update()
    {
        NextTextOrDestroy();
    }

    public void Click()
    {
        NextTextOrDestroy();
    }

    void NextTextOrDestroy()
    {
        if (text.Count == 0)
        {
            throw new System.Exception("You need to call SetText on the DialogBoxController ya knucklehead!");
        }

        // allowing escape to destroy anyway for testing
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            //PlayRandomPaperSound();
            //animator.SetTrigger("DialogueClosed");
            CharacterManager.Instance.SetControlsEnabled(true);
            GameObject.Destroy(this.gameObject);
        }
        else if (isOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && textIndex < text.Count)
        {
            //PlayRandomPaperSound();
            gameObject.GetComponentInChildren<Text>().text = text[textIndex];
            textIndex++;
            // play a continue sound
        }
        else if (isOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && textIndex == text.Count)
        {
            //PlayRandomPaperSound();
            //animator.SetTrigger("DialogueClosed");
            this.DialogCloseAction?.Invoke();
            CharacterManager.Instance.SetControlsEnabled(true);
            GameObject.Destroy(this.gameObject);
        }
    }

    void PlayRandomPaperSound() => source.PlayOneShot(clips[UnityEngine.Random.Range(0, 7)]);

    public void SetDialogCloseAction(Action action)
    {
        this.DialogCloseAction = action;
    }

    public void SetText(List<string> text)
    {
        this.text = text;
        gameObject.GetComponentInChildren<Text>().text = this.text[textIndex];
        textIndex++;
    }
}
