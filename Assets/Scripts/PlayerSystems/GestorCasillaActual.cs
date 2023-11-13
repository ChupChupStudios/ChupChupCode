using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCasillaActual : MonoBehaviour
{
    public GameObject currentNode;
    GameObject previousNode;
    public LayerMask Ground;
    public WolfMovement wolfMovement;
    // Start is called before the first frame update
    void Start()
    {
        if(Utils.CustomRaycast(transform.position + Vector3.up,Vector3.down,out currentNode,Ground))
        {
            currentNode.GetComponent<Nodo>().caminable = false;
            previousNode = currentNode;
            if(wolfMovement!=null) wolfMovement.StartCall();
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.CustomRaycast(transform.position + Vector3.up, Vector3.down, out currentNode, Ground))
        {
            if(currentNode != previousNode)
            {
                previousNode.GetComponent<Nodo>().caminable = true;
                currentNode.GetComponent<Nodo>().caminable = false;
                previousNode = currentNode;

            }
        }
    }
}
