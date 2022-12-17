using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHealth = 0f;
    [SerializeField] private float health = 0f;

    private void Start()
    {
        health = maxHealth;
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
        health -= damage;

        //방향 따라 넉백

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("player die");
    }
}
