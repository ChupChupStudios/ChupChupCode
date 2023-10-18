using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public bool caminable = true;
    public Vector3 posicionGlobal;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public int FCost
    {
        get => gCost + hCost;
    }

    private void Start()
    {
        posicionGlobal = transform.GetChild(0).transform.position;
    }
}
