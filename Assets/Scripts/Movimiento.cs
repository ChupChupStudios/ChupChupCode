using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 0.5f;
    Vector3 direccion = Vector3.zero;

    Stack<Nodo> camino;
    Nodo nodoObjetivo;


    // Declarar un delegado de evento
    public delegate void CasillaMovidaEventHandler();
    public event CasillaMovidaEventHandler CasillaMovida;

    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    private void Update()
    {
        SeguirCamino();
        //Debug.Log(camino.Count);
    }

    public void DefinirCamino(Nodo destino)
    {
        camino = Pathfinding.Instance.HacerPathFinding(transform.position, destino.posicionGlobal);
        if(nodoObjetivo == null && camino != null) nodoObjetivo = camino.Pop();
    }

    void SeguirCamino()
    {
        if (nodoObjetivo == null) return;

        // SEGUIR CAMINO
        transform.position = transform.position + velocidad * Time.deltaTime * direccion;

        // AVANZAR NODO
        if (Vector3.Distance(nodoObjetivo.posicionGlobal, transform.position) < 0.1)
        {
            // Emitir el evento cuando se mueve una casilla
            CasillaMovida?.Invoke();
            
            // Actualizar siguiente nodo -------
            
            if (camino.Count == 0)
            {
                nodoObjetivo = null;
                return;
            }

            if (camino.Peek() == nodoObjetivo)
                camino.Pop();

            if (camino.Count == 0) return;

            direccion = camino.Peek().posicionGlobal - nodoObjetivo.posicionGlobal;
            nodoObjetivo = camino.Pop();
            transform.forward = direccion.normalized;
        }
    }
}
