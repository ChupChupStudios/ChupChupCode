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
        if (camino != null && camino.Count > 0)
        {
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

                camino.Pop();

                if (camino.Count == 0)
                {
                    casillaAlcanzada = true;
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
            yield return new WaitUntil(()=>casillaAlcanzada);
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
        Debug.Log("Pompeya");
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

        Huir();
    }

    private void Huir()
    {
        Vector3 direccionHuida = transform.position - Player.transform.position;
        int pasosDados = 0;
        bool movidoALaIzquierda = false;
        bool movidoALaDerecha = false;
        Transform ultimoPuntoPartida = transform;

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

        bool IntentarMover(Vector3 direction, string debugLog)
        {
            Vector3 rayStart = (ultimoPuntoPartida.position + direction) + Vector3.up * 3.0f;
            RaycastHit hit;
            Debug.DrawLine(rayStart, rayStart + Vector3.down * 1.0f, Color.cyan);

            if (Physics.Raycast(rayStart, Vector3.down, out hit, 10.0f, casillaLayer))
            {
                Debug.Log("EntraIF 1" + hit.collider.name);
                Nodo nodoCasilla = hit.collider.GetComponent<Nodo>();
                if (nodoCasilla != null && nodoCasilla.caminable)
                {
                    Debug.Log("EntraIF 2");
                    Debug.Log(debugLog);
                    ultimoPuntoPartida = nodoCasilla.transform;
                    pasosDados++;
                    return true; // Se ha movido exitosamente
                }
            }

            return false; // No se ha movido
        }

        casillaAlcanzada = false;
        Nodo nodoAux = camino.Peek();
        camino = Pathfinding.Instance.HacerPathFinding(transform.position, ultimoPuntoPartida.position);
        camino.Push(nodoAux);

        StartCoroutine(RecuperarArmadura());
    }

    IEnumerator RecuperarArmadura()
    {
        // Esperar 3 segundos
        yield return new WaitForSeconds(3f);
        // Recuperar 1 punto de vidaRestante
        evm.lifePoints++;
    }
}