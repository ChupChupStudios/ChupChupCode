using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetMovement : MonoBehaviour
{
    public float velocidad = 0.5f;
    Vector3 direccion = Vector3.zero;

    PetVariablesManager petVariables;

    public Transform ojetivoFinalTransform;
    Nodo ultimoNodoDeObjetivo;
    Stack<Nodo> camino = new();
    Nodo nodoObjetivo;
    public float umbralLlegadaObjetivo = 0.05f;

    bool persecucion = false;


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    private void Start()
    {
        petVariables = GetComponent<PetVariablesManager>();
    }

    public void DefinirCamino(Transform objetivo, bool persecucion)
    {
        ojetivoFinalTransform = objetivo;

        ultimoNodoDeObjetivo = GestorCuadricula.Instance.NodoCoincidente(objetivo.position);
        if (persecucion)
            camino = Pathfinding.Instance.HacerPathFinding(transform.position, ultimoNodoDeObjetivo.posicionGlobal);
        else
            camino = Pathfinding.Instance.HacerPathFinding(transform.position, ultimoNodoDeObjetivo.posicionGlobal);

        this.persecucion = persecucion;

        Utils.Log($"{objetivo.gameObject.name}, {camino == null}");

        if (nodoObjetivo == null && camino != null) nodoObjetivo = camino.Pop();
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
            // REDUCIR ESTAMINA
            petVariables.ChangeStamina(-petVariables.movementCost);

            // Actualizar siguiente nodo -------

            // COMPROBAR CAMBIO EN EL OBJETIVO AL PERSEGUIR
            if (persecucion)
            {
                Nodo nodoObjetivoFinal = GestorCuadricula.Instance.NodoCoincidente(ojetivoFinalTransform.position);
                // el objetivo se ha movido
                if (ultimoNodoDeObjetivo != nodoObjetivoFinal)
                {
                    nodoObjetivo = null;
                    DefinirCamino(ojetivoFinalTransform, persecucion);
                    return false;
                }
                // se ha llegado a casilla adyacente a objetivo
                else if (camino?.Peek() == nodoObjetivoFinal)
                {
                    direccion = camino.Peek().posicionGlobal - nodoObjetivo.posicionGlobal;
                    transform.forward = direccion.normalized;
                    camino.Clear();
                    nodoObjetivo = null;
                    return true;
                }
            }

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
