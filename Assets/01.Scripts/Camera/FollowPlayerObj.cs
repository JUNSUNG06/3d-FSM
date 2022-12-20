using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerObj : MonoBehaviour
{
    private Transform playerTrm;

    private void Start()
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = playerTrm.position;
    }
}
