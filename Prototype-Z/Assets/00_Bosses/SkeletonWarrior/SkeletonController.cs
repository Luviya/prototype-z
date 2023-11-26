using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    // Reference to the player to calculate the distance and interaction.
    public Transform player;
    public float attackRange = 2.0f; // The distance within which the skeleton will attack.
    public float walkSpeed = 1.5f; // The walking speed of the skeleton.

    public float maxHealth = 100f;
    private float currentHealth;
    public HealthBar healthBar; // Reference to the HealthBar script/component.

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(isDead) return; // Stop processing if skeleton is dead.

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        
        // If the skeleton is not within the attack range, walk towards the player.
        if (distanceToPlayer > attackRange)
        {
            WalkTowardsPlayer();
        }
        else
        {
            // Stop walking and attack if within range.
            animator.SetBool("isWalking", false);
            Attack();
        }
    }

    void WalkTowardsPlayer()
    {
        animator.SetBool("isWalking", true);
        // Rotate to face the player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, walkSpeed * Time.deltaTime);
    }

    void Attack()
    {
        // Trigger the attack animation.
        animator.SetTrigger("attack");
        // TODO: Add code to deal damage to the player.

    }

    // This method would be called when the skeleton is supposed to take damage.
    public void TakeDamage(float damage)
    {
        if(isDead) return;

        if(currentHealth <= 0) return; // Check to prevent negative health or repeated death.

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("getHit"); // Trigger the get hit animation.

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger the death animation.
        animator.SetBool("isDead", true);
        isDead = true;
        // Destroy the skeleton after a few seconds.
        Destroy(gameObject, 2f);
        // TODO: Add code to drop loot, play sound effects, etc.
    }

    // If you want to implement reaction when hit but not dead.
    void GetHit()
    {
        // Trigger the get hit animation.
        animator.SetTrigger("getHit");
    }
}

