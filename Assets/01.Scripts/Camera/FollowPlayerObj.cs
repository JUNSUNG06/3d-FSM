using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerObj : MonoBehaviour
{
    private Transform playerTrm;
    public float yOffset;

    private void Start()
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(playerTrm.position.x , playerTrm.position.y + yOffset, playerTrm.position.z);
    }
}
