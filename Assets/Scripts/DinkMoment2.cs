using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class DinkMoment2 : MonoBehaviour, ISign
{
    [SerializeField] private Animator exclamationAnimator;
    [SerializeField] private SpriteRenderer exclamationSprite;
    [SerializeField] private AIPath aipath;
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] AIDestinationSetter destinationSetter1;

    private bool startedDialogue = false;

    public Action dialogCloseAction { get; set; }

    public List<string> text { get; set; } = new List<string>
    {
        "OMGsh a beautiful kitty!",
        "We will love you forever"
    };

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {
            //
        };

        dialogCloseAction += () => StartCoroutine(PlayEnd());
    }

    void Awake()
    {
        SetDialogCloseAction();
    }

    void Update()
    {
        if (destinationSetter1.target == null) destinationSetter1.target = CharacterManager.Instance.player.transform;
        if (aipath.reachedEndOfPath && !startedDialogue)
        {
            startedDialogue = true;
            aipath.enabled = false;
            GetComponentInParent<Sign>().Click(true);
            dialogueBox.SetActive(true);
        }
    }

    public void TriggerMoment()
    {
        CharacterManager.Instance.player.Enabled = false;
        StartCoroutine(Exclamation());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TriggerMoment();
        }
    }

    IEnumerator Exclamation()
    {
        AudioManager.Instance.PlayNotificationSound();
        exclamationSprite.enabled = true;
        exclamationAnimator.enabled = true;
        exclamationAnimator.Play("PopupEmote");
        yield return new WaitForSeconds(1);
        exclamationAnimator.Play("CloseEmote");
        yield return new WaitForSeconds(1);
        destinationSetter1.enabled = true;
    }

    IEnumerator PlayEnd()
    {
        CharacterManager.Instance.player.Enabled = false;
        yield return new WaitForSeconds(1);
        CharacterManager.Instance.player.Enabled = false;
        // fade to black
        yield return new WaitForSeconds(3);
        // show another text about the end is a new beginning
        // change scene to main menu
        SceneManager.LoadScene(0);
    }
}
