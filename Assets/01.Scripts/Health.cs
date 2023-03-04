using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamage
{
    public virtual void Damaged(float damage, Vector3 direction) { }
    
    protected abstract void Die();
}
