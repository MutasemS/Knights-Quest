using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : EnemyHealth
{

	[SerializeField]
	GameObject dropableItem;
<<<<<<< HEAD
	
=======
	[SerializeField] GameObject Youwin;
>>>>>>> refs/remotes/origin/main
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
			Youwin.SetActive(true);
			Time.timeScale = 0;
		}
	}
	protected override void Die()
	{
		
		Instantiate(dropableItem, this.transform.position + Vector3.up, this.transform.rotation);
		Destroy(this.gameObject);

	}

}
