using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GestorCuadricula gestorCuadricula;

    public static int DISTANCIA_ENTRE_NODOS = 10;

    private void Awake()
    {
        gestorCuadricula = GetComponent<GestorCuadricula>();
    }

    Stack<Nodo> HacerPathFinding(Vector3 posicionInicial, Vector3 posicionDestino)
    {
        Nodo nodoInicial = gestorCuadricula.NodoCoincidente(posicionInicial);
        Nodo nodoDestino = gestorCuadricula.NodoCoincidente(posicionDestino);

        List<Nodo> nodosAccesibles = new List<Nodo>();
        HashSet<Nodo> nodosRevisados = new HashSet<Nodo>();
        nodosAccesibles.Add(nodoInicial);
        nodoInicial.previo = null;

        // INICIO --------------------------------------------

        while(nodosAccesibles.Count > 0)
        {
            // BUSCAR MENOR COSTE F --------------
            Nodo nodoActual = nodosAccesibles[0];
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
                return TrazarCamino(nodoActual);

            // REVISAR VECINOS -------------------
            foreach(Nodo vecino in gestorCuadricula.GetNeighbours(nodoActual))
            {
                if (!vecino.caminable || nodosRevisados.Contains(vecino)) continue;

                int costeHastaVecino = nodoActual.costeG + Pathfinding.DISTANCIA_ENTRE_NODOS;
                if(costeHastaVecino < vecino.costeG || !nodosRevisados.Contains(vecino))
                {
                    vecino.costeG = costeHastaVecino;
                    vecino.costeH = vecino.GetDistancia(nodoDestino);
                    vecino.previo = nodoActual;

                    if (!nodosRevisados.Contains(vecino)) nodosRevisados.Add(vecino);
                }
            }
        }

        return null;
    }

    Stack<Nodo> TrazarCamino(Nodo nodoDestino)
    {
        Stack<Nodo> camino = new();

        Nodo nodoActual = nodoDestino;
        while (nodoActual.previo != null)
        {
            camino.Push(nodoActual);
            nodoActual = nodoActual.previo;
        }

        return camino;
    }
}
