using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    public float maxPower = 0f;
    public float Power { get; set; }
    public Action activeEvent;
    [SerializeField] private float recoveryTime = 0f;
    [SerializeField] private float recoveryAmount = 0f;

    private void Start()
    {
        Power = maxPower;
        Mathf.Clamp(Power, maxPower, 0f);
    }

    public bool canActive(float necessaryPower)
    {
        return Power >= necessaryPower;
    }

    public void DecreasePower(float usePower)
    {
        Power -= usePower;
        activeEvent?.Invoke();

        StopCoroutine("RecoveryPower");
        StartCoroutine("RecoveryPower");
    }

    IEnumerator RecoveryPower()
    {
        yield return new WaitForSeconds(recoveryTime);
        Debug.Log("1");

        while(Power < maxPower)
        {
            Power += recoveryAmount * Time.deltaTime;
            activeEvent?.Invoke();

            yield return null;
        }
    }
}
