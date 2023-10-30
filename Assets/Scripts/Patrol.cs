using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    int cur = 0;
    public float speed = 0.001f;
    private SpriteRenderer enemySprite;
    private bool isFacingRight = false;

    void Start(){
        enemySprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate(){
        onPatrol();
        Vector2 dir = waypoints[cur].position - transform.position;

        if(dir.x <= -0.01f && isFacingRight ){
           FlipSpriteX();
        }
        if(dir.x >= 0.01f && !isFacingRight ){
            FlipSpriteX();
        }

    }
    
    void onPatrol(){
        if (transform.position != waypoints[cur].position) {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        } else {
            cur = (cur+1) % waypoints.Length;
        }
    
    }

    private void FlipSpriteX()
    {
        enemySprite.flipX = !enemySprite.flipX;
        isFacingRight = !isFacingRight;
    }
}
