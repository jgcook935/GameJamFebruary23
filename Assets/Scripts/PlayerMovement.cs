using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        set
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Speed", 0f);
            _enabled = value;
        }
    }

    public Rigidbody2D rb;
    public Animator animator;
    public float moveSpeed = 10f;
    private PlayerInput playerInput;
    private PlayerInputs playerInputActions;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputs();
        playerInputActions.Player.Enable();
    }

    void FixedUpdate()
    {
        if (!Enabled) return;
        var moveDirection = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void Update()
    {
        if (!Enabled) return;
        var moveDirection = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
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
