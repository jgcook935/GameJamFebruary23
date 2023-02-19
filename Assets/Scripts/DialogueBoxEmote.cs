using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxEmote : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    private bool isEnemy = false;

    void Update()
    {
        if (animator.enabled && ClickManager.Instance.clicksEnabled && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F)))
        {
            GetComponentInParent<Sign>().Click(isEnemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.enabled = true;
            animator.Play("PopupEmote");
            isEnemy = other.gameObject.tag == "Enemy";
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
