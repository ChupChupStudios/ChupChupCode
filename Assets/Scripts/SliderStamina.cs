using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class SliderStamina : MonoBehaviour
{
    public Slider slider;

    public int staminaPerStep = 15;
    public int maxStamina = 100;

    void Start()
    {
        slider.maxValue = maxStamina;
        slider.value = maxStamina;

        // Suscribirse al evento CasillaMovida
        Movimiento mov = FindObjectOfType<Movimiento>();
        if (mov != null)
        {
            mov.CasillaMovida += ActualizarSlider;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el script Movimiento en la escena.");
        }
    }

    void ActualizarSlider()
    {
        slider.value -= staminaPerStep;
    }
}