using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float fallTime = 2f;  // Time it takes for the block to fall and reset
    public float fallSpeed = 10f;  // Additional speed for falling (you can adjust this value)

    // Reference to the Rigidbody2D component of the block
    private Rigidbody2D rb2D;
    private Vector3 initialPosition;
    private float gravityScale;

    void Start()
    {
        // Get the Rigidbody2D component and initial position on start
        rb2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rb2D.gravityScale *= fallSpeed;
    }

    public void TriggerFall()
    {
        // Start the falling coroutine
        StartCoroutine(FallAndReset());
    }

    IEnumerator FallAndReset()
    {
        // Make the block fall
        rb2D.isKinematic = false;

        // Wait for the specified fallTime
        yield return new WaitForSeconds(fallTime);


        // Reset the block position and rotation
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;

        // Make the block kinematic again
        rb2D.isKinematic = true;
    }
}
