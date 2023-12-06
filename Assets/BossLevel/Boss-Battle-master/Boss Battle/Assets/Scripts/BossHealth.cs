using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : EnemyHealth
{

	[SerializeField]
	GameObject dropableItem;


	public override void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;

		if (health <= 60)
		{
			this.animator.SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			this.Die();
		}
	}
	protected override void Die()
	{

		Instantiate(dropableItem, this.transform.position + Vector3.up, this.transform.rotation);
		Destroy(this.gameObject);

	}

}
