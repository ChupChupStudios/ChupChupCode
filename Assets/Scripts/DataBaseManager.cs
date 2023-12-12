using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField inputName;
    public TMP_InputField inputPassword;
    public TMP_Text textYears;
    public Slider sliderAge;
    public Dropdown dropdownSex;
    public Button continueButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        continueButton.gameObject.SetActive(CheckData());
    }

    public void OnValueChangedSlider()
    {
        textYears.text = sliderAge.value.ToString();
    }

    public bool CheckData()
    {
        if (inputName.text == "") return false;
        if (inputPassword.text == "") return false;
        return true;
    }
}
