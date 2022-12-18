using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamage
{
    public float maxHealth = 0f;
    public float Health { get; set; }

    public Action damagedEvent;
    private void Start()
    {
        Health = maxHealth;
    }

    private void Update()
    {
        Healing();
    }

    private void Healing()
    {
        /*if (isAttack || isHealing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //
        }*/
    }

    public void Damaged(float damage, Vector3 direction)
    {
        Health -= damage;

        damagedEvent();
        //방향 따라 넉백

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("player die");
    }
}
