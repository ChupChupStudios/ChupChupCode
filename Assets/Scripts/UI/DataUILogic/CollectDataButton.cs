using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectDataButton : MonoBehaviour
{
    Button collectButton;
    [Space]

    // CAMPOS:
    public TMP_Text nameTextField;
    public Slider ageSlider;
    public TMP_Dropdown genreDropdown;

    // SCRIPTABLE OBJECT PARA GUARDAR:
    public UserDataSO userData;


    void Start()
    {
        collectButton = GetComponent<Button>();

        // RECOLECTAR DATOS AL PULSAR EL BOTON
        collectButton.onClick.AddListener(CollectData);
    }

    // RECOLECTAR DATOS
    void CollectData()
    {
        if (nameTextField.text.Equals("") || nameTextField.text.Equals(string.Empty)) return;

        // GUARDAR DATOS
        userData.nombre = nameTextField.text;
        userData.edad = (int) ageSlider.value;
        userData.genero = genreDropdown.value;

        // PASAR A MENU PRINCIPAL
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
