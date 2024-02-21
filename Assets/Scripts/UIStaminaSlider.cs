using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIssa controlli staminan esitt�miselle. Mostly slider value change.
/// </summary>
public class UIStaminaSlider : MonoBehaviour
{
    [SerializeField] Slider slider; //Staminan slider componentti

    void Start()
    {
        slider = GetComponent<Slider>();

        //TEMP: Testi ett� slider muuttuu
        slider.value = 30;
    }

}
