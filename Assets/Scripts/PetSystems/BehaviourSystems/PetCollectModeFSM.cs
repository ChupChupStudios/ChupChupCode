using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCollectModeFSM : BehaviourSystem
{
    public PetCollectModeFSM(PetBehaviour systemOwner) : base(systemOwner)
    {
    }

    public override void OnEnter()
    {
        Utils.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n" +
            "ENTRANDO EN MODO RECOLECCION");
    }

    public override void OnExit()
    {
        Utils.Log("SALIENDO DE MODO RECOLECCION\n" +
            ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
    }

    public override void OnUpdate()
    {
        Utils.Log("\t\t\tUPDATE DE MODO RECOLECCION");
    }
}
