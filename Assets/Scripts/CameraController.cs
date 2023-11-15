using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the player or object the camera is following
    public float minX, maxX, minY, maxY;


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Get the target's position
            Vector3 targetPosition = target.position;

            // Clamp the camera's x and y position within the specified boundaries
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

            // Set the new position for the camera
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
        
    }
}
