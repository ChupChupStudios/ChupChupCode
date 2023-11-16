using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemManager : MonoBehaviour
{
    public ACard[] cardPrefab;
    public TextMeshProUGUI contadorText;
    private int objetosRecogidos = 0;
    public int totalObjetos = 0;
    public Button button;

    void Start()
    {
        // Establecer el texto inicial
        ActualizarContador();
    }

    void ActualizarContador()
    {
        contadorText.text = objetosRecogidos + "/" + totalObjetos;
    }

    public void RecogerObjeto()
    {
        objetosRecogidos++;
        ActualizarContador();
    }

    public void AllItemsCollected()
    {
        if (objetosRecogidos == totalObjetos)
        {
            if (DeckManager.Instance.cards.Count == 7) return;
            DeckManager.Instance.CreateCard(cardPrefab[0]);
            button.gameObject.SetActive(false);
        }
    }
}
