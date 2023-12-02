using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2 : MonoBehaviour
{
    public bool attacking = false;
    public bool attackDamageActive = false;
    Animator m_animator;
    [SerializeField]
    ParticleSystem attackParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            doAttack();
        }
        if(!attacking)
        {
            attackParticleSystem.Stop();
        }
    }
    private void FixedUpdate() {
        if(attackDamageActive)
        {
            whileAttackDamage();
        }
    }
    public void startAttackDamage()
        {
            attackDamageActive = true;
            attackParticleSystem.Play();

        }
        public void endAttackDamage()
        {
            attackDamageActive = false;
            attacking = false;
        }
        public void whileAttackDamage()
        {
            TarodevController.ScriptableStats _stats = this.GetComponent<TarodevController.PlayerController>()._stats;
            Debug.DrawRay(this.transform.position+0.8f*Vector3.up, new Vector3(-transform.localScale.x*_stats.attackRange,0,0), Color.green, 0.5f);
            RaycastHit2D hit = Physics2D.Raycast(transform.position+0.8f*Vector3.up, new Vector3(-transform.localScale.x,0,0), _stats.attackRange, LayerMask.GetMask("Enemy"));
            if(hit.collider!=null && hit.collider.tag == "Enemy")
            {
                Destroy(hit.collider.gameObject);
            }
        }
        private void doAttack()
        {
            if(!attacking)
            {
                attacking = true;
                m_animator.SetTrigger("Attack");
            }
        }

}
