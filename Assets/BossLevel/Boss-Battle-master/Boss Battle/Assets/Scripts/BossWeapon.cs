using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	public int attackDamage = 20;
	public int enragedAttackDamage = 40;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
<<<<<<< HEAD
	private AudioSource audioSource;
	public AudioClip attackSound;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}
=======
	public AudioSource audioSource;
	public AudioClip attackSound;
>>>>>>> refs/remotes/origin/main

	public void Attack()
	{
		audioSource.PlayOneShot(attackSound);

		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerStatus>().TakeDamage(attackDamage);
		}

		audioSource.PlayOneShot(attackSound);

	}


	public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		audioSource.Play();
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerStatus>().TakeDamage(enragedAttackDamage);
		}
		audioSource.PlayOneShot(attackSound);
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;


		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
