using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour
{
    public float raycastDistance = 3.0f;
    public float alturaInicialRayoPlayer = 3.0f; // Nueva altura inicial del rayo Player
    public float alturaInicialRayoCasilla = 0.5f; // Nueva altura inicial del rayo Casilla
    float velEnterr =1.0f;

    public LayerMask casillaLayer;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public LayerMask criatureLayer;

    List<Renderer> renderersAfectados = new();
    private bool jugadorDetectado = false;
    public bool enterrado = false;

    public bool root = false;
    bool raizDesenterrada = false;

    public SliderStamina sliderStamina;
    public EnemyVariablesManager evm;
    public MoleMovement moleMovement;

    public GameObject raiz;

    private bool curarse;
    //public bool enCasilla = false;

    Color blockColor;

    Vector3[] casillaCenters;

    void Update()
    {
        casillaCenters = ObtenerCentrosCasillas();
        if (!root)
        {
            if (!raizDesenterrada && evm.lifePoints == 1 && DetectarCriatura(casillaCenters, true))
            {
                Enterrar();
            }
            else if (DetectarCriatura(casillaCenters, false))
            {
                Enterrar();
            }
            else
            {
                Desenterrar();
            }
        }
        if (!enterrado)
        {
            if (DetectarPlayer(casillaCenters))
            {
                if (!jugadorDetectado) // Verificar si el jugador no ha sido detectado previamente
                {
                    DetectarCasillas(casillaCenters);
                    jugadorDetectado = true; // Establecer que el jugador ha sido detectado
                    if (!root)
                    {
                        sliderStamina.ActualizarSlider(this, 1.0f);
                    }
                    else
                    {
                        sliderStamina.ActualizarSlider(this, 5.0f);
                        curarse = false;
                        raiz.SetActive(false);
                        root = false;
                    }
                }
            }
            else
            {
                jugadorDetectado = false; // Reiniciar el estado de detección cuando el jugador no es detectado
                RestablecerColorCasillas();
            }
        }

        //if (!raizDesenterrada && evm.lifePoints == 1 && !root && !DetectarCriatura(casillaCenters, true) && enCasilla)
        if (!raizDesenterrada && evm.lifePoints == 1 && !root && !DetectarCriatura(casillaCenters, true))
        {
            Desenterrar();
            if (!enterrado)
            {
                root = true;

                raiz.SetActive(true);
                curarse = true;
                StartCoroutine(ConsumirRaiz());
                raizDesenterrada = true;
                //enCasilla = false;
                //moleMovement.camino.Clear();
            }
        }
    }

    bool DetectarCriatura(Vector3[] casillaCenters, bool any)
    {
        bool aux = false;
        if (any)
        {
            for (int i = 0; i < casillaCenters.Length; i++)
            {
                Vector3 inicioRayo = casillaCenters[i] + Vector3.up * alturaInicialRayoPlayer; // Ajuste de la altura inicial
                RaycastHit hit;
                if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, criatureLayer))
                {
                    aux = true;

                    //Debug.Log($"Rayo {i} detectó un jugador");
                    //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.white);
                }
                else
                {
                    //Debug.Log($"Rayo {i} NO detectó un jugador");
                    //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.black);
                }
            }
        }
        else
        {
            bool playerRange = false;

            for (int i = 0; i < casillaCenters.Length; i++)
            {
                if (casillaCenters[i] == transform.position + transform.forward || casillaCenters[i] == transform.position + transform.forward * 2)
                {
                    Vector3 inicioRayo = casillaCenters[i] + Vector3.up * alturaInicialRayoPlayer; // Ajuste de la altura inicial
                    RaycastHit hit;
                    if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, playerLayer))
                    {
                        playerRange = true;

                        //Debug.Log($"Rayo {i} detectó un jugador");
                        //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.white);
                    }
                    else if (!playerRange)
                    {
                        if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, enemyLayer))
                        {
                            aux = true;
                        }
                    }
                    else
                    {
                        //Debug.Log($"Rayo {i} NO detectó un jugador");
                        //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.black);
                    }
                }
                else
                {
                    if (!playerRange)
                    {
                        Vector3 inicioRayo = casillaCenters[i] + Vector3.up * alturaInicialRayoPlayer; // Ajuste de la altura inicial
                        RaycastHit hit;
                        if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, criatureLayer))
                        {
                            aux = true;

                            //Debug.Log($"Rayo {i} detectó un jugador");
                            //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.white);
                        }
                        else
                        {
                            //Debug.Log($"Rayo {i} NO detectó un jugador");
                            //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.black);
                        }
                    }
                }
            }
        }
        return aux;
    }

    bool DetectarPlayer(Vector3[] casillaCenters)
    {
        bool aux = false;

        for (int i = 0; i < 2; i++)
        {
            if (casillaCenters[i] == transform.position + transform.forward || casillaCenters[i] == transform.position + transform.forward * 2)
            {
                Vector3 inicioRayo = casillaCenters[i] + Vector3.up * alturaInicialRayoPlayer; // Ajuste de la altura inicial
                RaycastHit hit;
                if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, playerLayer))
                {
                    aux = true;

                    //Debug.Log($"Rayo {i} detectó un jugador");
                    //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.white);
                }
                else
                {
                    //Debug.Log($"Rayo {i} NO detectó un jugador");
                    //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.black);
                }
            }
        }

        return aux;
    }

    void DetectarCasillas(Vector3[] casillaCenters)
    {
        for (int i = 0; i < 2; i++)
        {
            if (casillaCenters[i] == transform.position + transform.forward || casillaCenters[i] == transform.position + transform.forward * 2)
            {
                Vector3 inicioRayo = casillaCenters[i] + Vector3.up * alturaInicialRayoCasilla; // Ajuste de la altura inicial
                RaycastHit hit;
                if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, casillaLayer))
                {
                    Renderer renderer = hit.collider.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderersAfectados.Add(renderer); // Agregar a la lista de renderers afectados
                        blockColor = renderer.material.color;
                        Color32 nuevoColor = new Color32(255, 0, 0, 255); // Rojo /RGBA
                        renderer.material.color = nuevoColor;
                    }

                    //Debug.Log($"Rayo {i} detectó una casilla");
                    //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.blue);
                }
                else
                {
                    //Debug.Log($"Rayo {i} NO detectó una casilla");
                    //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.red);
                }
            }
        }
    }

    void Enterrar()
    {
        transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, 0.5f, transform.GetChild(0).position.z);
        transform.position += velEnterr * Time.deltaTime * Vector3.down;
        transform.GetChild(0).transform.localScale += 3 * velEnterr * Time.deltaTime * Vector3.one;
        if (transform.position.y <= -0.75f)
        {
            transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);
            transform.GetChild(0).transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
            enterrado = true;
        }
    }

    void Desenterrar()
    {
        transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, 0.5f, transform.GetChild(0).position.z);
        transform.position += velEnterr * Time.deltaTime * Vector3.up;
        transform.GetChild(0).transform.localScale -= 3 * velEnterr * Time.deltaTime * Vector3.one;
        if (transform.position.y >= 0.5f)
        {
            transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, 0.0f, transform.GetChild(0).position.z);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            transform.GetChild(0).transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            enterrado = false;
        }
    }

    IEnumerator ConsumirRaiz()
    {
        float tiempoEspera = (1.0f);
        for (int i = 0; i < 10; i++)
        {
            if (!curarse) yield break;
            yield return new WaitForSeconds(tiempoEspera);
        }

        raiz.SetActive(false);
        evm.lifePoints++;
        root = false;
        raizDesenterrada = false;

    }

    void RestablecerColorCasillas()
    {
        foreach (Renderer renderer in renderersAfectados)
        {
            renderer.material.color = blockColor; // Restaurar el color original
        }
        renderersAfectados.Clear(); // Limpiar la lista
    }

    //public void CasillaAlcanzadaCallBack()
    //{
    //    //if (!DetectarCriatura(casillaCenters, true)) return;

    //    //enterrado = true;
    //    enCasilla = true;
    //    //moleMovement.camino.Clear();
    //}

    Vector3[] ObtenerCentrosCasillas()
    {
        Vector3[] casillaCenters = new Vector3[20];

        Vector3 posicionTopo = transform.position;

        // Posiciones casillas de ataque
        casillaCenters[0] = posicionTopo + transform.forward;
        casillaCenters[1] = posicionTopo + transform.forward * 2;

        // Posiciones casillas detección
        casillaCenters[2] = posicionTopo + transform.right;
        casillaCenters[3] = posicionTopo + transform.right * 2;
        casillaCenters[4] = posicionTopo + transform.forward * -1;
        casillaCenters[5] = posicionTopo + transform.forward * -2;
        casillaCenters[6] = posicionTopo + transform.right * -1;
        casillaCenters[7] = posicionTopo + transform.right * -2;
        casillaCenters[8] = posicionTopo + transform.forward + transform.right;
        casillaCenters[9] = posicionTopo + transform.forward * 2 + transform.right;
        casillaCenters[10] = posicionTopo + transform.forward + transform.right * 2;
        casillaCenters[11] = posicionTopo + transform.forward * -1 + transform.right;
        casillaCenters[12] = posicionTopo + transform.forward * -2 + transform.right;
        casillaCenters[13] = posicionTopo + transform.forward * -1 + transform.right * 2;
        casillaCenters[14] = posicionTopo + transform.forward * -1 + transform.right * -1;
        casillaCenters[15] = posicionTopo + transform.forward * -2 + transform.right * -1;
        casillaCenters[16] = posicionTopo + transform.forward * -1 + transform.right * -2;
        casillaCenters[17] = posicionTopo + transform.forward + transform.right * -1;
        casillaCenters[18] = posicionTopo + transform.forward * 2 + transform.right * -1;
        casillaCenters[19] = posicionTopo + transform.forward + transform.right * -2;

        return casillaCenters;
    }
}