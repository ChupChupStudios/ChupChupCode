using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class SliderExploration : MonoBehaviour
{
    public Slider slider;

    public int maxExploration = 100;

    void Start()
    {
        slider.maxValue = maxExploration;
        slider.value = 0;

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

    void ActualizarSlider(object sender, float value)
    {
        slider.value += (value * 2);
    }
}