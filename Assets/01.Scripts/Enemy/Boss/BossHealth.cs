using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamage
{
    [SerializeField] private float currentHealth = 0f;

    private void Start()
    {
        currentHealth = GetComponent<AIBrain>().status.maxHealth;
    }

    public void Damaged(float damage, Vector3 direction)
    {
        Debug.Log("boss damaged");

        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            Die();
        }
        
    }

    private void Die()
    {
        Debug.Log("die");
    }
}
