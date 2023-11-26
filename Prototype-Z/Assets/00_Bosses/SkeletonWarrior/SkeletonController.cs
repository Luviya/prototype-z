using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    // Reference to the player to calculate the distance and interaction.
    public Transform player;
    public float attackRange = 2.0f; // The distance within which the skeleton will attack.
    public float walkSpeed = 1.5f; // The walking speed of the skeleton.
    public float health = 100.0f; // Health of the skeleton.

    void Start()
    {
        animator = GetComponent<Animator>();
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
        // You would put your attack logic here (e.g., reduce player health).
    }

    // This method would be called when the skeleton is supposed to take damage.
    public void TakeDamage(float damage)
    {
        if(isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // Trigger the get hit animation.
            animator.SetTrigger("getHit");
        }
    }

    void Die()
    {
        // Trigger the death animation.
        animator.SetBool("isDead", true);
        isDead = true;
        // Disable the skeleton's ability to attack or move.
        // You can also add a delay here to destroy the object after the death animation plays.
    }

    // If you want to implement reaction when hit but not dead.
    void GetHit()
    {
        // Trigger the get hit animation.
        animator.SetTrigger("getHit");
    }
}

