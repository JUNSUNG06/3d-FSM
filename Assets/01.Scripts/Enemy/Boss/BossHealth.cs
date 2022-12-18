using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour, IDamage
{
    public float maxHealth = 0f;
    public float Health { get; set; }

    public Action damagedEvent;

    private void Start()
    {
        Health = maxHealth;
    }

    public void Damaged(float damage, Vector3 direction)
    {
        Debug.Log("boss damaged");

        Health -= damage;
        damagedEvent?.Invoke();

        if(Health <= 0f)
        {
            Die();
        }
        
    }

    private void Die()
    {
        Debug.Log("die");

        GetComponent<BossAnim>().DieAnim();
    }
}
