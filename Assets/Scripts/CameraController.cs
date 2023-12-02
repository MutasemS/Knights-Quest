using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPlayer : MonoBehaviour
{
    public Transform target;
    public float minX, maxX, minY, maxY;
    public float zoomLevel = 5f; // Default zoom level, adjust as needed

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);

            // Adjust camera zoom
            cam.orthographicSize = zoomLevel;
        }
    }
}
