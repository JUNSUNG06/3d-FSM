using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ActiveOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
