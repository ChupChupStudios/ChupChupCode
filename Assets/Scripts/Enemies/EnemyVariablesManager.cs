using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariablesManager : MonoBehaviour
{
    public int lifePoints;
    GameObject currentNode;
    GameObject previousNode;
    public LayerMask Ground;

    public EventHandler<int> Golpeado;

    // Start is called before the first frame update
    void Start()
    {
        //lifePoints = 2;
        if (Utils.CustomRaycast(transform.position + Vector3.up, Vector3.down, out currentNode, Ground))
        {
            currentNode.GetComponent<Nodo>().caminable = false;
            previousNode = currentNode;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.CustomRaycast(transform.position + Vector3.up, Vector3.down, out currentNode, Ground))
        {
            if (currentNode != previousNode)
            {
                previousNode.GetComponent<Nodo>().caminable = true;
                currentNode.GetComponent<Nodo>().caminable = false;
                previousNode = currentNode;

            }
        }
    }

    public void GetDamage()
    {
        lifePoints--;
        if (lifePoints <= 0) gameObject.SetActive(false);
        Golpeado?.Invoke(this, lifePoints);
    }
}
