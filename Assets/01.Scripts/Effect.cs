using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : Poolable
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public override void Reset()
    {
        particle.Play();
        StartCoroutine(Push());
    }

    IEnumerator Push()
    {
        yield return new WaitForSeconds(1);

        PoolManager.Instance.Push(this);
    }
}
