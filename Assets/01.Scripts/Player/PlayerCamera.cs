using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public Transform playerTrm = null;  

    private void Update()
    {
        transform.position = playerTrm.position - offset;
    }
}