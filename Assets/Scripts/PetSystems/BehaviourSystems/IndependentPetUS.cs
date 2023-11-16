using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentPetUS : BehaviourSystem
{
    enum FinalAction
    {
        None,
        CombatMode,
        RestMode,
        CollectMode
    }
    FinalAction currentAction = FinalAction.None;
    Dictionary<FinalAction, BehaviourSystem> finalActions = new();


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public IndependentPetUS(PetBehaviour systemOwner, BehaviourSystem parentSystem) : base(systemOwner, parentSystem)
    {
        finalActions = new Dictionary<FinalAction, BehaviourSystem>
        {
            { FinalAction.None, null },
            { FinalAction.CombatMode, new PetAttackModeFSM(systemOwner, this) },
            { FinalAction.RestMode, new PetRestModeFSM(systemOwner, this) },
            { FinalAction.CollectMode, new PetCollectModeFSM(systemOwner, this) }
        };

        finalActions[FinalAction.CombatMode].CurrentSystemFinishedEvent += ChooseDecisionSystem;
        finalActions[FinalAction.CollectMode].CurrentSystemFinishedEvent += ChooseDecisionSystem;
        finalActions[FinalAction.RestMode].CurrentSystemFinishedEvent += ChooseDecisionSystem;
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" + 
            "ENTRANDO EN SISTEMA DE UTILIDAD");

        ChooseDecisionSystem();
    }

    public override void OnUpdate()
    {
        //Utils.Log("\t\t\tUPDATE DE SISTEMA DE UTILIDAD");
        finalActions[currentAction].OnUpdate();
    }


    private void ChooseDecisionSystem()
    {
        // EVALUAR VARIABLES Y DETERMINAR SISTEMA DE DECISION DESTINO
        ComputeDecisionFactors();

        // BUSCAR FACTOR MAYOR
        Factor maxFactor;
        float maxFactorValue = 0f;
        foreach (Factor factor in decisionFactors.Keys)
        {
            if (decisionFactors[factor] > maxFactorValue)
                maxFactor = factor;
            Utils.Log($"    {factor}: {decisionFactors[factor]}");
        }

        // DETERMINAR SISTEMA DE DECISION DESTINO
        FinalAction newAction;
        //  MODO DESCANSO
        if (decisionFactors[Factor.DesireToRest] >= decisionFactors[Factor.DesireToPatrol] &&
            decisionFactors[Factor.DesireToRest] >= decisionFactors[Factor.DesireToCollect])
        {
            newAction = FinalAction.RestMode;
        }
        //  MODO ATAQUE
        else if (decisionFactors[Factor.DesireToPatrol] >= decisionFactors[Factor.DesireToCollect])
        {
            newAction = FinalAction.CombatMode;
        }
        //  MODO RECOLECCION
        else
        {
            newAction = FinalAction.CollectMode;
        }

        // cambio de accion
        currentAction = newAction;
        finalActions[currentAction].OnEnter();
    }

    //--------------------------------------------------------------------
    // FUNCIONES MATEMATICAS PARA FACTORES DE DECISION
    enum Factor
    {
        DesireToRest,
        DesireToPatrol,
        DesireToCollect
    }
    Dictionary<Factor, float> decisionFactors = new();
    private void ComputeDecisionFactors()
    {
        // FACTORES INTERMEDIOS

        // f(x) = e^(x-10)
        float protectiveInstinct = Mathf.Pow((float)Math.E, systemOwner.statusVariables.timeWithoutCombat - 10);
        protectiveInstinct = Mathf.Clamp01(protectiveInstinct);

        // 1 / (1 + e^-(x-5))
        float desireToShowAffection = 1f;
        desireToShowAffection /= 1 + Mathf.Pow((float)Math.E, -1 * (systemOwner.statusVariables.timeWithoutSupport - 5));

        // -0.25 * x + 1
        float playerCardShortage = -0.25f * DeckManager.Instance.cards.Count + 1;
        playerCardShortage = Mathf.Clamp01(playerCardShortage);


        // GANAS DE DESCANSAR
        // 1 / (1 + e^(0.75 * (x-6)))
        float desireToRest = 1f;
        desireToRest /= 1 + Mathf.Pow((float)Math.E, 0.75f * (systemOwner.statusVariables.stamina/10 - 6));
        desireToRest = (float)Math.Round(desireToRest, 2);
        decisionFactors[Factor.DesireToRest] = desireToRest;

        // GANAS DE PATRULLAR
        float desireToPatrol =
            protectiveInstinct * 0.5f +
            (1 - desireToRest) * 0.5f;
        desireToPatrol = (float)Math.Round(desireToPatrol, 2);
        decisionFactors[Factor.DesireToPatrol] = desireToPatrol;

        // GANAS DE RECOLECTAR
        float desireToCollect =
            (1 - desireToRest) * 0.5f +
            desireToShowAffection * 0.2f +
            playerCardShortage * 0.3f;
        desireToCollect = (float)Math.Round(desireToCollect, 2);
        decisionFactors[Factor.DesireToCollect] = desireToCollect;
    }
}
