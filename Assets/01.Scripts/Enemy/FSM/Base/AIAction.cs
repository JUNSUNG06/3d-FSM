using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    public virtual void OnStartAction() { }
    public abstract void TakeAction();
}
