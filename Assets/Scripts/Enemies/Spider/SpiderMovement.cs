using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    private GestorCuadricula gestorCuadricula;
    private Stack<Nodo> camino;

    public float velocidad = 0.25f;
    private Vector3 direccion = Vector3.zero;
    private bool casillaAlcanzada = true;

    public GameObject prefabSpiderWeb;
    public int contadorTrampasActivas = 0;
    private List<GameObject> telarañasPool = new List<GameObject>();

    EnemyVariablesManager evm;
    public LayerMask casillaLayer;
    public GameObject Player;
    private bool calcularHuida = false;

    private void Awake()
    {
        // Crear el Object Pool de telarañas
        for (int i = 0; i < 2; i++)
        {
            GameObject nuevaTelaraña = Instantiate(prefabSpiderWeb, Vector3.zero, Quaternion.identity);
            nuevaTelaraña.SetActive(false);
            nuevaTelaraña.GetComponent<SpiderWeb>().spider = this.gameObject;
            telarañasPool.Add(nuevaTelaraña);
        }
    }

    void Start()
    {
        evm = this.gameObject.GetComponent<EnemyVariablesManager>();

        gestorCuadricula = FindObjectOfType<GestorCuadricula>();
        StartCoroutine(EsperarYMover());

        if (evm != null)
        {
            evm.Golpeado += ArmaduraRota;
        }

        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        SeguirCamino();
    }

    void SeguirCamino()
    {
        if (camino == null || camino.Count == 0)
        {
            if (!calcularHuida) return;
            Huir();
            calcularHuida = false;
        }

        transform.position += velocidad * Time.deltaTime * direccion;

        Vector3 posicionSiguiente = camino.Peek().posicionGlobal;
        posicionSiguiente = new(posicionSiguiente.x, 0f, posicionSiguiente.z);
        Vector3 posicionArana = transform.position;
        posicionArana = new(posicionArana.x, 0f, posicionArana.z);

        if (Vector3.Distance(posicionSiguiente, posicionArana) < 0.01)
        {
            if (contadorTrampasActivas < telarañasPool.Count)
            {
                // Obtener la telaraña del Object Pool
                GameObject telaraña = ObtenerTelaraña(this.transform.position);
                if (telaraña != null)
                {
                    // Configurar posición y activar la telaraña
                    telaraña.transform.position = this.transform.position;
                    telaraña.SetActive(true);
                    telaraña.GetComponent<SpiderWeb>().trampaActiva = true;

                    contadorTrampasActivas++;
                }
            }

            if (calcularHuida)
            {
                camino.Clear();
                return;
            }

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
            float tiempoEspera = Random.Range(3f, 10f);
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
        Debug.Log("Caminando");
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

    // Método para obtener una telaraña del Object Pool
    private GameObject ObtenerTelaraña(Vector3 newPosition)
    {
        foreach (GameObject telaraña in telarañasPool)
        {
            if (!telaraña.activeSelf)
            {
                // Verificar si la nueva posición coincide con la posición de otra telaraña activa
                bool overlapping = false;
                foreach (GameObject otherTelaraña in telarañasPool)
                {
                    if (otherTelaraña.activeSelf && otherTelaraña != telaraña)
                    {
                        if (Vector3.Distance(newPosition, otherTelaraña.transform.position) < 0.1f)
                        {
                            overlapping = true;
                            break;
                        }
                    }
                }

                if (!overlapping)
                {
                    return telaraña;
                }
            }
        }
        return null; // Si no hay telarañas disponibles en el pool
    }

    // Método para devolver una telaraña al Object Pool
    public void DevolverTelaraña(GameObject telaraña)
    {
        telaraña.SetActive(false);
        contadorTrampasActivas--;
    }

    private void ArmaduraRota(object sender, int vidaRestante)
    {
        if (vidaRestante == 0) return;

        calcularHuida = true;
    }

    private void Huir()
    {
        bool diagonal = false;
        Vector3 direccionHuida = gestorCuadricula.NodoCoincidente(transform.position).transform.position - gestorCuadricula.NodoCoincidente(Player.transform.position).transform.position;
        Vector3 direccionHuida1 = gestorCuadricula.NodoCoincidente(transform.position).transform.position - gestorCuadricula.NodoCoincidente(Player.transform.position).transform.position;

        // Error a veces se mete en diagonal a pesar de que está recto debido a que atacas mientras se mueve a forward.
        // Ahora no se mete en diagonal pero se recorre más casillas por la puta cara

        Debug.Log("a " + direccionHuida.magnitude + "a " + direccionHuida);
        if (direccionHuida.magnitude > 1 && direccionHuida.magnitude != 2)
        {
            diagonal = true;
            direccionHuida.z = 0;
        }

        Debug.Log("b " + direccionHuida1.magnitude + "b " + direccionHuida1);
        if (direccionHuida1.magnitude > 1 && direccionHuida1.magnitude != 2)
        {
            diagonal = true;
            direccionHuida1.x = 0;
        }

        direccionHuida.Normalize();
        direccionHuida1.Normalize();

        int pasosDados = 0;
        bool movidoALaIzquierda = false;
        bool movidoALaDerecha = false;
        Transform ultimoPuntoPartida = transform;

        int pasosDados1 = 0;
        bool movidoALaIzquierda1 = false;
        bool movidoALaDerecha1 = false;
        Transform ultimoPuntoPartida1 = transform;

        while (pasosDados < 3)
        {
            if (IntentarMover(direccionHuida, "forward")) // Intentar ir hacia adelante
            {
                movidoALaIzquierda = false;
                movidoALaDerecha = false;
            }
            else if (!movidoALaDerecha && IntentarMover(Vector3.Cross(direccionHuida, Vector3.up), "left")) // Intentar ir hacia la izquierda
            {
                movidoALaIzquierda = true;
                movidoALaDerecha = false;
            }
            else if (!movidoALaIzquierda && IntentarMover(Vector3.Cross(Vector3.up, direccionHuida), "right")) // Intentar ir hacia la derecha
            {
                movidoALaIzquierda = false;
                movidoALaDerecha = true;
            }
            else
            {
                Debug.Log("ERROR FATAL MUERTE DOLOR Y DESTRUCCIÓN");
                break;
            }
        }

        if (diagonal)
        {
            Debug.Log("Diagonal");
            while (pasosDados1 < 3)
            {
                if (IntentarMover1(direccionHuida1, "forward")) // Intentar ir hacia adelante
                {
                    movidoALaIzquierda1 = false;
                    movidoALaDerecha1 = false;
                }
                else if (!movidoALaDerecha1 && IntentarMover1(Vector3.Cross(direccionHuida1, Vector3.up), "left")) // Intentar ir hacia la izquierda
                {
                    movidoALaIzquierda1 = true;
                    movidoALaDerecha1 = false;
                }
                else if (!movidoALaIzquierda1 && IntentarMover1(Vector3.Cross(Vector3.up, direccionHuida1), "right")) // Intentar ir hacia la derecha
                {
                    movidoALaIzquierda1 = false;
                    movidoALaDerecha1 = true;
                }
                else
                {
                    Debug.Log("ERROR FATAL MUERTE DOLOR Y DESTRUCCIÓN");
                    break;
                }
            }
        }

        bool IntentarMover(Vector3 direction, string debugLog)
        {
            Vector3 rayStart = (ultimoPuntoPartida.position + direction) + Vector3.up * 3.0f;
            RaycastHit hit;
            Debug.DrawLine(rayStart, rayStart + Vector3.down * 1.0f, Color.cyan);

            if (Physics.Raycast(rayStart, Vector3.down, out hit, 10.0f, casillaLayer))
            {
                Debug.Log("aEntraIF 1" + hit.collider.name);
                Nodo nodoCasilla = hit.collider.GetComponent<Nodo>();
                if (nodoCasilla != null && nodoCasilla.caminable)
                {
                    Debug.Log("aEntraIF 2");
                    Debug.Log(debugLog);
                    ultimoPuntoPartida = nodoCasilla.transform;
                    pasosDados++;
                    return true; // Se ha movido exitosamente
                }
            }

            return false; // No se ha movido
        }


        bool IntentarMover1(Vector3 direction1, string debugLog1)
        {
            Vector3 rayStart1 = (ultimoPuntoPartida1.position + direction1) + Vector3.up * 3.0f;
            RaycastHit hit1;
            Debug.DrawLine(rayStart1, rayStart1 + Vector3.down * 1.0f, Color.cyan);

            if (Physics.Raycast(rayStart1, Vector3.down, out hit1, 10.0f, casillaLayer))
            {
                Debug.Log("bEntraIF 1" + hit1.collider.name);
                Nodo nodoCasilla1 = hit1.collider.GetComponent<Nodo>();
                if (nodoCasilla1 != null && nodoCasilla1.caminable)
                {
                    Debug.Log("bEntraIF 2");
                    Debug.Log(debugLog1);
                    ultimoPuntoPartida1 = nodoCasilla1.transform;
                    pasosDados1++;
                    return true; // Se ha movido exitosamente
                }
            }

            return false; // No se ha movido
        }

        Vector3 aux = gestorCuadricula.NodoCoincidente(ultimoPuntoPartida.transform.position).transform.position - gestorCuadricula.NodoCoincidente(Player.transform.position).transform.position;
        Vector3 aux1 = gestorCuadricula.NodoCoincidente(ultimoPuntoPartida1.transform.position).transform.position - gestorCuadricula.NodoCoincidente(Player.transform.position).transform.position;

        Transform ultimoPuntoPartidaDefinitivo;

        if (aux.magnitude > aux1.magnitude && diagonal)
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartida;
        }
        else if (aux1.magnitude > aux.magnitude && diagonal)
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartida1;
        }
        else
        {
            ultimoPuntoPartidaDefinitivo = ultimoPuntoPartida;
        }

        casillaAlcanzada = false;
        Nodo nodoAux = null;
        if (camino != null && camino.Count > 0)
        {
            nodoAux = camino.Peek();
        }
        camino = Pathfinding.Instance.HacerPathFinding(transform.position, ultimoPuntoPartidaDefinitivo.position);
        if (nodoAux != null)
        {
            camino.Push(nodoAux);
        }

        StartCoroutine(RecuperarArmadura());
    }

    IEnumerator RecuperarArmadura()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(15f);
        // Recuperar 1 punto de vidaRestante
        evm.lifePoints++;
    }
}