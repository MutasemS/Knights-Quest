using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendulum : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movementSpeed;
    public float leftAngle;
    public float rightAngle;

    bool movingClockWise;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        Move();
    }

    public void ChangeMoveDir(){
        if(transform.rotation.z > rightAngle){
            movingClockWise = false;
        }
        if(transform.rotation.z < leftAngle){
            movingClockWise = true;
        }
    }

    public void Move(){
        ChangeMoveDir();

        if (movingClockWise){
            rb.angularVelocity = movementSpeed;
        }
        if (!movingClockWise){
            rb.angularVelocity = movementSpeed * -1;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().TakeDamage(100);
        }
    }
}
