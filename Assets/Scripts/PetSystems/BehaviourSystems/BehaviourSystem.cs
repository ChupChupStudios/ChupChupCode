using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourSystem
{
    protected PetBehaviour systemOwner;

    public BehaviourSystem(PetBehaviour systemOwner)
    {
        this.systemOwner = systemOwner;
    }
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
