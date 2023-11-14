using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRestModeFSM : BehaviourSystem
{
    public PetRestModeFSM(PetBehaviour systemOwner, BehaviourSystem parentSystem) : base(systemOwner, parentSystem)
    {
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO DESCANSO");
    }

    public override void OnUpdate()
    {
        //Utils.Log("\t\t\tUPDATE DE MODO DESCANSO");
    }
}
