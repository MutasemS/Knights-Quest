using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderPhysics : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector3 initialPosition;
    public GameObject player;
    private PlayerStatus playerStatus;

    private float initialVelocity;
    private float timeBetweenDamage = 1f;
    private float timeSinceDamage = 0f;
    private bool canDamage = true;
    private bool canPlaySound = true;
    private float timeBetweenSound = 1f;
    private float timeSinceSound = 0f;
    private AudioSource audioSource;
    public AudioClip boulderSound;
    // Screen shake variables
    public float screenShakeDuration = 0.1f;  // Adjust as needed
    public float screenShakeIntensity = 0.05f; // Adjust as needed


    void Start()
    {
        // Get the Rigidbody2D component and initial position on start
        rb2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;    
        playerStatus = player.GetComponent<PlayerStatus>();
        initialVelocity = rb2D.velocity.y;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceDamage += Time.deltaTime;
        if(timeSinceDamage >= timeBetweenDamage){
            canDamage = true;
        }
        timeSinceSound += Time.deltaTime;
        if(timeSinceSound >= timeBetweenSound){
            canPlaySound = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player (you can modify this based on your player setup)
        if (other.CompareTag("FinalPosition"))
        {
            Debug.Log("Boulder hit final position");
            // Trigger the block to fall after the specified delay
            Invoke("Reset", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
           //do damage to player
           if(canDamage){
               playerStatus.TakeDamage(50);
               canDamage = false;
               timeSinceDamage = 0f;
           }
            
        }
        if (other.gameObject.CompareTag("ground"))
        {
            StartCoroutine(ScreenShake());
            if(canPlaySound){
                audioSource.clip = boulderSound;
                audioSource.time = 2.5f;
                audioSource.Play(); 
                canPlaySound = false;
                timeSinceSound = 0f;
            }
            
        }
        
    }

    private void Reset(){
        rb2D.transform.position = initialPosition;
        rb2D.velocity = new Vector2(0, initialVelocity);
    }

    IEnumerator ScreenShake()
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float elapsed = 0f;

        while (elapsed < screenShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * screenShakeIntensity;
            float y = Random.Range(-1f, 1f) * screenShakeIntensity;

            Camera.main.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the camera position after the shake
        Camera.main.transform.position = originalPosition;
    }

}
