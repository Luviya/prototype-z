using UnityEngine;

public class PriestController : MonoBehaviour
{
    private Animator animator;
    private bool isAlive = true;
    private float attackTimer = 0f;
    private float attackCooldown = 2f; // 2 seconds for attack cooldown

    public Transform player; // Assign this to your player in the Inspector

    public float health = 100f;
    // You can add other properties here, like a reference to the player character if needed

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Placeholder for behavior logic to determine which animation to play
        // You might want to replace this with actual game logic

        if (!isAlive) return;

        // Turn towards the player continuously
        TurnTowardsPlayer();

        // Handle attack timing
        if (attackTimer <= 0f)
        {
            PerformAttack();
            attackTimer = attackCooldown; // Reset attack timer
        }
        else
        {
            attackTimer -= Time.deltaTime; // Countdown to next attack
        }

        // Example conditions for different actions
        // bool shouldWalk = false;
        // bool performAttack1 = false;
        // bool performAttack2 = false;

        // // Set Animator parameters based on the conditions
        // animator.SetBool("isWalking", shouldWalk);
        // if (performAttack1)
        // {
        //     animator.SetTrigger("attack1");
        //     // TODO: Add code to deal damage to the player.
        // }
        // if (performAttack2)
        // {
        //     animator.SetTrigger("attack2");
        //     // TODO: Add code to deal damage to the player.
        // }
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;

        health -= damage;
        animator.SetTrigger("getHit");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("die");
        isAlive = false;
        // Destroy the object after 2 seconds
        Destroy(gameObject, 2f);
        // TODO: Add code to drop loot, play sound effects, etc.
    }

    private void TurnTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void PerformAttack()
    {
        // Decide between attack1 and attack2 here, for now we'll just trigger attack1
        animator.SetTrigger("attack1");
        // TODO: Add code to deal damage to the player.
    }
}
