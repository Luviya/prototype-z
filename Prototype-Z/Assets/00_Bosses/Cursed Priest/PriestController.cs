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
        bool shouldWalk = false; // Determine if the Priest should be walking
        bool performAttack1 = false; // Determine if the Priest should perform Attack 1
        bool performAttack2 = false; // Determine if the Priest should perform Attack 2

        // Set Animator parameters based on the conditions
        animator.SetBool("isWalking", shouldWalk);
        if (performAttack1)
        {
            animator.SetTrigger("attack1");
        }
        if (performAttack2)
        {
            animator.SetTrigger("attack2");
        }
        // Other conditions and Animator parameter settings would go here
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
        // Handle the Priest's death here, such as disabling movement and interaction
    }

    // Additional methods for initiating attacks or other behaviors can be added here
}
