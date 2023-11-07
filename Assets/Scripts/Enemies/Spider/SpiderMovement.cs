using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    private GestorCuadricula gestorCuadricula;
    private Stack<Nodo> camino;

    public float velocidad = 1.25f;
    private Vector3 direccion = Vector3.zero;

    private bool esperar = true;

    void Start()
    {
        gestorCuadricula = FindObjectOfType<GestorCuadricula>();
        StartCoroutine(EsperarYMover());
    }

    void Update()
    {
        SeguirCamino();
    }

    void SeguirCamino()
    {
        if (camino != null && camino.Count > 0)
        {
            transform.position += velocidad * Time.deltaTime * direccion;

            Vector3 posicionSiguiente = camino.Peek().posicionGlobal;
            posicionSiguiente = new(posicionSiguiente.x, 0f, posicionSiguiente.z);
            Vector3 posicionArana = transform.position;
            posicionArana = new(posicionArana.x, 0f, posicionArana.z);

            if (Vector3.Distance(posicionSiguiente, posicionArana) < 0.01)
            {
                camino.Pop();

                if (camino.Count == 0)
                {
                    esperar = true;
                    return;
                }
                    
                direccion = camino.Peek().posicionGlobal - transform.position;
                direccion = new Vector3(direccion.x, 0f, direccion.z).normalized;
                transform.forward = direccion;
            }
        }
    }

    IEnumerator EsperarYMover()
    {
        while (true)
        {
            yield return new WaitUntil(()=>esperar);
            float tiempoEspera = Random.Range(3f, 10f);
            yield return new WaitForSeconds(tiempoEspera);
            esperar = false;
            MoverAUnaCasillaAleatoria();
        }
    }

    void MoverAUnaCasillaAleatoria()
    {
        Nodo nodoActual = gestorCuadricula.NodoCoincidente(transform.position);
        List<Nodo> vecinos = gestorCuadricula.ListaDeVecinos(nodoActual);
        List<Nodo> casillasDisponibles = new List<Nodo>();

        foreach (Nodo vecino in vecinos)
        {
            if (vecino != null && vecino.caminable)
            {
                casillasDisponibles.Add(vecino);
                //Debug.Log("Vecino:" + vecino.posicionGlobal);
            }
        }
        //Debug.Log("CasillasDisponibles.count:" + casillasDisponibles.Count);


        if (casillasDisponibles.Count > 0)
        {
            Nodo casillaAleatoria = casillasDisponibles[Random.Range(0, casillasDisponibles.Count)];

            camino = new();
            camino.Push(casillaAleatoria);
            camino.Push(nodoActual);
        }
    }
}
