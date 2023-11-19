using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour
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

    public void SignText()
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
}