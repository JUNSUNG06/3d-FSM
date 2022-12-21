using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain brain;

    protected virtual void Start()
    {
        brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public virtual void OnStartAction() { }
    public virtual void OnEndActino() { }
    public abstract void TakeAction();
}
