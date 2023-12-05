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

    public Button bestiario;
    public GameObject panelBestiario;
    public GameObject pantallaAjustes;

    [SerializeField] private AudioClip clickButton;

    void Start()
    {
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        auxiliarPlane.SetActive(false);
        // Inicializar el estado del juego en Running
        currentState = GameState.Running;

        //MovementComponent = GameObject.FindObjectOfType<MovementComponent>();
        //ShootComponent = GameObject.FindObjectOfType<ShootComponent>();

        bestiario.onClick.AddListener(() => { SFXManager.Instance.EjecutarSonido(clickButton); panelBestiario.SetActive(true); });
    }

    void Update()
    {
        // Comprobar si se ha pulsado el botón de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Comprobar si el juego está en ejecución
            if (currentState == GameState.Running)
            {
                ShowPauseMenu();
            }
            else if (currentState == GameState.Paused)
            {
                HidePauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        // Mostrar el menú de pausa
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = true;
        auxiliarPlane.SetActive(true);

        // Pausar el juego
        currentState = GameState.Paused;
        Time.timeScale = 0;

    }

    public void HidePauseMenu()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        // Ocultar el menú de pausa
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        auxiliarPlane.SetActive(false);

        // Reanudar el juego
        currentState = GameState.Running;
        Time.timeScale = 1;

    }

    public void PauseButton()
    {
        if (currentState == GameState.Running)
        {
            ShowPauseMenu();
        }
        else if (currentState == GameState.Paused)
        {
            HidePauseMenu();
        }
    }

    public void MainMenu()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        // Volver al menú principal
        Canvas pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        auxiliarPlane.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ExitGame()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        // Cerrar el juego
        UnityEngine.Application.Quit();
    }

    public void RestartLevel()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);

        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual);
        Time.timeScale = 1;
    }

    public void Configuration()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        if (!pantallaAjustes.activeSelf)
        {
            pantallaAjustes.SetActive(true);
        }
        else
        {
            pantallaAjustes.SetActive(false);
        }
    }
}