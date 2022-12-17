using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatusSO", menuName = "Scriptable Object/EnemyStatusSO")]
public class EnemyStatusSO : ScriptableObject
{
    public float moveSpeed;
    public float maxHealth;
}
