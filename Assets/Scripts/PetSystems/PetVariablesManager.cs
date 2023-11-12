using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetVariablesManager : MonoBehaviour
{
    // variables
    public float MAX_ENERGY = 100f;
    public float energy = 100f;
    public float timeWithoutCombat = 0f;
    public float timeWithoutSupport = 0f;

    void Update()
    {
        timeWithoutCombat += Time.deltaTime;
        timeWithoutSupport += Time.deltaTime;
    }
}
