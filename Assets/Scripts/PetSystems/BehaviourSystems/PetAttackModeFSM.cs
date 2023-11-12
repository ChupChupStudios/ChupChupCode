using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAttackModeFSM : BehaviourSystem
{
    public enum State
    {
        None,
        SearchEnemy,
        Moving,
        Attacking
    }
    public State currentState = State.SearchEnemy;

    // informacion de busqueda del enemigo
    public Transform targetEnemyTransform;
    private PetMovement movementManager;


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public PetAttackModeFSM(PetBehaviour systemOwner) : base(systemOwner)
    {
        movementManager = systemOwner.GetComponent<PetMovement>();
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO ATAQUE");

        // estado de entrada
        currentState = State.SearchEnemy;
    }

    public override void OnExit()
    {
        Utils.Log("SALIENDO DE MODO ATAQUE\n" +
            ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        targetEnemyTransform = null;
    }

    public override void OnUpdate()
    {
        Utils.Log("\t\t\tUPDATE DE MODO ATAQUE");

        switch (currentState)
        {
            case State.SearchEnemy:
                DefinirRuta();
                break;
            case State.Moving:
                Moving();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    void Moving()
    {
        bool nextToEnemy = movementManager.SeguirCamino();

        if (nextToEnemy)
            currentState = State.Attacking;
    }

    void Attacking()
    {
        // si no esta a distancia de ataque, pasar a buscar ruta
        if (!systemOwner.attack.enemiesTouched.Contains(targetEnemyTransform.gameObject))
            currentState = State.SearchEnemy;

        // realizar ataque
        // posibilidades:
        //  - atacar al objetivo concreto
        //  - atacar a todo enemigo en rango de ataque
        targetEnemyTransform.GetComponent<EnemyVariablesManager>().GetDamage();
    }


    void DefinirRuta()
    {
        if (targetEnemyTransform == null)
        {
            ////////////////////////////////////////////////
            // BUSCAR ENEMIGO MAS CERCANO
            targetEnemyTransform = systemOwner.closestEnemy.closestEnemyTransform;
            if (targetEnemyTransform == null) return;

            Utils.Log("Ruta marcada por enemigo mas cercano");
        }

        // definir camino hasta enemigo
        Nodo enemyNode = GestorCuadricula.Instance.NodoCoincidente(targetEnemyTransform.position);
        movementManager.DefinirCamino(enemyNode);

        currentState = State.Moving;
    }
}
