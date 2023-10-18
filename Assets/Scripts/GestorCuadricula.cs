using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCuadricula : MonoBehaviour
{
    public LayerMask mascaraCaminable;
    public Vector2 dimensionesCuadricula;
    public float radioNodoNuevo = 1f;
    public float margen = 0f;

    Nodo[,] cuadricula;
    float radioNodo = 1f;

    float diametroNodo;
    int nodosEnX, nodosEnY;


    //--------------------------------------------------------------------
    // METODOS DE UNITY
    //--------------------------------------------------------------------

    private void Start()
    {
        radioNodo = radioNodoNuevo;
        CrearCuadricula();
    }

    private void Update()
    {
        if (radioNodoNuevo != radioNodo)
        {
            radioNodo = radioNodoNuevo;
            CrearCuadricula();
        }
    }


    //--------------------------------------------------------------------
    // METODOS
    //--------------------------------------------------------------------

    void CrearCuadricula()
    {
        diametroNodo = radioNodo * 2;

        // calcular filas y columnas
        nodosEnX = Mathf.RoundToInt(dimensionesCuadricula.x / diametroNodo);
        nodosEnY = Mathf.RoundToInt(dimensionesCuadricula.y / diametroNodo);
        // crear cuadricula
        cuadricula = new Nodo[nodosEnX, nodosEnY];

        //-------------------------------------
        // crear los nodos de la cuadricula yendo de arriba hacia abajo y de izquierda a derecha
        Vector3 posicionAbajoIzq = transform.position - Vector3.right * dimensionesCuadricula.x/2 - Vector3.forward * dimensionesCuadricula.y/2;

        for(int x = 0; x < nodosEnX; x++)
        {
            for(int y= 0; y< nodosEnY; y++)
            {
                Vector3 posicionNodo = posicionAbajoIzq + Vector3.right * (x * diametroNodo + radioNodo) + Vector3.forward * (y * diametroNodo + radioNodo);

                // detectar si hay casilla caminable
                Nodo nodo = null;
                if(Physics.Raycast(posicionNodo + Vector3.up * 3, Vector3.down, out RaycastHit colision, Mathf.Infinity, mascaraCaminable))
                {
                    nodo = colision.collider.GetComponent<Nodo>();

                    // indicar posicion del nodo
                    nodo.posicionX = x;
                    nodo.posicionY = y;
                }

                // guardar casilla
                cuadricula[x, y] = nodo;
            }
        }
    }

    public List<Nodo> GetNeighbours(Nodo nodo)
    {
        List<Nodo> neighbours = new List<Nodo>();

        if (nodo.posicionY + 1 < dimensionesCuadricula.y)
            neighbours.Add(cuadricula[nodo.posicionX, nodo.posicionY + 1]);
        if (nodo.posicionX - 1 >= 0)
            neighbours.Add(cuadricula[nodo.posicionX - 1, nodo.posicionY]);
        if (nodo.posicionX + 1 < dimensionesCuadricula.x)
            neighbours.Add(cuadricula[nodo.posicionX + 1, nodo.posicionY]);
        if (nodo.posicionY - 1 >= 0)
            neighbours.Add(cuadricula[nodo.posicionX, nodo.posicionY - 1]);

        return neighbours;
    }

    public Nodo NodoCoincidente(Vector3 posicion)
    {
        float porcentajeX = (posicion.x + dimensionesCuadricula.x / 2) / dimensionesCuadricula.x;
        float porcentajeY = (posicion.z + dimensionesCuadricula.y / 2) / dimensionesCuadricula.y;
        porcentajeX = Mathf.Clamp01(porcentajeX);
        porcentajeY = Mathf.Clamp01(porcentajeY);

        int x = Mathf.RoundToInt((nodosEnX - 1) * porcentajeX);
        int y = Mathf.RoundToInt((nodosEnY - 1) * porcentajeY);

        return cuadricula[x,y];
    }


    //--------------------------------------------------------------------
    // METODOS PARA VISUALIZACION
    //--------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(dimensionesCuadricula.x, 2, dimensionesCuadricula.y));

        if(cuadricula != null)
        {
            foreach(Nodo n in cuadricula)
            {
                if (n == null) continue;
                Gizmos.color = (n.caminable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.posicionGlobal, Vector3.one * (radioNodo * 2 - margen));
            }
        }
    }
}
