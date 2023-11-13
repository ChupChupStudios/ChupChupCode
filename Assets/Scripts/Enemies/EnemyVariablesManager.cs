using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariablesManager : MonoBehaviour
{
    public int lifePoints;

    public EventHandler<int> Golpeado;

    // Start is called before the first frame update
    void Start()
    {
        lifePoints = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage()
    {
        lifePoints--;
        if (lifePoints <= 0) gameObject.SetActive(false);
        Golpeado?.Invoke(this, lifePoints);
    }
}
