using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : EnemyHealth
{

	public GameObject dropableItem;
	public new void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;

		if (health <= 60)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			this.Die();
			newDie();
		}
	}

	private void newDie(){
		Instantiate(dropableItem, transform.position, Quaternion.identity);
	}

}
