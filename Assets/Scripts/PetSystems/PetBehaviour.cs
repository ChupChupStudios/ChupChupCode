using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBehaviour : MonoBehaviour
{
    public ClosestEnemy closestEnemy;
    public Attack attack;
    PetGlobalFSM fsm;

    //----------------------------------------------------------------
    //  METODOS DE UNITY
    //----------------------------------------------------------------

    private void Start()
    {
        fsm = new(this);
        fsm.OnEnter();
    }

    private void Update()
    {
        fsm.OnUpdate();
    }
}
