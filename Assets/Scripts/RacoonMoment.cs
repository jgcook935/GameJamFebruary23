using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RacoonMoment : MonoBehaviour
{
    [SerializeField] private Animator exclamationAnimator;
    [SerializeField] private SpriteRenderer exclamationSprite;
    [SerializeField] private AIPath aipath;
    [SerializeField] private BoolSO racoonTalked;
    [SerializeField] private GameObject dialogueBox;

    private bool startedDialogue = false;

    private AIDestinationSetter destinationSetter;

    void Start()
    {
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
        if (racoonTalked.Value)
        {
            dialogueBox.SetActive(true);
            GetComponentInParent<CircleCollider2D>().isTrigger = false;
            GetComponentInParent<CircleCollider2D>().isTrigger = true;
        }
    }

    void Update()
    {
        if (destinationSetter.target == null) destinationSetter.target = CharacterManager.Instance.player.transform;
        if (aipath.reachedEndOfPath && !startedDialogue)
        {
            startedDialogue = true;
            GetComponentInParent<Sign>().Click();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !racoonTalked.Value)
        {
            racoonTalked.Value = true;
            CharacterManager.Instance.player.Enabled = false;
            StartCoroutine(Exclamation());
        }
    }

    IEnumerator Exclamation()
    {
        exclamationSprite.enabled = true;
        exclamationAnimator.enabled = true;
        exclamationAnimator.Play("PopupEmote");
        yield return new WaitForSeconds(1);
        exclamationAnimator.Play("CloseEmote");
        yield return new WaitForSeconds(1);
        destinationSetter.enabled = true;
    }
}
