using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : Health
{
    public float maxHealth = 0f;
    public float Health { get; set; }
    private bool isDie = false;

    public Action damagedEvent;
    public ParticleSystem deadEffect;

    private void Start()
    {
        Health = maxHealth;
    }

    public override void Damaged(float damage, Vector3 direction)
    {
        Debug.Log("boss damaged");

        if(isDie) return;

        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);

        damagedEvent?.Invoke();
        PoolManager.Instance.Pop("Blood Splash 1", transform.position);
        PoolManager.Instance.Pop("HitSound", transform.position);
        if (Health <= 0f)
        {
            Die();
        }
        
    }

    protected override void Die()
    {
        Debug.Log("die");

        isDie = true;
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        GetComponent<BossAnim>().DieAnim();
        GetComponent<AIBrain>().enabled = false;
    }
}
