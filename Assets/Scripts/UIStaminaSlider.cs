using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

/// <summary>
/// UI representation of stamina. Mostly slider value change.
/// </summary>
public class UIStaminaSlider : MonoBehaviour
{
    [SerializeField] private Slider slider; //Stamina slider component

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetSliderPercentage(float percentage)
    {
        if (percentage < 0f || percentage > 1f)
        {
            Debug.LogError("Percentage representing value must be between 0 and 1.");
            return;
        }

        slider.value = percentage * slider.maxValue;
    }
}
