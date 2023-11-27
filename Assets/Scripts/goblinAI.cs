using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    public GameObject player;
    public float wanderSpeed;
    public float speed;
    public float distanceBetweenPlayer;
    public LayerMask groundLayer;
    public float delayBeforeChase = 2.0f; // Adjust the delay time as needed
    public float delayBeforeChangeDirection = 1.0f; // Adjust the direction change delay time as needed
    public float minX;
    public float maxX;
    public float xShift = 3.0f;
    private Rigidbody2D rb;
    private float delayTimer;
    private float directionChangeTimer;
    private float distance;
    private float initialPositionX;
    public bool isAttacking = false;
    private Animator animator;
    private GoblinAttack goblinAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPositionX = transform.position.x;
        delayTimer = delayBeforeChase;
        directionChangeTimer = 0.0f;
        goblinAttack = GetComponent<GoblinAttack>();
    }

    void FixedUpdate()
    {
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
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, 0);
        rb.velocity = direction.normalized * speed;

        if (distance < distanceBetweenPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // Set a delay before changing direction again
            directionChangeTimer = delayBeforeChangeDirection;
        }
    }

    void Wander()
    {
        float currentPositionX = transform.position.x;
        if (currentPositionX >= initialPositionX + xShift || currentPositionX <= initialPositionX - xShift)
        {
            wanderSpeed = -wanderSpeed;
        }
        rb.velocity = new Vector2(wanderSpeed, 0);
    }

    bool IsPlayerOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    private void HaltMovement()
    {
        rb.velocity = new Vector2(0, 0);
    }
    
}
