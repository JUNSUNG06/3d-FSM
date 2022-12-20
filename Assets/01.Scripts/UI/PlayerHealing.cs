using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealing : MonoBehaviour
{
    private PlayerController playerController;
    public TextMeshProUGUI text;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        text.text = $"x{playerController.healCount}";
    }
}
