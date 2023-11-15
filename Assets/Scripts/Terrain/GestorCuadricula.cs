using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCuadricula : MonoBehaviour
{
    public static GestorCuadricula Instance;

    public LayerMask mascaraCaminable;
    public Vector2 dimensionesCuadricula;
    public float margenDePintado = 0f;

    Nodo[,] cuadricula;
    float radioNodo = 0.5f;

    float diametroNodo;
    int nodosEnX, nodosEnY;
    float porcentajeCasillaX, porcentajeCasillaY;


    //--------------------------------------------------------------------
    // METODOS DE UNITY
    //--------------------------------------------------------------------

    private void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        CrearCuadricula();
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
        porcentajeCasillaX = 1f / nodosEnX;
        porcentajeCasillaY = 1f / nodosEnY;
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

    public List<Nodo> ListaDeVecinos(Nodo nodo)
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
        float porcentajeX = (posicion.x) / dimensionesCuadricula.x;
        float porcentajeY = (posicion.z) / dimensionesCuadricula.y;
        porcentajeX = Mathf.Clamp01(porcentajeX);
        porcentajeY = Mathf.Clamp01(porcentajeY);

        // x * porcCasillaX > porcentajeX
        // x < porcentajeX / porcCasillaX
        int x = Mathf.FloorToInt(porcentajeX / porcentajeCasillaX);
        int y = Mathf.FloorToInt(porcentajeY / porcentajeCasillaY);

        return cuadricula[x,y];
    }


    //--------------------------------------------------------------------
    // METODOS PARA VISUALIZACION
    //--------------------------------------------------------------------

    ///*
    public Nodo jugador;
    public Nodo destino;
    public Stack<Nodo> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(dimensionesCuadricula.x, 2, dimensionesCuadricula.y));

        if(cuadricula != null)
        {
            foreach(Nodo n in cuadricula)
            {
                if (n == null) continue;
                Gizmos.color = (n.caminable) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Debug.Log("camino");
                        Gizmos.color = Color.yellow;
                    }
                }
                if (n == jugador || n == destino)
                    Gizmos.color = (n == jugador) ? Color.cyan : Color.green;
                Gizmos.DrawCube(n.posicionGlobal, Vector3.one * (radioNodo * 2 - margenDePintado));
            }
        }
    }
    //*/
}
