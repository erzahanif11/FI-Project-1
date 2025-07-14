using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private string speedParameterName = "Speed";
    [SerializeField] private string verticalVelocityParameterName = "VerticalVelocity";
    [SerializeField] private string isGroundedParameterName = "isGrounded";

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float JumpMultiplier = 2f;

    [Header("Ground check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [Header("Dont edit this")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveDirection = 0f;
    [SerializeField] private bool jump = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] public float verticalVelocity = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        SetAnimationParameters();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void SetAnimationParameters()
    {
        if (animator == null) return;
        animator.SetFloat(speedParameterName, Mathf.Abs(moveDirection));
        animator.SetFloat(verticalVelocityParameterName, verticalVelocity);
        animator.SetBool(isGroundedParameterName, isGrounded);
    }
    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        Flip();
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }
    void Jump()
    {
        if (jump & isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jump = false;
        }
        
        if (rb.linearVelocity.y > 0 && !jump)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (JumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        verticalVelocity = rb.linearVelocity.y;
    }

    void Flip()
    {
        float scaleX = Mathf.Abs(transform.localScale.x);
        if (moveDirection > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

 #region Input System
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDirection = input.x;
        Debug.Log(moveDirection);
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed & isGrounded)
        {
            jump = true;
        }
    }
 #endregion
}
