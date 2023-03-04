using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : Poolable
{
    public enum EffectType
    {
        PARTICLE,
        SOUND
    }
    public EffectType effectType;

    private ParticleSystem particle;

    public override void Reset()
    {
        if (effectType == EffectType.PARTICLE)
            PlayEffect();
        else if (effectType == EffectType.SOUND)
            PlaySound();
        StartCoroutine(Push());
    }

    IEnumerator Push()
    {
        yield return new WaitForSeconds(1);

        PoolManager.Instance.Push(this);
    }

    private void PlayEffect()
    {
        GetComponent<ParticleSystem>().Play();
    }

    private void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>(); 
        //audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
