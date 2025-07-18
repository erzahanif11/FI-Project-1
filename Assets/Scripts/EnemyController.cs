using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string speedParameterName = "Speed";
    //[SerializeField] private string isGroundedParameterName = "IsGrounded";
    [SerializeField] private string isDeadParameterName = "IsDead";
    [SerializeField] private string attackedParameterName = "Attacked";

    //[Header("Ground Check")]
    //[SerializeField] private Transform groundCheckPoint;
    //[SerializeField] private float groundCheckRadius = 0.2f;
    //[SerializeField] private LayerMask groundLayer;

    [Header ("Stat")]
    [SerializeField] private float health = 2f;
    [SerializeField] private float chaseSpeed = 0f;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] protected bool knockable = true;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float moveDirection;
    //[SerializeField] private bool isGrounded;
    bool isDead = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj.transform;
            }
        }
    }
    void Update()
    {
        //GroundCheck();
        animator.SetFloat(speedParameterName, chaseSpeed);
        animator.SetBool(isDeadParameterName, isDead);
        //animator.SetBool(isGroundedParameterName, isGrounded);
        if (!isDead)
        {
            float distanceX = Mathf.Abs(targetPlayer.position.x - transform.position.x);
            moveDirection = targetPlayer.position.x - transform.position.x;
            Debug.Log(moveDirection);

            if (distanceX > 0.1f && chaseRange >= distanceX)
            {
                chaseSpeed = 1.5f;
                ChasePlayer();
            }else chaseSpeed = 0f;
        }
        Flip();
    }

    //void GroundCheck()
    //{
    //    isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, 0, groundLayer);
    //}
    public void TakeDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else if (health <= damage)
        {
            health = 0;
            isDead = true;
        }
        animator.SetTrigger(attackedParameterName);
    }

    public void Knockback(Vector2 force)
    {
        if (knockable)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        float scaleX = Mathf.Abs(transform.localScale.x);
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    void ChasePlayer()
    {
         float directionX = Mathf.Sign(targetPlayer.position.x - transform.position.x);
         Vector3 move = new Vector3(directionX * chaseSpeed * Time.deltaTime, 0, 0);
         transform.position += move;
    }

    void Die()
    {
       Destroy(gameObject);
    }
}
