using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public LayerMask playerLayer;
    public float attackCooldown = 1.0f; // Time between attacks

    private Animator animator;
    private float attackTimer; // Timer to track cooldown
    private RaycastHit2D hit;
    private bool isAttacking = false;   // Used to check if the goblin is attacking

    void Start()
    {
        animator = GetComponent<Animator>();
        attackTimer = 0.0f; // Initialize the attack timer
    }

    void FixedUpdate()
    {
        // Update the attack cooldown timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Check if the player is within attack range and the cooldown is complete
        if (attackTimer <= 0 && IsPlayerInRange())
        {
            // Perform the attack
            Attack();

            // Reset the attack cooldown timer
            attackTimer = attackCooldown;
        }
    
    }

    bool IsPlayerInRange()
    {
        // Cast a ray in the forward direction of the goblin
        hit = Physics2D.Raycast(transform.position, transform.right, attackRange, playerLayer);

        // Check if the ray hits the player
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void Attack()
    {
        Debug.Log("Attack");
        isAttacking = true; // Set isAttacking to true
        HaltMovement(); // Stop the goblin from moving
        // Set the attack trigger for the animation
        if (animator != null)
        {
            animator.SetTrigger("doAttack");
        }
    }

    // Called from the animation event to deal damage
    public void DealDamage()
    {
        PlayerStatus playerHealth = hit.collider.GetComponent<PlayerStatus>();

        // Check if playerHealth is not null before calling TakeDamage
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player health: " + playerHealth.currentHealth);
            isAttacking = false; // Set isAttacking to false
        }
    }

    private void HaltMovement(){
        // Set the velocity to 0
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;    
    }

    public bool getIsAttacking(){
        return isAttacking;
    }
}
