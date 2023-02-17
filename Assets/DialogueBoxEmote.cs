using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxEmote : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.enabled = true;
            animator.Play("PopupEmote");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.Play("CloseEmote");
        }
    }
}
