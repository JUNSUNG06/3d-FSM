using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    public void PlayFootSetpSound()
    {
        PoolManager.Instance.Pop("BossFootStep", transform.position);
    }
}
