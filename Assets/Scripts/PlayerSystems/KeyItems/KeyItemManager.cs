using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemManager : MonoBehaviour
{
    public ACard[] cardPrefab;
    public TextMeshProUGUI contadorText;
    //private int objetosRecogidos = 0;
    public int totalObjetos = 0;
    public Button button;

    public ItemsCollectedSO itemsCollected; 

    void Start()
    {
        ActualizarContador();
    }

    void ActualizarContador()
    {
        contadorText.text = itemsCollected.collectedItems + "/" + totalObjetos;
    }

    public void RecogerObjeto()
    {
        //objetosRecogidos++;
        itemsCollected.collectedItems++;
        ActualizarContador();
    }

    public void AllItemsCollected()
    {
        if (itemsCollected.collectedItems == totalObjetos)
        {
            if (DeckManager.Instance.cards.Count == 7) return;
            DeckManager.Instance.CreateCard(cardPrefab[0]);
            button.gameObject.SetActive(false);
        }
    }
}
