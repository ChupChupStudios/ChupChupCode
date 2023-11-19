using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menú : MonoBehaviour
{
    public void OnClickNuevaPartida()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;

        // Cargar la siguiente escena por índice
        SceneManager.LoadScene(indiceEscenaActual + 1);
    }

    public void OnCLickCreditos()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OnCLickSalirCreditos()
    {
        SceneManager.LoadScene("Menú");
    }
}
