using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int initialHealth = 100;
    public int currentHealth;

    public int initialMana = 50;
    public int currentMana;

    public HealthBar healthBar;

    //public HealthBar manaBar;

    public Animator animator;

    //public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;
        currentMana = initialMana;
        healthBar.SetMaxHealth(initialHealth);
        //manaBar.SetMaxHealth(initialMana);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
            StartCoroutine(ReloadSceneAfterAnimation(animator));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator ReloadSceneAfterAnimation(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LifeFlame"))
        {
            Destroy(other.gameObject);
            currentHealth += 20;
            if (currentHealth >= initialHealth)
            {
                currentHealth = initialHealth;
            }
            Debug.Log("this is the current health: " + currentHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("FlyingEnemy"))
        {
            TakeDamage(20);

        }
    }

}
