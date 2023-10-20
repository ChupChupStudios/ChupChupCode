using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 0.5f;
    Vector3 direccion = Vector3.zero;

    public Transform destino;
    public bool cambiarCamino = false;

    Stack<Nodo> camino;

    // Declarar un delegado de evento
    public delegate void CasillaMovidaEventHandler();
    public event CasillaMovidaEventHandler CasillaMovida;

    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    private void Update()
    {
        if (cambiarCamino)
        {
            cambiarCamino = false;
            DefinirCamino();
        }

        SeguirCamino();
        //Debug.Log(camino.Count);
    }

    void DefinirCamino()
    {
        camino = Pathfinding.Instance.HacerPathFinding(transform.position, destino.position);
    }

    void SeguirCamino()
    {
        // si no hay camino
        if (camino == null || camino.Count == 0) return;

        // SEGUIR CAMINO
        transform.position = transform.position + velocidad * Time.deltaTime * direccion;

        // AVANZAR NODO
        //Debug.Log($"{camino.Peek().posicionGlobal} y {transform.position}, distancia {Vector3.Distance(camino.Peek().posicionGlobal,transform.position)}");
        if (Vector3.Distance(camino.Peek().posicionGlobal, transform.position) < 0.1)
        {
            camino.Pop();

            // Emitir el evento cuando se mueve una casilla
            CasillaMovida?.Invoke();

            if (camino.Count == 0) return;

            direccion = camino.Peek().posicionGlobal - transform.position;
            direccion = new Vector3(direccion.x, 0f, direccion.z);
            transform.forward = direccion.normalized;
        }
    }
}
