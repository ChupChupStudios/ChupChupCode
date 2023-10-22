using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 0.5f;
    Vector3 direccion = Vector3.zero;

    Stack<Nodo> camino;
    Nodo nodoObjetivo;


    public EventHandler<float> CasillaMovida;

    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    private void Update()
    {
        SeguirCamino();
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
            CasillaMovida?.Invoke(this, 5.0f);
            
            // Actualizar siguiente nodo -------
            
            // FINAL DE CAMINO
            if (camino.Count == 0)
            {
                Goal goal = nodoObjetivo.gameObject.GetComponent<Goal>();
                if (goal!=null)
                {
                    Debug.Log("Entra if");
                    SceneManager.LoadScene("FinalScene");
                }
                nodoObjetivo = null;
                return;
            }

            // El nodo del camino es el mismo que en el que esta el personaje
            if (camino.Peek() == nodoObjetivo)
                camino.Pop();

            // NUEVA DIRECCION
            direccion = camino.Peek().posicionGlobal - nodoObjetivo.posicionGlobal;
            nodoObjetivo = camino.Pop();
            transform.forward = direccion.normalized;
        }
    }
}
