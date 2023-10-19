using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class SliderCircular : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public float currentHealth;
    public float maxHealth;

    public Image imageBorder;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = currentHealth / maxHealth;
        slider.value = value;
        imageBorder.fillAmount = value + 0.02f;
    }
}
