using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    private Vector3 initialOffset;
    [SerializeField]
    float cameraFolllowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        initialOffset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position + initialOffset;
        
    }
}
