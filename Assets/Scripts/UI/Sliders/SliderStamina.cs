using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderStamina : MonoBehaviour
{
    public Slider slider;

    public int maxStamina = 100;

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

    void Update()
    {
        if (slider.value <= 0)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }

    public void ActualizarSlider(object sender, float value)
    {
        slider.value -= value;
    }

    public void CartaUsada(int life)
    {
        if (slider.value + life < 100) slider.value += life;
        else slider.value = 100;
    }
}