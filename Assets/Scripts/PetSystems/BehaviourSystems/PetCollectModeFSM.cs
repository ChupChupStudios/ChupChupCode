using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetCollectModeFSM : BehaviourSystem
{
    public enum State
    {
        None,
        SearchingItem,
        SearchingPlayer,
        WaitingToGoToPlayer
    }
    public State currentState = State.SearchingItem;

    private PickedBehaviour targetItem;


    //----------------------------------------------------------------
    //  METODOS TRONCALES
    //----------------------------------------------------------------

    public PetCollectModeFSM(PetBehaviour systemOwner, BehaviourSystem parentSystem) : base(systemOwner, parentSystem)
    {
        // deteccion de que el jugador entra o sale del campo de enfrente de la mascota
        systemOwner.petButton.onClick.AddListener(() =>
        {
            // boton desactivado
            systemOwner.petButton.interactable = false;

            // cambiar de estado a seguir al jugador
            DefinePath(forItem: false);
            currentState = State.SearchingPlayer;
        });

    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO RECOLECCION");

        // estado de entrada
        DefinePath(forItem: true);
        currentState = State.SearchingItem;
    }

    public override void OnUpdate()
    {
        //Utils.Log("\t\t\tUPDATE DE MODO RECOLECCION");

        switch (currentState)
        {
            case State.SearchingItem:
                Moving();
                break;
            case State.WaitingToGoToPlayer:
                Idle();
                break;
            case State.SearchingPlayer:
                Moving();
                break;
        }
    }


    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    void Moving()
    {
        // seguir camino
        bool targetReached = systemOwner.movementManager.SeguirCamino();

        if (!targetReached) return;
        // si el camino ya se ha terminado...

        // ... y estaba yendo a por un item:
        if (currentState == State.SearchingItem)
        {
            // recoger el item
            targetItem.gameObject.SetActive(false);

            // esperar a que el jugador indique que la mascota debe dar la carta
            currentState = State.WaitingToGoToPlayer;
            // activar el boton
            systemOwner.petButton.interactable = true;
        }
        // ... y estaba yendo a por el jugador: (FINAL DE FSM)
        else if (currentState == State.SearchingPlayer)
        {
            // DAR CARTA
            targetItem.PickUpItem();

            Utils.Log("DAR EL ITEM");

            // resetear tiempo sin ayudar al jugador
            systemOwner.statusVariables.timeWithoutSupport = 0f;

            // informar de final de FSM
            CurrentSystemFinishedEvent?.Invoke();
        }
    }

    void Idle()
    {
        // ESPERANDO SEÑAL DE JUGADOR
    }

    void DefinePath(bool forItem)
    {
        Transform targetTransform;
        // para coger item:
        if (forItem)
        {
            // BUSCAR ITEM MAS CERCANO
            targetTransform = SearchClosestItem();
            if (targetTransform == null) return;
        }
        // para ir con el jugador:
        else targetTransform = systemOwner.player.transform;

        // definir camino hasta objetivo
        systemOwner.movementManager.DefinirCamino(targetTransform, persecucion: !forItem);
    }

    // buscar item mas cercano
    Transform SearchClosestItem()
    {
        Transform result = null;
        float minDistance = Mathf.Infinity;

        // recorrer todos los items para encontrar al mas cercano
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            float distance = Vector3.Distance(item.transform.position, systemOwner.transform.position);
            if (distance < minDistance)
            {
                result = item.transform;
                minDistance = distance;
            }
        }

        if (result != null) targetItem = result.GetComponent<PickedBehaviour>();

        return result;
    }
}
