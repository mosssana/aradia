using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
        EventManager.AddListener(EventName.PlayerLifeChanged, UpdateBar);
    }

    void UpdateBar(float life, float totalLife)
    {
        healthBar.fillAmount = life / totalLife;
    }
}
