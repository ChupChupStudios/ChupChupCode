using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyItemManager : MonoBehaviour
{
    public ACard[] cardPrefab;
    public TextMeshProUGUI contadorText;
    //private int objetosRecogidos = 0;
    public int totalObjetos = 0;
    public int totalObjetosTutorial = 1;
    public Button button;

    public ItemsCollectedSO itemsCollected; 

    void Start()
    {
        ActualizarContador(itemsCollected.CollectedItems);
    }

    void ActualizarContador(int piezasActuales)
    {
        contadorText.text = piezasActuales + "/" + totalObjetos;
    }

    public void RecogerObjeto()
    {
        ActualizarContador(itemsCollected.CollectedItems+1);
    }

    public void AllItemsCollected()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (itemsCollected.CollectedItems == totalObjetosTutorial)
            {
                if (DeckManager.Instance.cards.Count == 7) return;
                DeckManager.Instance.CreateCard(cardPrefab[0]);
                button.gameObject.SetActive(false);
            }
        }
        if (itemsCollected.CollectedItems == totalObjetos)
        {
            if (DeckManager.Instance.cards.Count == 7) return;
            DeckManager.Instance.CreateCard(cardPrefab[0]);
            button.gameObject.SetActive(false);
        }
    }
}
