using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string speedParameterName = "Speed";
    [SerializeField] private string isDeadParameterName = "IsDead";
    [SerializeField] private string attackedParameterName = "Attacked";

    [Header ("Stat")]
    [SerializeField] private float health = 2f;
    [SerializeField] protected bool knockable = true;
    bool isDead = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool(isDeadParameterName, isDead);
    }
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

    void Die()
    {
       Destroy(gameObject);
    }
}
