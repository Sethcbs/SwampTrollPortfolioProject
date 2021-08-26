using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public Animator animator;
    public GameObject plant;
    public float deathDelayTime;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        //when called, subtract the damage inputted from the objects current health value.
        currentHealth -= damage;
        //trigger animation
        animator.SetTrigger("gotHit");

        //if the health value is less than or equal to zero, deactivate collider, start death animation, wait until animation is done, then deactivate the whole game object.
        if(currentHealth <= 0)
        {
            StartCoroutine(DeathDelay());
        }
        IEnumerator DeathDelay()
        {
            GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("isDead", true);
            yield return new WaitForSeconds(deathDelayTime);
            plant.SetActive(false);
        }
    }
}
