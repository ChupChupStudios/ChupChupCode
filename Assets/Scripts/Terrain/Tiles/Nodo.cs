using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    // ATRIBUTOS GENERALES
    [SerializeField] LayerMask capaObstaculos;
    [SerializeField] LayerMask capaObstaculosEnemigo;
    public bool caminable = true;
    public bool objeto = false;
    public Vector3 posicionGlobal;

    // NODO PREVIO EN EL CAMINO
    [HideInInspector] public Nodo previo;

    // INDICES EN CUADRICULA
    [HideInInspector] public int posicionX;
    [HideInInspector] public int posicionY;

    // COSTES PARA PATHFINDING
    [HideInInspector] public int costeG;
    [HideInInspector] public int costeH;
    public int CosteF
    {
        get => costeG + costeH;
    }

    // -----------------------------------------------------------
    // -----------------------------------------------------------

    private void Awake()
    {
        posicionGlobal = transform.GetChild(0).transform.position;
        caminable = !Physics.Raycast(transform.position + Vector3.up * 3, Vector3.down, Mathf.Infinity, capaObstaculos);
        objeto = Physics.Raycast(transform.position + Vector3.up * 3, Vector3.down, Mathf.Infinity, capaObstaculosEnemigo);
    }

    public int GetDistancia(Nodo otro)
    {
        int distX = Mathf.Abs(posicionX - otro.posicionX);
        int distY = Mathf.Abs(posicionY - otro.posicionY);

        return Pathfinding.DISTANCIA_ENTRE_NODOS * (distX + distY);
    }
}
