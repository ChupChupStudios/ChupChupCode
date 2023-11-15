using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetVariablesManager : MonoBehaviour
{
    // variables
    public float MAX_STAMINA = 100f;
    public float stamina = 100f;
    public float timeWithoutCombat = 0f;
    public float timeWithoutSupport = 0f;

    public float movementCost = 5f;
    public float attackCost = 10f;

    public float timeBetweenAttacks = 2f;
    public float staminaRecoveredPerSecond = 1f;

    public event Action<float> StaminaReducedEvent;

    void Update()
    {
        timeWithoutCombat += Time.deltaTime;
        timeWithoutSupport += Time.deltaTime;
    }

    public void AddStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0f, MAX_STAMINA);
        StaminaReducedEvent.Invoke(stamina);
    }
}
