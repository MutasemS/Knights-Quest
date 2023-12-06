using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int health = 500;

	//public GameObject deathEffect;

	public bool isInvulnerable = false;
	public Animator animator;
	private AudioSource audioSource;
	public AudioClip dieSound;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		animator = this.gameObject.GetComponent<Animator>();
	}
	public virtual void TakeDamage(int damage)
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

	protected virtual void Die()
	{
		animator.SetTrigger("Death");
		audioSource.PlayOneShot(dieSound);
//		float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(2).length;
		Destroy(gameObject, 3);
	}

}
