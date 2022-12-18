using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    private BossHealth healthController;
    private RectTransform rect;
    private float originXPos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        healthController = GameObject.Find("Boss").GetComponent<BossHealth>();
        originXPos = transform.position.x;

        healthController.damagedEvent += SetScale;
        healthController.damagedEvent += SetPosition;
    }

    private void SetScale()
    {
        float value = healthController.Health / healthController.maxHealth;

        rect.localScale = new Vector3(1 * value, 1, 1);
    }

    private void SetPosition()
    {
        float value = rect.rect.width - (healthController.Health / healthController.maxHealth * rect.rect.width);

        transform.position = new Vector3(originXPos - value / 2, transform.position.y, transform.position.z);
    }
}
