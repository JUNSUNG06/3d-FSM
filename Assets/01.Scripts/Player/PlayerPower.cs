using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [SerializeField] private float maxPower = 0f;
    [SerializeField] private float currentPower = 0f;
    [SerializeField] private float recoveryTime = 0f;
    [SerializeField] private float recoveryAmount = 0f;

    private void Start()
    {
        currentPower = maxPower;
        Mathf.Clamp(currentPower, maxPower, 0f);
    }

    public bool canActive(float necessaryPower)
    {
        return currentPower >= necessaryPower;
    }

    public void DecreasePower(float usePower)
    {
        currentPower -= usePower;

        StopCoroutine("RecoveryPower");
        StartCoroutine("RecoveryPower");
    }

    IEnumerator RecoveryPower()
    {
        yield return new WaitForSeconds(recoveryTime);
        Debug.Log("1");

        while(currentPower < maxPower)
        {
            currentPower += recoveryAmount * Time.deltaTime;

            yield return null;
        }
    }
}
