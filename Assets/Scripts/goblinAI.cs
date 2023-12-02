using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float wanderSpeed;
    [SerializeField]
    private float chaseSpeed;
    [SerializeField]
    private float retreatSpeed;
    [SerializeField]
    private float sightDistance;
    [SerializeField]
    private float wanderDistance;
    private float directionSign = 1.0f;
    public GoblinState goblinAIState;
    private Rigidbody2D rb;
    private Vector3 initialPosition;    
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float attackRange = 1.5f;
    [SerializeField]
    private int attackDamage = 10;
    [SerializeField]
    private LayerMask playerLayer;
    private Animator animator;
    private bool isFacingRight;

    // public float distanceBetweenPlayer;
    // public float delayBeforeChase = 2.0f; // Adjust the delay time as needed
    // public float delayBeforeChangeDirection = 1.0f; // Adjust the direction change delay time as needed
    // public float minX;
    // public float maxX;
    // public float xShift = 3.0f;
    // private float delayTimer;
    // private float directionChangeTimer;
    // private float distance;
    // public bool isAttacking = false;
    // private Animator animator;
    // private GoblinAttack goblinAttack;

    public enum GoblinState{
        Wander,Chase,Attack,Retreat
    }
    void Start()
    {
        player = FindObjectOfType<TarodevController.PlayerController>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = this.transform.position;
        animator = this.GetComponent<Animator>();
        isFacingRight = false;
        // delayTimer = delayBeforeChase;
        // directionChangeTimer = 0.0f;
        // goblinAttack = GetComponent<GoblinAttack>();
        // goblinAIState = GoblinState.Wander;
    }

    void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position+this.transform.right*attackRange, Color.red);
        switch(goblinAIState)
        {
            case GoblinState.Wander:
                Wander();
                break;
            case GoblinState.Chase:
                ChasePlayer();
                break;
            case GoblinState.Retreat:
                Retreat();
                break;
        }
        float currentSpeed = rb.velocity.x;
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));
        if (currentSpeed > 0.1f && isFacingRight)
        {
            Flip();
        }
        else if (currentSpeed < -0.1f && !isFacingRight)
        {
            Flip();
        }
        /*
        isAttacking = goblinAttack.getIsAttacking();
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        else
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (IsPlayerOnGround())
            {
                if (directionChangeTimer > 0)
                {
                    directionChangeTimer -= Time.deltaTime;
                }
                else
                {
                    
                    if (player.transform.position.x > minX && player.transform.position.x < maxX)
                    {
                        if(isAttacking){
                            HaltMovement();
                            Debug.Log("Halt Movement");
                        }
                        else{ 
                            ChasePlayer();
                        }
                    }
                    else
                    {
                        
                        Wander();
                    }
                }
            }
        }*/
    }

    private void Retreat()
    {
        float moveDirection = this.transform.position.x - this.initialPosition.x > 0 ? -1 : 1;
        rb.velocity = new Vector2(retreatSpeed*moveDirection, 0);
        if(Vector2.Distance(this.transform.position, initialPosition) < 0.1f)
        {
            goblinAIState = GoblinState.Wander;
        }
    }

    private void attack()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("doAttack");
        goblinAIState = GoblinState.Attack;
    }
    public void castAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, playerLayer);
        if(hit.collider != null && hit.collider.CompareTag("Player"))
        {
            PlayerStatus playerHealth = hit.collider.GetComponent<PlayerStatus>();

        // Check if playerHealth is not null before calling TakeDamage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player health: " + playerHealth.currentHealth);
            }
        }

    }
    public void endAttack()
    {
        goblinAIState = GoblinState.Retreat;
    }
    void ChasePlayer()
    {
        float distanceFromPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if(distanceFromPlayer<attackRange && IsOnGround())
        {
            print("Enter Attack State");
            attack();
        }
        else if (distanceFromPlayer < sightDistance && IsOnGround())
        {
            Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, 0);
            rb.velocity = direction.normalized * chaseSpeed;
        }
        else
        {
            rb.velocity = new Vector2(-rb.velocity.x, 0);
            goblinAIState = GoblinState.Retreat;
        }
    }

    void Wander()
    {
        if((this.transform.position - this.initialPosition).magnitude >= wanderDistance || !IsOnGround())
        {
            directionSign*=-1;
        }
        rb.velocity = new Vector2(wanderSpeed*directionSign,0);
        if(Vector2.Distance(this.transform.position, player.transform.position) < sightDistance)
        {
            goblinAIState = GoblinState.Chase;
        }
    }

    bool IsOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 2.0f, groundLayer);
        return hit.collider != null;
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
}
