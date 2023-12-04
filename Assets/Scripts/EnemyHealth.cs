using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int health = 500;

	public GameObject deathEffect;

	public bool isInvulnerable = false;
	public Animator animator;

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;



		if (health <= 0)
		{
			Die();
		}
	}

	protected void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
