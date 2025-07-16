using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 2f;
    [SerializeField] protected bool knockable = true;
    bool isDead = false;
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
        Die();
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
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
