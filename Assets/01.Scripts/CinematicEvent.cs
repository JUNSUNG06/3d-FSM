using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEvent : MonoBehaviour
{
    public GameObject inGameUI;

    public void ActiveCharacters()
    {
        inGameUI.SetActive(true);
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<AIBrain>().enabled = true;
        FindObjectOfType<Mouse>().enabled = true;
    }
}
