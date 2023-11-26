using UnityEngine;

public class PriestController : MonoBehaviour
{
    private Animator animator;
    private bool isAlive = true;

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

        // Example conditions for different actions
        bool shouldWalk = false;
        bool performAttack1 = false;
        bool performAttack2 = false;

        // Set Animator parameters based on the conditions
        animator.SetBool("isWalking", shouldWalk);
        if (performAttack1)
        {
            animator.SetTrigger("attack1");
            // TODO: Add code to deal damage to the player.
        }
        if (performAttack2)
        {
            animator.SetTrigger("attack2");
            // TODO: Add code to deal damage to the player.
        }
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
}
