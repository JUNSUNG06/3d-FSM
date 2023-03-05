using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class Intro : MonoBehaviour
{
    private GameObject activePanel;
    public GameObject introPanel;
    public GameObject settingPanel;
    public float fadeTime = 1f;
    public Material textMaterial;
    private Color originTextColor;

    private void Start()
    {
        introPanel.SetActive(true);
        settingPanel.SetActive(false);
        activePanel = introPanel;
        originTextColor = textMaterial.GetColor("_FaceColor");
    }

    private void OnDisable()
    {
        textMaterial.SetColor("_FaceColor", originTextColor);
    }

    public void StartGame()
    {
        StartCoroutine(SetStartGame());
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

    IEnumerator SetStartGame()
    {
        ChangeActivePanel(introPanel);
        foreach (Transform child in activePanel.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(1);

        while (true)//텍스트 페이드
        {
            Color color = textMaterial.GetColor("_FaceColor");
            float alpha = color.a;
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime / fadeTime);
            color.a = alpha;
            textMaterial.SetColor("_FaceColor", color);

            if (alpha < 0.001f)
            {
                break;
            }

            yield return null;
        }

        GameObject.Find("Cinematic").GetComponent<PlayableDirector>().Play();

        introPanel.SetActive(false);
        settingPanel.SetActive(false);
        textMaterial.SetColor("_FaceColor", originTextColor);//바꾼 메트리얼 색상 복구
    }
}
