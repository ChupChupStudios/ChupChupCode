using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Cuadricula grid;

    private void Awake()
    {
        grid = GetComponent<Cuadricula>();
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Nodo startNode = grid.NodoCoincidente(startPos);
        Nodo targetNode = grid.NodoCoincidente(targetPos);

        List<Nodo> openSet = new List<Nodo>();
        HashSet<Nodo> closedSet = new HashSet<Nodo>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Nodo currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode) return;
        }
    }
}
