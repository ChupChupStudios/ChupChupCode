using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetBehaviour : MonoBehaviour
{
    public PetGlobalFSM globalSystem;
    public Attack attack;
    public Button petButton;
    public EnemyDetection enemyDetection;
    public Transform initialBlock;
    [HideInInspector] public PetMovement movementManager;
    [HideInInspector] public PetVariablesManager statusVariables;
    [HideInInspector] public GameObject player;


    //----------------------------------------------------------------
    //  METODOS DE UNITY
    //----------------------------------------------------------------

    private void Start()
    {
        movementManager = GetComponent<PetMovement>();
        statusVariables = GetComponent<PetVariablesManager>();
        player = GameObject.FindWithTag("Player");

        globalSystem = new(this);
        globalSystem.OnEnter();
    }

    private void Update()
    {
        globalSystem.OnUpdate();
    }
}
