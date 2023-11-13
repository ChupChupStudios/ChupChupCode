using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using System;

public class SliderStamina : MonoBehaviour
{
    public Slider slider;

    public int maxStamina = 100;

    public static event Action<float> WolfSpeedEvent;

    void Start()
    {
        StaminaCard.SliderEvent += CartaUsada;
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

    public void ActualizarSlider(object sender, float value)
    {
        slider.value -= value;
        if (slider.value <= 35) WolfSpeedEvent?. Invoke(2f);
    }
    public void CartaUsada(int life)
    {
        if (slider.value + life < 100)
        {
            slider.value += life;
            WolfSpeedEvent?.Invoke(1f);
        }
        else slider.value = 100;
    }
}