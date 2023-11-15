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

    protected DeckManager deckManager;


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
        DeckManager.Instance.cards.Remove(this);
        deckManager.UpdateCardsPositions();
    }
}
