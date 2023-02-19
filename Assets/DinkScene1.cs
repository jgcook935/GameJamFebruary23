using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DinkScene1 : MonoBehaviour, ISign
{
    [SerializeField] private Animator exclamationAnimator;
    [SerializeField] private SpriteRenderer exclamationSprite;
    [SerializeField] private BoolSO doneDinkScene;

    [SerializeField] Transform dinkDestination;
    [SerializeField] AIDestinationSetter destinationSetter1;
    [SerializeField] AIDestinationSetter destinationSetter2;

    public Action dialogCloseAction { get; set; }

    public List<string> text { get; set; } = new List<string>
    {
        "This picnic is so lovely",
        "If only we had a cat to share it with..."
    };

    private void SetDialogCloseAction()
    {
        dialogCloseAction = () =>
        {
            StartCoroutine(WalkAway());
        };

        dialogCloseAction += () => StartCoroutine(WalkAway());
    }

    void Start()
    {
        SetDialogCloseAction();
        if (doneDinkScene.Value)
        {
            Destroy(this.gameObject);
        }
    }

    public void TriggerMoment()
    {
        doneDinkScene.Value = true;
        CharacterManager.Instance.player.Enabled = false;
        StartCoroutine(Exclamation());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !doneDinkScene.Value)
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
        GetComponent<Sign>().Click(false);
    }

    IEnumerator WalkAway()
    {
        CharacterManager.Instance.player.Enabled = false;
        yield return new WaitForSeconds(1);
        CharacterManager.Instance.player.Enabled = false;
        // set destination
        destinationSetter1.enabled = true;
        destinationSetter2.enabled = true;
        destinationSetter1.target = dinkDestination;
        destinationSetter2.target = dinkDestination;

        yield return new WaitForSeconds(3);
        CharacterManager.Instance.player.Enabled = true;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
