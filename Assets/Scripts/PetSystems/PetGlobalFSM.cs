using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGlobalFSM : MonoBehaviour
{
    public enum State
    {
        None,
        IndependentUS,
        CombateModeFSM,
        CollectModeFSM
    }
    // GESTION DEL ESTADO PRINCIPAL
    State currentState = State.IndependentUS;
    public State CurrentState
    {
        get => currentState;
        set
        {
            if (value == State.None) return;

            behaviourSystems[currentState].OnExit();
            currentState = value;
            behaviourSystems[currentState].OnStart();
        }
    }

    // TODOS LOS SISTEMAS DE DECISION DE LA MASCOTA
    public Dictionary<State, BehaviourSystem> behaviourSystems;


    //----------------------------------------------------------------
    //  METODOS DE UNITY
    //----------------------------------------------------------------

    private void Start()
    {
        behaviourSystems = new Dictionary<State, BehaviourSystem>
        {
            { State.None, null },
            { State.CombateModeFSM, new PetAtackModeFSM(gameObject) },
            { State.CollectModeFSM, new PetCollectModeFSM(gameObject) }
        };

        behaviourSystems.Add(State.IndependentUS, new IndependentPetUS(gameObject,
            behaviourSystems[State.CombateModeFSM] as PetAtackModeFSM,
            new PetRestModeFSM(gameObject),
            behaviourSystems[State.CollectModeFSM] as PetCollectModeFSM
            ));

        behaviourSystems[currentState].OnStart();
    }

    private void Update()
    {
        // ejecutar la accion correspondiente a la toma de decisiones actual
        behaviourSystems[currentState].OnUpdate();
    }
}
