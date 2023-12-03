using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinController : MonoBehaviour
{
    private Rigidbody2D goblin; 
    private Animator animator;
    private float currentSpeed;
    private float currentHealth;
    private bool isFacingRight = true;
    private bool tookHit = false;

    // Start is called before the first frame update
    void Start()
    {
        goblin = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = 0.0f;
        currentHealth = 60.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentSpeed = goblin.velocity.x;
        animator.SetFloat("speed", Mathf.Abs(currentSpeed));
       
        if (currentSpeed > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (currentSpeed < 0 && isFacingRight)
        {
            Flip();
        }

        if (tookHit)
        {
            animator.SetBool("takeHit", false);
            tookHit = false;
        }   
            
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetBool("takeHit", true);
       
        tookHit = true;

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        Destroy(gameObject, 0.5f);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }


}
