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
        FinalAction maxFactor = FinalAction.None;
        float maxFactorValue = -1f;
        foreach (FinalAction factor in decisionFactors.Keys)
        {
            if (decisionFactors[factor] > maxFactorValue)
            {
                maxFactor = factor;
                maxFactorValue = decisionFactors[factor];
            }
            Utils.Log($"    {factor}: {decisionFactors[factor]}");
        }

        // cambio de accion
        currentAction = maxFactor;
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
    Dictionary<FinalAction, float> decisionFactors = new();
    private void ComputeDecisionFactors()
    {
        //--------------------------
        // FACTORES INTERMEDIOS

        // INSTINTO PROTECTOR
        // f(x) = e^(x-10)
        float protectiveInstinct = Mathf.Pow((float)Math.E, systemOwner.statusVariables.timeWithoutCombat - 10);
        protectiveInstinct = Mathf.Clamp01(protectiveInstinct);

        // GANAS DE MOSTRAR AFECTO
        // 1 / (1 + e^-(x-5))
        float desireToShowAffection = 1f;
        desireToShowAffection /= 1 + Mathf.Pow((float)Math.E, -1 * (systemOwner.statusVariables.timeWithoutSupport - 5));

        // ESCASEZ DE CARTAS DEL JUGADOR
        // -0.25 * x + 1
        float playerCardShortage = -0.25f * DeckManager.Instance.cards.Count + 1;
        playerCardShortage = Mathf.Clamp01(playerCardShortage);

        //--------------------------
        // FACTORES DE DECISION

        // GANAS DE DESCANSAR
        // 1 / (1 + e^(0.75 * (x-6)))
        float desireToRest = 1f;
        desireToRest /= 1 + Mathf.Pow((float)Math.E, 0.75f * (systemOwner.statusVariables.stamina/10 - 6));
        desireToRest = (float)Math.Round(desireToRest, 2);
        decisionFactors[FinalAction.RestMode] = desireToRest;

        // GANAS DE PATRULLAR
        float desireToPatrol =
            protectiveInstinct * 0.5f +
            (1 - desireToRest) * 0.5f;
        desireToPatrol = (float)Math.Round(desireToPatrol, 2);
        decisionFactors[FinalAction.CombatMode] = desireToPatrol;

        // GANAS DE RECOLECTAR
        float desireToCollect =
            (1 - desireToRest) * 0.4f +
            desireToShowAffection * 0.4f +
            playerCardShortage * 0.2f;
        desireToCollect = (float)Math.Round(desireToCollect, 2);
        decisionFactors[FinalAction.CollectMode] = desireToCollect;

        Utils.Log("=======");
        Utils.Log($"descansar: {desireToRest}");
        Utils.Log($"patrullar: {desireToPatrol};\t instinto {protectiveInstinct}");
        Utils.Log($"recoleeccion: {desireToCollect};\t affection {desireToShowAffection}, escasez {playerCardShortage}");
        Utils.Log("=======");
    }
}
