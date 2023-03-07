using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<AIBrain>().enabled = false;
        FindObjectOfType<PlayerCamera>().enabled = false;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
