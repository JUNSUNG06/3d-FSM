using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public float maxHealth = 0f;
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            health = Mathf.Clamp(health, 0, 100);
        }
    }
    private float health;
    private bool isDie = false;

    public Action damagedEvent;

    private PlayerController playerController;
    private PlayerPower playerPower;
    private void Start()
    {
        health = maxHealth;
        playerController = GetComponent<PlayerController>();
        playerPower = GetComponent<PlayerPower>();
    }

    public override void Damaged(float damage, Vector3 direction)
    {
        if (isDie || playerController.isDodge) return;
 
        if(playerController.isGaurd)
        {
            health -= damage * 0.33f;
            playerPower.DecreasePower(playerController.gaurdNPower);
        }
        else
        {
            health -= damage;
        }

        health = Mathf.Clamp(health, 0f, maxHealth);

        damagedEvent();

        /*f(!playerController.isHealing)
        {
            PlayerCamera.Instance.ShakeCam(6f, 0.1f);
            PoolManager.Instance.Pop("Blood Splash", transform.position);
        }*/

        PlayerCamera.Instance.ShakeCam(6f, 0.1f);
        PoolManager.Instance.Pop("Blood Splash", transform.position);
        PoolManager.Instance.Pop("HitSound", transform.position);

        if (health <= 0)
        {
            Die();
        }     
    }

    protected override void Die()
    {
        isDie = true;
        playerController.animator.DieAnim();
        playerController.enabled = false;
    }
}
