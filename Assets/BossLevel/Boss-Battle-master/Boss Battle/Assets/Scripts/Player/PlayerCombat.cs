using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator m_animator;
    public Transform attackPoint;
    private TarodevController.ScriptableStats _stats;
    private AudioSource audioSource;
    public AudioClip attackSound;

    public LayerMask enemyLayers;
    [SerializeField]
    ParticleSystem attackParticleSystem;



    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
            audioSource.PlayOneShot(attackSound);
        }

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        _stats = GetComponent<TarodevController.PlayerController>()._stats;
    }

    void Attack()
    {
        attackParticleSystem.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _stats.attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(_stats.attackDamage);
        }
    
    }

    /*void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, _stats.attackRange);

    }*/
}
