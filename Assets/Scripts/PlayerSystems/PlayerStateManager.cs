using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // SINGLETON
    public static PlayerStateManager Instance;

    // POSIBLES ESTADOS
    public enum State
    {
        None,
        Idle,
        Movement,
        UsingCard
    }

    // GESTION DE ESTADOS
    State currentState = State.Idle;
    State requestedState = State.None;
    public State CurrentState
    {
        get => currentState;
        set
        {
            if (value == State.None || value == requestedState) return;

            // no se esta haciendo nada o se ha pedido no hacer nada
            if (currentState == State.Idle || value == State.Idle || value == State.Movement)
            {
                currentState = (requestedState == State.None) ? value : requestedState;
                requestedState = State.None;

                ConsolidatedNewStateEvent?.Invoke(currentState);
            }
            // no es posible hacer el cambio de estado inmediatamente
            else
            {
                requestedState = value;
                StateChangeRequestedEvent?.Invoke(requestedState); 
            }
        }
    }

    // EVENTOS
    public event Action<State> StateChangeRequestedEvent;
    public event Action<State> ConsolidatedNewStateEvent;


    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
}
