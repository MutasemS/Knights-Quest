using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private Rigidbody2D platform;
    public float moveSpeed = 2f;
    private float initialPositionY;
    public float yShift = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Rigidbody2D>();
        initialPositionY = platform.position.y;
    }

    // FixedUpdate is called at a fixed time step
    void FixedUpdate()
    {
        platformController();
    }

    private void platformController()
    {
        float currentPositionY = platform.position.y;
        if (currentPositionY >= initialPositionY + yShift || currentPositionY <= initialPositionY - yShift)
        {
            moveSpeed = -moveSpeed;
        }
        platform.velocity = new Vector2(0, moveSpeed);
    }
}
