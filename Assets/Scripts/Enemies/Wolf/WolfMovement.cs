using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    // ATRIBUTOS DEL LOBO
    public float velocidad = 1f;
    public Vector3 direccion;
    public int descanso = 0; 

    // VARIABLES DE MOVIMIENTO

    Stack<Nodo> camino;

    Nodo nodoDestino;
    public Nodo nodoObjetivo;

    public float umbralLlegadaObjetivo = 0.1f;

    public GestorCasillaActual player;

    // VARIABLE DE CORUTINA

    private bool esperar = true;
    private bool descansando = false;
    Nodo nodoObjetivoAux;

    public Animator animator;


    //public EventHandler<float> CasillaMovida;

    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    private void Start()
    {
        SliderStamina.WolfSpeedEvent += CambioDeVelocidad;
        transform.LookAt(new Vector3(1f,0f,0f));

    }

    private void Update()
    {
        SeguirCamino();
    }


    public void StartCall()
    {
        nodoDestino = player.currentNode.GetComponent<Nodo>();
        DefinirCamino(nodoDestino);
    }

    public void DefinirCamino(Nodo destino)
    {

        camino = Pathfinding.Instance.HacerPathFindingEnemigo(transform.position, destino.posicionGlobal);

        //HACER QUE NO SE MUEVA SI EL JUGADOR YA ESTA DELANTE;

        if (nodoObjetivo == null && camino != null)
        {
            nodoObjetivo = camino.Pop();
        }

        Utils.Log($"camino {camino == null}, nodoObjetivo {nodoObjetivo == null}");
        direccion = camino.Peek().posicionGlobal -nodoObjetivo.posicionGlobal;

        animator.SetBool("Moviendo", true);

    }

    void SeguirCamino()
    {
        if (nodoObjetivo == null)
        {
            animator.SetBool("Moviendo", false);
            return;
        }

        // SEGUIR CAMINO

        Vector3 posicionSiguiente = new(nodoObjetivo.posicionGlobal.x, 0.45f, nodoObjetivo.posicionGlobal.z);
        Vector3 posicionLobo = transform.position;
        posicionLobo = new(posicionLobo.x, 0.45f, posicionLobo.z);

        Vector3 posicion = nodoObjetivo.transform.GetChild(0).position;
        transform.position = Vector3.MoveTowards(transform.position, posicionSiguiente, velocidad * Time.deltaTime);

        // AVANZAR NODO
        if (Vector3.Distance(posicionSiguiente, posicionLobo) < 0.01)
        {
            descanso++;

            // Actualizar siguiente nodo -------
            if (camino.Count == 0)
            {
                direccion = Vector3.zero;
                animator.SetBool("Moviendo", false);
                return;
            }

            if(descanso >= 4)
            {
                nodoObjetivo = null;
                camino = null;
                animator.SetBool("Moviendo", false);
                StartCoroutine(EsperarYMover());
                return;
            }


            // NUEVA DIRECCION
            direccion = camino.Peek().posicionGlobal - nodoObjetivo.posicionGlobal;
            nodoObjetivo = camino.Pop();
            transform.forward = direccion.normalized;
        }
    }

    IEnumerator EsperarYMover()
    {
        direccion = Vector3.zero;
        float tiempoEspera = Random.Range(3f, 8f);
        yield return new WaitForSeconds(tiempoEspera);
        if (player.gameObject.GetComponent<PlayerStateManager>().CurrentState == PlayerStateManager.State.Idle)
            {
              StartCoroutine(EsperarYMover());
            }
        descanso = 0; 
        nodoDestino = player.currentNode.GetComponent<Nodo>();
        DefinirCamino(nodoDestino);
        esperar = false;
    }

    public void CambioDeVelocidad(float f)
    {
        velocidad = f;
    }

    public void Corutina()
    {
        nodoObjetivo = null;
        camino = null;
        StartCoroutine(EsperarYMover());
    }

}










