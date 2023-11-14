using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    None,
    Estamina,
    Ataque,
    Niebla
}

public abstract class ACard : MonoBehaviour
{
    public CardType type = CardType.None;
    public Color baseColor;

    public int cardIndex;

    Vector3 POSITION_OFFSET_IF_SELECTED = new(0.0f, 0.5f, 0.0f);

    [HideInInspector] public List<GameObject> affectedBlocks = new();

    public DeckManager deckManager;


    //----------------------------------------------------------------
    //  METODOS DE UNITY
    //----------------------------------------------------------------

    private void Start()
    {
        deckManager = DeckManager.Instance;
        GetComponent<Renderer>().material.color = baseColor;
    }

    private void OnMouseDown()
    {
        deckManager.SelectedCard = this;
    }


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public abstract void ShowEffectArea();
    public abstract void HideEffectArea();
    public abstract void CheckAndExecute(Block tile);

    public void CardSelected()
    {
        transform.position += POSITION_OFFSET_IF_SELECTED;
        ShowEffectArea();
    }

    public void CardDeselected()
    {
        transform.position -= POSITION_OFFSET_IF_SELECTED;
        HideEffectArea();
    }

    public void OnDestroy()
    {
        
        Debug.Log("ANTES: " + deckManager.cards.Count);
        Debug.Log("OBJETO LISTA " + deckManager.cards[0] + " OBJETO DEL SCRIPT " + gameObject);
        deckManager.cards.Remove(this.gameObject);
        Debug.Log("DESPUES: " + deckManager.cards.Count);
        deckManager.UpdateCardsPositions();
        
    }
}
