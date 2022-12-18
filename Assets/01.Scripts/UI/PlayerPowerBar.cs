using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerBar : MonoBehaviour
{
    private PlayerPower powerController;
    private RectTransform rect;
    private float originXPos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        powerController = GameObject.Find("Player").GetComponent<PlayerPower>();
        originXPos = transform.position.x;

        powerController.activeEvent += SetScale;
        powerController.activeEvent += SetPosition;
    }

    private void SetScale()
    {
        float value = powerController.Power / powerController.maxPower;

        rect.localScale = new Vector3(1 * value, 1, 1);
    }

    private void SetPosition()
    {
        float value = rect.rect.width - (powerController.Power / powerController.maxPower * rect.rect.width);

        transform.position = new Vector3(originXPos - value / 2, transform.position.y, transform.position.z);
    }
}
