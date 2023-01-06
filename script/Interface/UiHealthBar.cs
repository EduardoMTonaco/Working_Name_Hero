using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthBar : MonoBehaviour
{
    private Slider SliderBar;
    private void Awake()
    {
        SliderBar = GetComponentInChildren<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        SliderBar.maxValue = health;
        SliderBar.value = health;
    }

    public void SetHealth(float health)
    {
        SliderBar.value = health;
    }

}
