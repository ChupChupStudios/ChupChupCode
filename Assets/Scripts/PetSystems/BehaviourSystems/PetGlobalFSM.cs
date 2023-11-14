using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGlobalFSM : BehaviourSystem
{
    // sistemas de toma de decisiones
    public enum GlobalStates
    {
        None,
        IndependentUS,
        CombatModeFSM,
        CollectModeFSM
    }
    public Dictionary<GlobalStates, BehaviourSystem> behaviourSystems = new();

    GlobalStates currentState = GlobalStates.IndependentUS;
    public GlobalStates CurrentState
    {
        get => currentState;
        set
        {
            if (value == GlobalStates.None) return;

            currentState = value;
            behaviourSystems[currentState].OnEnter();
        }
    }


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public PetGlobalFSM(PetBehaviour systemOwner) : base(systemOwner, null)
    {
        behaviourSystems = new Dictionary<GlobalStates, BehaviourSystem>
        {
            { GlobalStates.None, null },
            { GlobalStates.IndependentUS, new IndependentPetUS(systemOwner, this) },
            { GlobalStates.CombatModeFSM, new PetAttackModeFSM(systemOwner, this) },
            { GlobalStates.CollectModeFSM, new PetCollectModeFSM(systemOwner, this) }
        };
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" + 
            "ENTRANDO EN FSM GLOBAL");

        behaviourSystems[currentState].OnEnter();
    }

    public override void OnUpdate()
    {
        //Utils.Log("\t\t\tUPDATE DE FSM GLOBAL");

        behaviourSystems[currentState].OnUpdate();
    }
}
