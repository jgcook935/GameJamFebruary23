using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AIPath aipath;
    [SerializeField] AudioClip[] footsteps;
    private AudioSource source => GetComponent<AudioSource>();

    void Update()
    {
        var moveDirection = aipath.velocity;
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    public void PlayFootStep()
    {
        var random = Random.Range(0, 8);
        source.PlayOneShot(footsteps[random]);
    }
}
