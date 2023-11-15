using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourSystem
{
    protected PetBehaviour systemOwner;
    protected BehaviourSystem parentSystem;

    public BehaviourSystem(PetBehaviour systemOwner, BehaviourSystem parentSystem)
    {
        this.systemOwner = systemOwner;
        this.parentSystem = parentSystem;
    }
    public abstract void OnEnter();
    public abstract void OnUpdate();

    public Action CurrentSystemFinishedEvent;
}
