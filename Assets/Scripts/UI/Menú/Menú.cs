using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menú : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
