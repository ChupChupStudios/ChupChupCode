using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    public GameObject tutorial;

    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void PopUp()
    {
        if (!tutorial.activeSelf)
            tutorial.SetActive(true);
        else
            tutorial.SetActive(false);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
