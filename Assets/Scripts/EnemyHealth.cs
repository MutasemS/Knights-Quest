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
		animator.SetTrigger("takeHit");
		health -= damage;



		if (health <= 0)
		{
			Die();
		}
	}

	protected void Die()
	{
		animator.SetTrigger("Death");
		float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(2).length;
		Destroy(gameObject, 3);
	}

}
