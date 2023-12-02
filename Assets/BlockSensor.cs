using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    // Reference to the FallingBlock script attached to the block
    public FallingBlock fallingBlock;
    public float delayAfterTrigger = 1f;  // Delay after the player enters the trigger

    // OnTriggerEnter2D is called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player (you can modify this based on your player setup)
        if (other.CompareTag("Player"))
        {
            // Trigger the block to fall after the specified delay
            Invoke("TriggerBlockFall", delayAfterTrigger);
        }
    }

    void TriggerBlockFall()
    {
        fallingBlock.TriggerFall();
    }
}
