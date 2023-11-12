using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetMovement : MonoBehaviour
{
    public float velocidad = 0.5f;
    Vector3 direccion = Vector3.zero;

    Stack<Nodo> camino = new();
    Nodo nodoObjetivo;
    public float umbralLlegadaObjetivo = 0.1f;


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public void DefinirCamino(Nodo destino)
    {
        camino = Pathfinding.Instance.HacerPathFinding(transform.position, destino.posicionGlobal);
        if(nodoObjetivo == null && camino != null) nodoObjetivo = camino.Pop();
    }

    // devuelve true si ha llegado al nodo destino
    public bool SeguirCamino()
    {
        if (nodoObjetivo == null) return false;

        // SEGUIR CAMINO
        Vector3 posicion = nodoObjetivo.transform.GetChild(0).transform.position;
        posicion = new Vector3(posicion.x, transform.position.y, posicion.z);
        transform.position = Vector3.MoveTowards(transform.position, posicion, velocidad * Time.deltaTime);

        // AVANZAR NODO
        if (Vector3.Distance(posicion, transform.position) < umbralLlegadaObjetivo)
        {
            // Actualizar siguiente nodo -------
            
            // FINAL DE CAMINO
            if (camino.Count == 0)
            {
                nodoObjetivo = null;

                return true;
            }

            // NUEVA DIRECCION
            direccion = camino.Peek().posicionGlobal - nodoObjetivo.posicionGlobal;
            nodoObjetivo = camino.Pop();
            transform.forward = direccion.normalized;
        }

        return false;
    }
}
