using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRestModeFSM : BehaviourSystem
{
    public PetRestModeFSM(PetBehaviour systemOwner) : base(systemOwner)
    {
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO DESCANSO");
    }

    public override void OnExit()
    {
        Utils.Log("SALIENDO DE MODO DESCANSO\n" +
            ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
    }

    public override void OnUpdate()
    {
        Utils.Log("\t\t\tUPDATE DE MODO DESCANSO");
    }
}
