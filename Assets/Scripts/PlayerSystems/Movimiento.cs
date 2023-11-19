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
    public float umbralLlegadaObjetivo = 0.1f;

    public ParticleSystem particulasAndar;

    public EventHandler<float> CasillaMovida;

    int actualScene;

    [SerializeField] private AudioClip grassSteps;
    [SerializeField] private AudioClip caveSteps;

    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    private void Start()
    {
        PlayerStateManager.Instance.StateChangeRequestedEvent += PeticionCambioDeEstado;

        // Detener las partículas al inicio
        if (particulasAndar != null) particulasAndar.Stop();
        actualScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        SeguirCamino();
    }

    public void DefinirCamino(Nodo destino)
    {
        //camino = Pathfinding.Instance.HacerPathFinding(transform.position, destino.posicionGlobal);
        Vector3 origen = (nodoObjetivo == null) ? transform.position : nodoObjetivo.posicionGlobal;
        camino = Pathfinding.Instance.HacerPathFinding(origen, destino.posicionGlobal);
        if (nodoObjetivo == null && camino != null) nodoObjetivo = camino.Pop();

        // NOTIFICAR CAMBIO DE ESTADO (a moviendose)
        PlayerStateManager.Instance.CurrentState = PlayerStateManager.State.Movement;
    }

    void SeguirCamino()
    {
        if (nodoObjetivo == null) return;

        //Debug.Log("Direccion " + direccion);
        // SEGUIR CAMINO
        //transform.position = transform.position + velocidad * Time.deltaTime * direccion;
        Vector3 posicion = nodoObjetivo.transform.GetChild(0).position;
        transform.position = Vector3.MoveTowards(transform.position, posicion, velocidad * Time.deltaTime);

        // Activar partículas andar
        if (particulasAndar != null && !particulasAndar.isPlaying) particulasAndar.Play();

        if (!SFXManager.Instance.soundsAudioSource.isPlaying)
        {
            if (actualScene == 4 || actualScene == 5 || actualScene == 6)
            {
                SFXManager.Instance.EjecutarSonido(caveSteps);
            }
            else
            {
                SFXManager.Instance.EjecutarSonido(grassSteps);
            }


        }

        // AVANZAR NODO
        if (Vector3.Distance(nodoObjetivo.posicionGlobal, transform.position) < umbralLlegadaObjetivo)
        {
            // Emitir el evento cuando se mueve una casilla
            CasillaMovida?.Invoke(this, 5.0f);

            // Actualizar siguiente nodo -------

            // FINAL DE CAMINO
            if (camino.Count == 0)
            {
                // COMPROBAR SI ESTA EN CASILLA CON CARTEL
                Sign sign= nodoObjetivo.gameObject.GetComponent<Sign>();
                if (sign != null)
                {
                    sign.SignText();
                    Debug.Log("Cartel funciona");
                }

                // COMPROBAR SI ESTA EN CASILLA OBJETIVO
                Goal goal = nodoObjetivo.gameObject.GetComponent<Goal>();
                if (goal != null)
                {
                    int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(indiceEscenaActual + 1);
                }
                nodoObjetivo = null;

                // NOTIFICAR CAMBIO DE ESTADO (a idle)
                PlayerStateManager.Instance.CurrentState = PlayerStateManager.State.Idle;

                // Desactivar partículas andar
                if (particulasAndar != null) particulasAndar.Stop();
                if (SFXManager.Instance.soundsAudioSource.isPlaying) SFXManager.Instance.soundsAudioSource.Stop();
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

    void PeticionCambioDeEstado(PlayerStateManager.State newState)
    {
        if (newState != PlayerStateManager.State.Movement)
            camino.Clear();
    }

    public void CambiarVelocidad(int i, float factorCambioVelocidad)
    {
        if (i == 1)
        {
            velocidad *= factorCambioVelocidad;
        }
        else if (i == 2)
        {
            velocidad /= factorCambioVelocidad;
        }
    }

    public void CambiarDireccionAbajoIzquierda()
    {
        if (gameObject.GetComponent<PlayerStateManager>().CurrentState == PlayerStateManager.State.Movement) return;
        if (DeckManager.Instance.SelectedCard != null)
        {
            ACard cardAux = DeckManager.Instance.SelectedCard;
            DeckManager.Instance.SelectedCard.CardDeselected();
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            //DeckManager.Instance.SelectedCard = cardAux;
            cardAux.CardSelected();

        }
        else
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);

    }

    public void CambiarDireccionAbajoDerecha()
    {
        if (gameObject.GetComponent<PlayerStateManager>().CurrentState == PlayerStateManager.State.Movement) return;
        if (DeckManager.Instance.SelectedCard != null)
        {
            ACard cardAux = DeckManager.Instance.SelectedCard;
            DeckManager.Instance.SelectedCard.CardDeselected();
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            //DeckManager.Instance.SelectedCard = cardAux;
            cardAux.CardSelected();

        }
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public void CambiarDireccionArribaIzquierda()
    {
        if (gameObject.GetComponent<PlayerStateManager>().CurrentState == PlayerStateManager.State.Movement) return;
        if (DeckManager.Instance.SelectedCard != null)
        {
            ACard cardAux = DeckManager.Instance.SelectedCard;
            DeckManager.Instance.SelectedCard.CardDeselected();
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //DeckManager.Instance.SelectedCard = cardAux;
            cardAux.CardSelected();

        }
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void CambiarDireccionArribaDerecha()
    {
        Debug.Log("ENTRA BOTON");
        if (gameObject.GetComponent<PlayerStateManager>().CurrentState == PlayerStateManager.State.Movement) return;
        if (DeckManager.Instance.SelectedCard != null)
        {
            ACard cardAux = DeckManager.Instance.SelectedCard;
            DeckManager.Instance.SelectedCard.CardDeselected();
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            //DeckManager.Instance.SelectedCard = cardAux;
            cardAux.CardSelected();

        }
        else
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }
}
