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
    public GameObject casillaGoal;

    int itMostrar;
    bool subir;

    private void Start()
    {
        itMostrar = 0;
        subir = true;
        cartelCanvas.gameObject.SetActive(false);
        textoUI = cartelCanvas.GetComponentInChildren<TextMeshProUGUI>();
        auxiliarPlane.SetActive(false);
    }

    void Update()
    {
        if (itMostrar < 3)
        {
            if (subir)
            {
                transform.position += Vector3.up * Time.deltaTime;
                if (transform.position.y >= 2.5)
                    subir = false;
            }
            else
            {
                transform.position -= Vector3.up * Time.deltaTime;
                if (transform.position.y <= 0.7)
                {
                    subir = true;
                    itMostrar++;
                    transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
                }
            }
        }
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

            // Comprobar si se encontró el script
            if (keyItemManager != null)
            {
                // Incrementar el contador y destruir el objeto recolectable
                keyItemManager.RecogerObjeto();

                StartCoroutine(EsperarYMostrarTexto());

                //LoreText();
                
            }
            else
            {
                Debug.LogError("No se encontró el script del contador.");
            }
        }
    }

    IEnumerator EsperarYMostrarTexto()
    {
        // Espera durante 3 segundos
        yield return new WaitForSeconds(0.25f);

        if (casillaGoal != null) casillaGoal.GetComponent<Goal>().enabled = true;

        // Llama a la función deseada después de esperar
        LoreText();
        gameObject.SetActive(false);
    }

}
