using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{

    public Slider slider;
    TMP_Text sliderValue;

    private void Start()
    {
        sliderValue = GetComponent<TMP_Text>();
        if (slider == null || sliderValue == null) return;


        slider.onValueChanged.AddListener(UpdateSlider);
    }

    public void UpdateSlider(float value)
    {
        sliderValue.text = slider.value.ToString();
    }
}
