using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentPetUS : BehaviourSystem
{
    // sistemas de toma de deciones:
    PetAtackModeFSM atackModeFSM;
    PetRestModeFSM restModeFSM;
    PetCollectModeFSM collectModeFSM;

    // variables
    public float MAX_ENERGY = 100f;
    float energy = 100f;
    float timeWithoutCombat = 0f;
    float timeWithoutSupport = 0f;


    public IndependentPetUS(GameObject systemOwner, PetAtackModeFSM atackModeFSM,
        PetRestModeFSM restModeFSM, PetCollectModeFSM collectModeFSM) : base(systemOwner)
    {
        this.atackModeFSM = atackModeFSM;
        this.restModeFSM = restModeFSM;
        this.collectModeFSM = collectModeFSM;
    }

    public override void OnStart()
    {
        // EVALUAR VARIABLES Y DETERMINAR SISTEMA DE DECISION DESTINO

    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }


    // FUNCIONES MATEMATICAS PARA FACTORES DE DECISION
    enum DecisionFactor
    {
        ProtectiveInstinct,
        DesireToRest,
        DesireToShowAffection,
        PlayerCardShortage
    }
    private float ComputeDecisionFactor()
    {
        return 0f;
    }
}
