using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollAttack : MonoBehaviour
{
    public Animator animator;

    [SerializeField] Transform attackArea;
    [SerializeField] float range;
    [SerializeField] LayerMask enemiesLayer;

    [SerializeField] float cooldownTime = 2f;
    [SerializeField] float nextFireTime = 1f;

    void Update()
    {
        //cooldown timer for attacks to keep animation synced properly.
        if(Time.time > nextFireTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }
    void Attack()
    {
        //start attack animation
        animator.SetTrigger("Attack 1");
        //list of game objects on the enemies layer that the sphere collider passes through.
        Collider[] hitEnemies = Physics.OverlapSphere(attackArea.position, range, enemiesLayer);

        //for each of those game objects, deal 50 damage.
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<PlantMonsterHealth>().DealDamage(50);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackArea.position, range);
    }
}
        
    

