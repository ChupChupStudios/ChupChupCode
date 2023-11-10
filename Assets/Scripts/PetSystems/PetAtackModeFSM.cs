using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAtackModeFSM : BehaviourSystem
{
    public enum State
    {
        None,
        Moving,
        Atacking
    }
    public State currentState = State.Moving;

    // informacion de busqueda del enemigo
    public Transform targetEnemyTransform;
    private PetMovement movementManager;


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public PetAtackModeFSM(GameObject systemOwner) : base(systemOwner)
    {
        movementManager = systemOwner.GetComponent<PetMovement>();
    }

    public override void OnStart()
    {
        // estado de entrada
        currentState = State.Moving;

        DefinirRuta();
    }

    public override void OnExit()
    {
        Debug.Log("Saliendo de: MODO ATAQUE");
        targetEnemyTransform = null;
    }

    public override void OnUpdate()
    {
        switch (currentState)
        {
            case State.Moving:
                Moving();
                break;
            case State.Atacking:
                Atacking();
                break;
        }
    }

    void Moving()
    {
        bool nextToEnemy = movementManager.SeguirCamino();

        if (nextToEnemy)
            currentState = State.Atacking;
    }

    void Atacking()
    {
        Debug.Log("Mascota atacando");
        // si no esta a distancia de ataque, buscar ruta y perseguir
    }


    void DefinirRuta()
    {
        if (targetEnemyTransform == null)
        {
            Debug.Log("No hay enemigo objetivo");
            ////////////////////////////////////////////////
            // BUSCAR ENEMIGO MAS CERCANO
            return;
        }

        // definir camino hasta enemigo
        Nodo enemyNode = GestorCuadricula.Instance.NodoCoincidente(targetEnemyTransform.position);
        movementManager.DefinirCamino(enemyNode);
    }
}
