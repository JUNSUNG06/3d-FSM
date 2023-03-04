using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public float maxHealth = 0f;
    public float Health { get; set; }

    private bool isDie = false;

    public Action damagedEvent;

    private PlayerController playerController;
    private PlayerPower playerPower;
    private void Start()
    {
        Health = maxHealth;
        playerController = GetComponent<PlayerController>();
        playerPower = GetComponent<PlayerPower>();
    }

    public override void Damaged(float damage, Vector3 direction)
    {
        if (isDie || playerController.isDodge) return;
 
        if(playerController.isGaurd)
        {
            Health -= damage * 0.33f;
            playerPower.DecreasePower(playerController.gaurdNPower);
        }
        else
        {
            Health -= damage;
        }

        Health = Mathf.Clamp(Health, 0f, maxHealth);

        damagedEvent();
        
        if(!playerController.isHealing)
        {
            PlayerCamera.Instance.ShakeCam(6f, 0.1f);
            PoolManager.Instance.Pop("Blood Splash", transform.position);
        }

        if (Health <= 0)
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
