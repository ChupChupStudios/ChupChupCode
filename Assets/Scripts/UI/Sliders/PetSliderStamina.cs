using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetSliderStamina : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        PetVariablesManager petVariables = GameObject.FindObjectOfType<PetVariablesManager>();
        slider.maxValue = petVariables.MAX_STAMINA;
        slider.value = petVariables.MAX_STAMINA;
        petVariables.StaminaReducedEvent += UpdateSlider;
    }

    private void UpdateSlider(float newValue)
    {
        Utils.Log($"cambio en stamina {newValue}");
        slider.value = newValue;
    }
}
