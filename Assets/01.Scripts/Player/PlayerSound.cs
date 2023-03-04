using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void PlayFootSetpSound()
    {
        PoolManager.Instance.Pop("Footstep Concrete Walking 1_01", transform.position);
    }

    public void PlaySwingSound()
    {
        PoolManager.Instance.Pop("Swing1-Free-1", transform.position);
    }
}
