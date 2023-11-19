using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    public GameObject tutorial;
    [SerializeField] private AudioClip clickButton;
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private SceneSO previousScene;

    public void CambiarEscena(string nombre)
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        SceneManager.LoadScene(nombre);
    }

    public void PopUp()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        if (!tutorial.activeSelf)
            tutorial.SetActive(true);
        else
            tutorial.SetActive(false);
    }

    public void VolverMenu()
    {
        SFXManager.Instance.CambiarMúsica(mainTheme, true);
        SFXManager.Instance.EjecutarSonido(clickButton);
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void CerrarJuego()
    {
        SFXManager.Instance.EjecutarSonido(clickButton);
        Application.Quit();
    }

    public void ReiniciarNivel()
    {
        SFXManager.Instance.CambiarMúsica(mainTheme, true);
        SFXManager.Instance.EjecutarSonido(clickButton);
        SceneManager.LoadScene(previousScene.numEscena);
    }
}
