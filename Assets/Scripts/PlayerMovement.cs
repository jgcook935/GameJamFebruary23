using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static PlayerMovement _instance;
    public static PlayerMovement Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PlayerMovement>();
            return _instance;
        }
    }

    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }

    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;
    public float moveSpeed = 10f;

    float tileScale = 1; //0.15f;

    void FixedUpdate()
    {
        if (!Enabled) return;

        // handle movement
        var totalMoveSpeed = moveSpeed;
        var moveDirection = movement.normalized;
        rb.velocity = new Vector2(moveDirection.x * totalMoveSpeed * tileScale, moveDirection.y * totalMoveSpeed * tileScale);
    }

    void Update()
    {
        if (!Enabled) return;

        // handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public void SetMovementEnabled(bool enabled)
    {
        Enabled = enabled;
        if (!enabled)
        {
            rb.velocity = Vector2.zero;
            animator.StopPlayback();
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
    }
}
