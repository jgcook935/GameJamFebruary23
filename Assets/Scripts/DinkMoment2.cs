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
    [SerializeField] GameObject credits;

    [SerializeField] AIDestinationSetter destinationSetter1;

    private bool startedDialogue = false;

    public Action dialogCloseAction { get; set; }

    public List<string> text { get; set; } = new List<string>
    {
        "Aww you poor kitty! How'd you find your way in here?",
        "You look like you could use a bath and a good meal.",
        "We've always wanted a cat. If you'd join us, our family would finally be complete.",
        "Now you just need a name...",
        "Welcome home, Skully!"
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
        ClickManager.Instance.SetClicksEnabled(false);
        CharacterManager.Instance.player.Enabled = false;
        yield return new WaitForSeconds(1);
        CharacterManager.Instance.player.Enabled = false;
        AudioManager.Instance.LeaveOverWorld();
        UIManager.Instance.crossfadeAnimator.SetTrigger("Start");
        credits.SetActive(true);
        yield return new WaitForSeconds(5);
        // show another text about the end is a new beginning
        // change scene to main menu
        SceneManager.LoadScene(0);
    }
}
