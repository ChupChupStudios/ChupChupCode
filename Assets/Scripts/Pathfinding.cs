using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    //-------------------------------------------------------------------------
    //  ATRIBUTOS
    //-------------------------------------------------------------------------

    // Singleton
    static Pathfinding instance;
    public static Pathfinding Instance
    {
        get => instance;
    }

    GestorCuadricula gestorCuadricula;
    public static int DISTANCIA_ENTRE_NODOS = 10;

    //-------------------------------------------------------------------------
    //  METODOS
    //-------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        gestorCuadricula = GetComponent<GestorCuadricula>();
    }



    public Stack<Nodo> HacerPathFinding(Vector3 posicionInicial, Vector3 posicionDestino)
    {
        Nodo nodoInicial = gestorCuadricula.NodoCoincidente(posicionInicial);
        Nodo nodoDestino = gestorCuadricula.NodoCoincidente(posicionDestino);

        if (nodoInicial == null || nodoDestino == null) return null;
        if (!nodoInicial.caminable || !nodoDestino.caminable) return null;

        List<Nodo> nodosAccesibles = new();
        HashSet<Nodo> nodosRevisados = new();
        nodosAccesibles.Add(nodoInicial);
        nodoInicial.previo = null;
        nodoInicial.costeG = 0;

        // INICIO --------------------------------------------

        Nodo nodoActual = null;
        while(nodosAccesibles.Count > 0)
        {
            // BUSCAR MENOR COSTE F --------------
            nodoActual = nodosAccesibles[0];
            for(int i = 1; i < nodosAccesibles.Count; i++)
            {
                if(nodosAccesibles[i].CosteF < nodoActual.CosteF || nodosAccesibles[i].CosteF == nodoActual.CosteF && nodosAccesibles[i].costeH < nodoActual.costeH)
                {
                    nodoActual = nodosAccesibles[i];
                }
            }

            // MARCAR NODO PROCESADO -------------
            nodosAccesibles.Remove(nodoActual);
            nodosRevisados.Add(nodoActual);

            // NODO DESTINO ENCONTRADO -----------
            if (nodoActual == nodoDestino)
                return TrazarCamino(nodoActual, nodoInicial);

            // REVISAR VECINOS -------------------
            foreach(Nodo vecino in gestorCuadricula.ListaDeVecinos(nodoActual))
            {
                if (vecino == null) continue;
                if (!vecino.caminable || nodosRevisados.Contains(vecino)) continue;

                int costeHastaVecino = nodoActual.costeG + Pathfinding.DISTANCIA_ENTRE_NODOS;
                if(costeHastaVecino < vecino.costeG || !nodosRevisados.Contains(vecino))
                {
                    vecino.costeG = costeHastaVecino;
                    vecino.costeH = vecino.GetDistancia(nodoDestino);
                    vecino.previo = nodoActual;

                    if (!nodosAccesibles.Contains(vecino)) nodosAccesibles.Add(vecino);
                }
            }
        }

        return null;
    }

    Stack<Nodo> TrazarCamino(Nodo nodoDestino, Nodo nodoInicio)
    {
        Stack<Nodo> camino = new();

        Nodo nodoActual = nodoDestino;
        while (nodoActual != null)
        {
            camino.Push(nodoActual);
            nodoActual = nodoActual.previo;
        }

        return camino;
    }
}
