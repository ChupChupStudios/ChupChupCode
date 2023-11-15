using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRestModeFSM : BehaviourSystem
{
    enum State
    {
        None,
        MovingToRest,
        Resting
    }
    State currentState = State.None;

    float timeResting = 0f;

    public PetRestModeFSM(PetBehaviour systemOwner, BehaviourSystem parentSystem) : base(systemOwner, parentSystem)
    {
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO DESCANSO");

        // estado de entrada
        DefinePath();
        currentState = State.MovingToRest;
    }

    public override void OnUpdate()
    {
        //Utils.Log("\t\t\tUPDATE DE MODO DESCANSO");

        switch (currentState)
        {
            case State.MovingToRest:
                Moving();
                break;
            case State.Resting:
                Resting();
                break;
        }
    }

    void Moving()
    {
        // seguir camino
        bool targetReached = systemOwner.movementManager.SeguirCamino();

        if (!targetReached) return;
        // si el camino ya se ha terminado

        // pasar a descansar
        currentState = State.Resting;
        // vigilar alrededores
        systemOwner.enemyDetection.DetectedEvent += () =>
        {
            // TERMINAR FSM
            if (systemOwner.statusVariables.stamina >= systemOwner.statusVariables.MAX_STAMINA / 2)
            {
                CurrentSystemFinishedEvent?.Invoke();
                return;
            }
        };
    }

    void Resting()
    {
        // TIEMPO DESCANSANDO
        timeResting += Time.deltaTime;

        // AUMENTAR ESTAMINA CADA SEGUNDO
        if(timeResting >= 1f)
        {
            // AUMENTAR ESTAMINA
            systemOwner.statusVariables.AddStamina(systemOwner.statusVariables.staminaRecoveredPerSecond);
            timeResting = 0f;

            // FINAL DE FSM (estamina recuperada)
            if (systemOwner.statusVariables.stamina >= systemOwner.statusVariables.MAX_STAMINA)
                CurrentSystemFinishedEvent?.Invoke();
        }
    }

    void DefinePath()
    {
        // definir camino hasta objetivo
        systemOwner.movementManager.DefinirCamino(systemOwner.initialBlock, persecucion: false, emergencia: true);
    }
}
