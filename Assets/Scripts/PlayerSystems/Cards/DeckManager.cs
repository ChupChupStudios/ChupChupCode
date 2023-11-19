using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour
{
    public SFXManager sFXManager;

    // SINGLETON
    public static DeckManager Instance;

    public LayerMask tileMask;

    // VARIABLES DE INSTANCIACION
    public const int INITIAL_CARDS = 5;
    public const int INITIAL_CARDS_TUTO = 4;
    //Vector3 CARD_SCALE = new(0.2f, 1.0f, 0.3f);
    Quaternion CARD_ROTATION = Quaternion.Euler(new Vector3(-60.0f, 45.0f, 0.0f));

    // POSICIONES DE LAS CARTAS
    Vector3[] cardPositions = {
        new Vector3(-7.0f, 0.0f, 2.0f),
        new Vector3(-5.5f, 0.0f, 0.5f),
        new Vector3(-4.0f, 0.0f, -1.0f),
        new Vector3(-2.5f, 0.0f, -2.5f),
        new Vector3(-1.0f, 0.0f, -4.0f),
        new Vector3(0.5f, 0.0f, -5.5f),
        new Vector3(2.0f, 0.0f, -7.0f),
    };
    int currentPositionIndex = 0;

    // LISTA DE CARTAS INSTANCIABLES
    [SerializeField] List<ACard> cardPrefabs = new();

    // LISTA DE CARTAS EN MANO
    public List<GameObject> cards = new();

    // POSICION DEL DUEŃO DEL MAZO
    [HideInInspector] public Transform ownerTransform;

    // GESTION DE LA CARTA SELECCIONADA
    ACard selectedCard;
    public ACard SelectedCard
    {
        get => selectedCard;
        set
        {
            if (value == null) return;

            var newCard = value;
            if (newCard != selectedCard)
            {
                // ya habia una carta escogida (se aprovecha el estado UsingCard):
                if (selectedCard != null && PlayerStateManager.Instance.CurrentState == PlayerStateManager.State.UsingCard)
                {
                    selectedCard.CardDeselected();
                    newCard.CardSelected();

                    selectedCard = newCard;
                    return;
                }

                // no habia una carta escogida:
                selectedCard = newCard;
                // PETICION DE CAMBIO DE ESTADO
                PlayerStateManager.Instance.CurrentState = PlayerStateManager.State.UsingCard;
            }
            else if (PlayerStateManager.Instance.CurrentState == PlayerStateManager.State.UsingCard)
            {
                selectedCard.CardDeselected();
                selectedCard = null;

                // NOTIFICAR NUEVO ESTADO (idle)
                PlayerStateManager.Instance.CurrentState = PlayerStateManager.State.Idle;
            }
            sFXManager.PickCardSound();
        }
    }

    

    //----------------------------------------------------------------
    //  METODOS DE UNITY
    //----------------------------------------------------------------

    void Awake()
    {
        // SINGLETON
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        // PLAYER TRANSFORM
        ownerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            // INSTANTIATE INITIAL CARDS
            for (int i = 0; i < INITIAL_CARDS_TUTO; i++)
            {
                //USE CREATE CARD
                CreateCard(cardPrefabs[i % cardPrefabs.Count]);
            }
        }
        else 
        {
            // INSTANTIATE INITIAL CARDS
            for (int i = 0; i < INITIAL_CARDS; i++)
            {
                //USE CREATE CARD
                CreateCard(cardPrefabs[i % cardPrefabs.Count]);
            }
        }
    }

    private void Start()
    {
        PlayerStateManager.Instance.ConsolidatedNewStateEvent += PlayerStateChanged;
    }


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public void CreateCard(ACard card)
    {
        if (card == null || !cardPrefabs.Contains(card)) return;
        if (cards.Count == 7) return;

        GameObject cardToInstantiate = card.gameObject;

        // Instantiate the card
        cards.Add(Instantiate(cardToInstantiate, cardPositions[cards.Count], CARD_ROTATION));

        // Update the index for the next card position
        //currentPositionIndex = (currentPositionIndex + 1) % cardPositions.Length;
    }

    // si el jugador puede usar cartas se notifica a la carta seleccionada
    void PlayerStateChanged(PlayerStateManager.State newState)
    {
        if (newState != PlayerStateManager.State.UsingCard) return;

        selectedCard.CardSelected();
    }

    public void BloquePulsadoCallBack(Block tile)
    {
        if (selectedCard == null) return;
        selectedCard.CheckAndExecute(tile);

        // NOTIFICAR CAMBIO DE ESTADO (idle)
        PlayerStateManager.Instance.CurrentState = PlayerStateManager.State.Idle;
    }

    public void Deselect()
    {
        // la gestion de deseleccion ya la hace la propiedad (SelectedCard)
        SelectedCard = selectedCard;
    }

    /* Devuelve:
     *   True si hay una casilla bajo la posicion del dueńo del mazo con un desplazamiento offset
     *   casilla con el GameObject de la casilla
     */
    public bool BloqueUsuarioDelMazo(Vector3 offset, out GameObject casilla)
    {
        return Utils.CustomRaycast(ownerTransform.position + Vector3.up * 3 + offset, Vector3.down, out casilla, tileMask);
    }

    public void UpdateCardsPositions()
    {
        int cont = 0;
        foreach(GameObject card in cards)
        {
            //Debug.Log(cardPositions[cont]);
            card.transform.position = cardPositions[cont];
            cont++;
        }
    }

}
