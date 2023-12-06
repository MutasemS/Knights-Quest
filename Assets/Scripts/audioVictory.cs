using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioVictory : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip victorySound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.tag == "Player")
         {
              audioSource.PlayOneShot(victorySound);
              Invoke("Destroy", 1);
         }
    }   
}
