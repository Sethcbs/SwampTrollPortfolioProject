using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockAttack : MonoBehaviour
{
    public Transform attackArea;
    public float range;
    public LayerMask enemiesLayer;

    void Update()
    {
        //list of objects on the enemies layer that pass through sphere collider.
            Collider[] hitEnemies = Physics.OverlapSphere(attackArea.position, range, enemiesLayer);

        //for each of those objects, access health system and deal 100 damage
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<PlantMonsterHealth>().DealDamage(100);
        }
        
    }

    //draw the sphere collider for editing purposes
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackArea.position, range);
    }
}
