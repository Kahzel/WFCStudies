using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public float GroundDistance = .05f;
    public float CeilingDistance = .05f;
    public float WallDistance = .05f;
    
    public ContactFilter2D CastFilter;
    private CapsuleCollider2D TouchingCol;
    private Animator animator;

    private RaycastHit2D[] GroundHits = new RaycastHit2D[5];
    private RaycastHit2D[] CeilingHits = new RaycastHit2D[5];
    private RaycastHit2D[] WallHits = new RaycastHit2D[5];

    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private bool _isOnWall = false;
    [SerializeField] private bool _isOnCeiling = false;

    private Vector2 WallCheck => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            _isGrounded = value;
            animator.SetBool("IsGrounded", value);
        }
    }
    
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        set
        {
            _isOnWall = value;
            animator.SetBool("IsOnWall", value);
        }
    }
    
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        set
        {
            _isOnCeiling = value;
            animator.SetBool("IsOnCeiling", value);
        }
    }

    private void Awake()
    {
        TouchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        IsGrounded = TouchingCol.Cast(Vector2.down, CastFilter, GroundHits, GroundDistance) > 0;
        IsOnCeiling = TouchingCol.Cast(Vector2.up, CastFilter, CeilingHits, CeilingDistance) > 0;
        IsOnWall = false;
    }

    
}
