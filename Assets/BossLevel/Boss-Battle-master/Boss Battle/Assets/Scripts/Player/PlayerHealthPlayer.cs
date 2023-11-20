using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthPlayer : MonoBehaviour
{

	public int health = 100;

	public Animator animator;

	//public GameObject deathEffect;



	public void TakeDamage(int damage)
	{
		health -= damage;

		animator.SetTrigger("Hurt");

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Animator animator = GetComponent<Animator>();
		if (animator != null)
		{
			animator.SetTrigger("Dead");
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

}


