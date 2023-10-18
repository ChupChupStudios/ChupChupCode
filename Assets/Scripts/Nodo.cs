using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    // ATRIBUTOS GENERALES
    public bool caminable = true;
    public Vector3 posicionGlobal;
    [SerializeField] LayerMask capaObstaculos;

    // NODO PREVIO EN EL CAMINO
    public Nodo previo;

    // INDICES EN CUADRICULA
    public int posicionX;
    public int posicionY;

    // COSTES PARA PATHFINDING
    public int costeG;
    public int costeH;
    public int CosteF
    {
        get => costeG + costeH;
    }

    // -----------------------------------------------------------

    private void Start()
    {
        posicionGlobal = transform.GetChild(0).transform.position;
        caminable = !Physics.Raycast(transform.position + Vector3.up * 3, Vector3.down, Mathf.Infinity, capaObstaculos);
    }

    public int GetDistancia(Nodo otro)
    {
        int distX = Mathf.Abs(posicionX - otro.posicionX);
        int distY = Mathf.Abs(posicionY - otro.posicionY);

        return Pathfinding.DISTANCIA_ENTRE_NODOS * (distX + distY);
    }
}
