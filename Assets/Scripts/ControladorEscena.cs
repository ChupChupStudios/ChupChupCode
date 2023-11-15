using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
