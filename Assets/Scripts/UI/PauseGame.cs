using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class PauseGame : MonoBehaviour
{
    // Enumeración para los diferentes estados del juego
    public enum GameState { Running, Paused }

    // Variable para almacenar el estado actual del juego
    private GameState currentState;

    public GameObject auxiliarPlane;

    void Start()
    {
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        auxiliarPlane.SetActive(false);
        // Inicializar el estado del juego en Running
        currentState = GameState.Running;

        //MovementComponent = GameObject.FindObjectOfType<MovementComponent>();
        //ShootComponent = GameObject.FindObjectOfType<ShootComponent>();
    }

    void Update()
    {
        // Comprobar si se ha pulsado el botón de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Comprobar si el juego está en ejecución
            if (currentState == GameState.Running)
            {
                // Pausar el juego
                currentState = GameState.Paused;
                Time.timeScale = 0;
                ShowPauseMenu();
            }
            else if (currentState == GameState.Paused)
            {
                // Reanudar el juego
                currentState = GameState.Running;
                Time.timeScale = 1;
                HidePauseMenu();
            }
        }
    }

    private void ShowPauseMenu()
    {
        // Mostrar el menú de pausa
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = true;
        auxiliarPlane.SetActive(true);
    }

    private void HidePauseMenu()
    {
        // Ocultar el menú de pausa
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        auxiliarPlane.SetActive(false);
    }

    public void MainMenu()
    {
        // Volver al menú principal
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        auxiliarPlane.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        // Cerrar el juego
        UnityEngine.Application.Quit();
    }
}