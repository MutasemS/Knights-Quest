using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int initialHealth = 100;
    public int currentHealth;

    public int initialMana = 50;
    public int currentMana;

    public HealthBar healthBar; // Reference to the HealthBar script component
    public HealthBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;
        currentMana = initialMana;
        healthBar.SetMaxHealth(initialHealth);
        manaBar.SetMaxHealth(initialMana); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            useMana(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth); // Access the HealthBar script component directly
    }
    
    void useMana(int mana){
        currentMana -= mana;
        manaBar.SetHealth(currentMana);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("LifeFlame")){
            Destroy(other.gameObject);
            currentHealth += 20;
            if(currentHealth >= initialHealth){
                currentHealth = initialHealth;
            }
            Debug.Log("this is the current health: "+ currentHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("FlyingEnemy")){
            TakeDamage(20);
        }
    }
}
