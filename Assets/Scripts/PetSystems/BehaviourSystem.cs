using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourSystem
{
    protected GameObject systemOwner;

    public BehaviourSystem(GameObject systemOwner)
    {
        this.systemOwner = systemOwner;
    }
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
