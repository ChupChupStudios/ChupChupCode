using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpKeyItems : MonoBehaviour
{
    public string textoDelCartel;
    public Canvas cartelCanvas;
    private TextMeshProUGUI textoUI;
    public GameObject auxiliarPlane;

    private void Start()
    {
        cartelCanvas.gameObject.SetActive(false);
        textoUI = cartelCanvas.GetComponentInChildren<TextMeshProUGUI>();
        auxiliarPlane.SetActive(false);
    }
    public void LoreText()
    {
        textoUI.text = textoDelCartel;
        cartelCanvas.gameObject.SetActive(true);
        auxiliarPlane.SetActive(true);
        Time.timeScale = 0;
    }

    public void CerrarCanvas()
    {
        cartelCanvas.gameObject.SetActive(false);
        auxiliarPlane.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el script del contador
            KeyItemManager keyItemManager = FindObjectOfType<KeyItemManager>();

            // Comprobar si se encontr� el script
            if (keyItemManager != null)
            {
                // Incrementar el contador y destruir el objeto recolectable
                keyItemManager.RecogerObjeto();
                LoreText();
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("No se encontr� el script del contador.");
            }
        }
    }
}
