using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatMovement : MonoBehaviour
{
    private EnemyVariablesManager enemyVariableManager;
    private GestorCuadricula gestorCuadricula;
    private Stack<Nodo> camino;
    public LayerMask casillaLayer;

    public bool spawner;
    public GameObject prefabBat;

    public float velocidad = 3.0f;
    private Vector3 direccion = Vector3.forward;
    private bool casillaAlcanzada = true;

    public Animator animator;

    void Start()
    {
        enemyVariableManager = this.gameObject.GetComponent<EnemyVariablesManager>();
        gestorCuadricula = FindObjectOfType<GestorCuadricula>();
        StartCoroutine(EsperarYMover());

        if (enemyVariableManager != null)
        {
            enemyVariableManager.Golpeado += InvocarEnemigos;
        }
    }

    void Update()
    {
        SeguirCamino();
    }

    void SeguirCamino()
    {
        if (camino == null || camino.Count == 0)
        {
            animator.SetBool("Moviendo", false);
            return;
        }

        animator.SetBool("Moviendo", true);

        //transform.position += velocidad * Time.deltaTime * direccion;
        transform.position = Vector3.MoveTowards(transform.position, camino.Peek().posicionGlobal, velocidad * Time.deltaTime);

        Vector3 posicionSiguiente = camino.Peek().posicionGlobal;
        posicionSiguiente = new Vector3(posicionSiguiente.x, 0f, posicionSiguiente.z);
        Vector3 posicioMurcielago = transform.position;
        posicioMurcielago = new Vector3(posicioMurcielago.x, 0f, posicioMurcielago.z);

        //Debug.Log("Nodo objetivo: " + camino.Peek() + ", Distancia: " + Vector3.Distance(posicionSiguiente, posicioMurcielago));
        if (Vector3.Distance(posicionSiguiente, posicioMurcielago) < 0.01)
        {
            camino.Pop();

            if (camino.Count == 0)
            {
                casillaAlcanzada = true;
                return;
            }

            Vector3 casillaActual = gestorCuadricula.NodoCoincidente(transform.position).transform.position;
            direccion = camino.Peek().posicionGlobal - casillaActual;
            direccion = new Vector3(direccion.x, 0f, direccion.z).normalized;
            transform.forward = direccion;
        }
    }

    IEnumerator EsperarYMover()
    {
        while (true)
        {
            yield return new WaitUntil(() => casillaAlcanzada);
            float tiempoEspera = Random.Range(3f, 4f);
            yield return new WaitForSeconds(tiempoEspera);
            if (casillaAlcanzada)
            {
                MoverAUnaCasillaAleatoria();
            }
            casillaAlcanzada = false;
        }
    }

    void MoverAUnaCasillaAleatoria()
    {

        Vector3 direccionHuida = transform.forward;
        direccionHuida.Normalize();

        Transform ultimoPuntoPartidaForward = Embestida(direccionHuida, "forward");
        Transform ultimoPuntoPartidaBackward = Embestida(-direccionHuida, "backward");
        Transform ultimoPuntoPartidaLeft = Embestida(Vector3.Cross(direccionHuida, Vector3.up), "left");
        Transform ultimoPuntoPartidaRight = Embestida(Vector3.Cross(Vector3.up, direccionHuida), "right");

        Vector3 auxForward = gestorCuadricula.NodoCoincidente(ultimoPuntoPartidaForward.transform.position).transform.position - gestorCuadricula.NodoCoincidente(transform.position).transform.position;
        Vector3 auxBackward = gestorCuadricula.NodoCoincidente(ultimoPuntoPartidaBackward.transform.position).transform.position - gestorCuadricula.NodoCoincidente(transform.position).transform.position;
        Vector3 auxLeft = gestorCuadricula.NodoCoincidente(ultimoPuntoPartidaLeft.transform.position).transform.position - gestorCuadricula.NodoCoincidente(transform.position).transform.position;
        Vector3 auxRight = gestorCuadricula.NodoCoincidente(ultimoPuntoPartidaRight.transform.position).transform.position - gestorCuadricula.NodoCoincidente(transform.position).transform.position;

        Transform ultimoPuntoPartidaDefinitivo = transform;

        // Comparar magnitudes
        float magForward = auxForward.magnitude;
        float magBackward = auxBackward.magnitude;
        float magLeft = auxLeft.magnitude;
        float magRight = auxRight.magnitude;

        // Encontrar el mayor
        float mayorMagnitud = Mathf.Max(magForward, magBackward, magLeft, magRight);

        if (mayorMagnitud == magForward)
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartidaForward;
        }
        else if (mayorMagnitud == magBackward)
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartidaBackward;
        }
        else if (mayorMagnitud == magLeft)
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartidaLeft;
        }
        else if (mayorMagnitud == magRight)
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartidaRight;
        }

        if (mayorMagnitud == 0)
        {
            ultimoPuntoPartidaDefinitivo = transform;
        }


        casillaAlcanzada = false;
        Nodo nodoAux = null;
        if (camino != null && camino.Count > 0)
        {
            nodoAux = camino.Peek();
        }
        camino = Pathfinding.Instance.HacerPathFindingEnemigo(transform.position, ultimoPuntoPartidaDefinitivo.position);
        if (nodoAux != null)
        {
            camino.Push(nodoAux);
        }
    }

    private Transform Embestida(Vector3 direccionHuida, string debugLog)
    {
        int pasosDados = 0;
        Transform ultimoPuntoPartida = transform;

        while (pasosDados < 3)
        {
            if (IntentarMover(direccionHuida, debugLog)) // Intentar ir hacia adelante
            {
            }
            else
            {
                break;
            }
        }

        bool IntentarMover(Vector3 direction, string debugLog)
        {
            Vector3 rayStart = (ultimoPuntoPartida.position + direction) + Vector3.up * 3.0f;
            RaycastHit hit;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, 10.0f, casillaLayer))
            {
                Nodo nodoCasilla = hit.collider.GetComponent<Nodo>();
                if (nodoCasilla != null && !nodoCasilla.objeto)
                {
                    //Debug.Log(debugLog);
                    ultimoPuntoPartida = nodoCasilla.transform;
                    pasosDados++;
                    return true; // Se ha movido exitosamente
                }
            }

            return false; // No se ha movido
        }

        return ultimoPuntoPartida;
    }

    private void InvocarEnemigos(object sender, int vidaRestante)
    {
        if (vidaRestante != 0 || !spawner) return;

        GameObject nuevoMurcielago1 = Instantiate(prefabBat, transform.position, Quaternion.identity);
        nuevoMurcielago1.transform.localScale = nuevoMurcielago1.transform.localScale * 0.6f;
        nuevoMurcielago1.SetActive(true);
        nuevoMurcielago1.GetComponent<BatMovement>().spawner = false;
        nuevoMurcielago1.GetComponent<BatMovement>().enabled = true;
        nuevoMurcielago1.GetComponent<BatAttack>().enabled = true;
        nuevoMurcielago1.GetComponent<EnemyVariablesManager>().enabled = true;


        Transform posicionAleatoria = EncontrarCasillaAdyacenteCaminable();

        GameObject nuevoMurcielago2 = Instantiate(prefabBat, new Vector3(posicionAleatoria.position.x, transform.position.y, posicionAleatoria.position.z), Quaternion.identity);
        nuevoMurcielago2.transform.localScale = nuevoMurcielago2.transform.localScale * 0.6f;
        nuevoMurcielago2.SetActive(true);
        nuevoMurcielago2.GetComponent<BatMovement>().spawner = false;
        nuevoMurcielago2.GetComponent<BatMovement>().enabled = true;
        nuevoMurcielago2.GetComponent<BatAttack>().enabled = true;
        nuevoMurcielago2.GetComponent<EnemyVariablesManager>().enabled = true;
    }

    private Transform EncontrarCasillaAdyacenteCaminable()
    {
        Nodo nodoActual = gestorCuadricula.NodoCoincidente(transform.position);
        List<Nodo> vecinos = gestorCuadricula.ListaDeVecinos(nodoActual);
        List<Nodo> casillasDisponibles = new List<Nodo>();

        foreach (Nodo vecino in vecinos)
        {
            if (vecino != null && !vecino.objeto)
            {
                casillasDisponibles.Add(vecino);
                //Debug.Log("Vecino:" + vecino.posicionGlobal);
            }
        }
        //Debug.Log("CasillasDisponibles.count:" + casillasDisponibles.Count);

        if (casillasDisponibles.Count > 0)
        {
            Nodo casillaAleatoria = casillasDisponibles[Random.Range(0, casillasDisponibles.Count)];
            return casillaAleatoria.transform;
        }

        return null;
    }

}