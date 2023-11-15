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
        Attacking,
        WaitingToAttack
    }
    public State currentState = State.SearchEnemy;

    // informacion de busqueda del enemigo
    public Transform targetEnemyTransform;

    float timeWaiting = 0f;

    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public PetAttackModeFSM(PetBehaviour systemOwner, BehaviourSystem parentSystem) : base(systemOwner, parentSystem)
    {
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO ATAQUE");

        // estado de entrada
        currentState = State.SearchEnemy;
    }

    public override void OnUpdate()
    {
        //Utils.Log("\t\t\tUPDATE DE MODO ATAQUE");

        switch (currentState)
        {
            case State.SearchEnemy:
                DefinePath();
                break;
            case State.Moving:
                Moving();
                break;
            case State.Attacking:
                Attacking();
                break;
            case State.WaitingToAttack:
                WaitingToAttack();
                break;
        }
    }


    //----------------------------------------------------------------
    //----------------------------------------------------------------


    void Moving()
    {
        bool nextToEnemy = systemOwner.movementManager.SeguirCamino();

        if (nextToEnemy)
        {
            currentState = State.Attacking;
        }
    }

    void Attacking()
    {
        // ENEMIGO FUERA DE DISTANCIA DE ATAQUE
        if (!systemOwner.attack.enemiesTouched.Contains(targetEnemyTransform.gameObject))
        {
            currentState = State.SearchEnemy;
            return;
        }

        // REALIZAR ATAQUE
        // posibilidades:
        //  - atacar al objetivo concreto   (la que esta implementada)
        //  - atacar a todo enemigo en rango de ataque
        EnemyVariablesManager enemyVariables = targetEnemyTransform.GetComponent<EnemyVariablesManager>();
        enemyVariables.GetDamage();
        // reducir estamina
        systemOwner.statusVariables.AddStamina(-systemOwner.statusVariables.attackCost);

        // EL ENEMIGO NO HA MUERTO
        if (enemyVariables.lifePoints > 0)
        {
            currentState = State.WaitingToAttack;
            return;
        }

        // EL ENEMIGO HA MUERTO
        systemOwner.statusVariables.timeWithoutCombat = 0f;
        targetEnemyTransform = null;
        currentState = State.SearchEnemy;

        // NOTIFICAR FIN DE FSM
        CurrentSystemFinishedEvent.Invoke();
    }

    // esperar para volver a atacar
    void WaitingToAttack()
    {
        timeWaiting += Time.deltaTime;
        if (timeWaiting >= systemOwner.statusVariables.timeBetweenAttacks)
        {
            timeWaiting = 0f;
            currentState = State.Attacking;
        }
    }


    void DefinePath()
    {
        if (targetEnemyTransform == null)
        {
            // COGER ENEMIGO MAS CERCANO
            targetEnemyTransform = SearchClosestEnemy();
            if (targetEnemyTransform == null) return;

            Utils.Log("Ruta marcada por enemigo mas cercano");
        }

        // definir camino hasta enemigo
        systemOwner.movementManager.DefinirCamino(targetEnemyTransform, persecucion: true);

        currentState = State.Moving;
    }

    // buscar item mas cercano
    Transform SearchClosestEnemy()
    {
        Transform result = null;
        float minDistance = Mathf.Infinity;

        // recorrer todos los enemigos para encontrar al mas cercano
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector3.Distance(enemy.transform.position, systemOwner.transform.position);
            if (distance < minDistance)
            {
                result = enemy.transform;
                minDistance = distance;
            }
        }

        return result;
    }
}
