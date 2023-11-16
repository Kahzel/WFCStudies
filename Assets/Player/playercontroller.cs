using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class playercontroller : MonoBehaviour
{
    public Vector2 moveInput;

    [SerializeField]
    private bool _isMoving = false;

    [SerializeField]
    private bool _isFacingRight = true;

    public float jumpInitialSpeed = -4f;

    public float BaseMoveSpeed = 5f;

    private TouchingDirections _touchingDirections;
    
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public float moveSpeed
    {
        get
        {
            if (IsMoving && !_touchingDirections.IsOnWall)
            {
                return BaseMoveSpeed;
            }

            return 0f;
        }
    }

    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        ToggleFacingRight(moveInput);
    }


    private void ToggleFacingRight(Vector2 vector)
    {
        if (vector.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        } else if (vector.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _touchingDirections.IsGrounded)
        {
            animator.SetTrigger("jump_start");
            rb.velocity = new Vector2(rb.velocity.x, jumpInitialSpeed);
        }
    }
}
