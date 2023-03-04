using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    private GameObject activePanel;
    public GameObject introPanel;
    public GameObject settingPanel;

    private void Start()
    {
        introPanel.SetActive(true);
        settingPanel.SetActive(false);
        activePanel = introPanel;
    }

    public void StartGame()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeActivePanel(GameObject newPanel)
    {
        activePanel.SetActive(false);
        activePanel = newPanel;
        activePanel.SetActive(true);
    }
}
