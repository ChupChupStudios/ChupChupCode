using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class SliderExploration : MonoBehaviour
{
    public Slider slider;
    public ACard[] cardPrefab;

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
    public void OnClickSlider()
    {
        if (slider.value >= 100)
        {
            if (DeckManager.cards.Count == 7) return;
            int cardType;
            cardType = Random.Range(0, 5);
            DeckManager.Instance.CreateCard(cardPrefab[cardType]);
            slider.value = 0;
        }
    }
}