using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;
    private float maxHealth;

    public void setHealth(int health)
    {
        slider.value = health;
        if (health > -1) healthText.text = health + "/" + maxHealth;
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        maxHealth = health;
        healthText.text = health + "/" + maxHealth;
    }
}
