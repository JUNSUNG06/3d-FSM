using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEvent : MonoBehaviour
{
    public void ActiveCharacters()
    {
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<AIBrain>().enabled = true;
        FindObjectOfType<Mouse>().enabled = true;
    }
}
