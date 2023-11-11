using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour
{
    public float raycastDistance = 2.0f;
    public float alturaInicialRayoPlayer = 2.0f; // Nueva altura inicial del rayo Player
    public float alturaInicialRayoCasilla = 0.5f; // Nueva altura inicial del rayo Casilla
    float velEnterr = 0.5f;

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

    public GameObject raiz;

    private bool curarse;

    void Update()
    {
        Vector3[] casillaCenters = ObtenerCentrosCasillas();
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
                if (casillaCenters[i] == transform.position + Vector3.forward || casillaCenters[i] == transform.position + Vector3.forward * 2)
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
            if (casillaCenters[i] == transform.position + Vector3.forward || casillaCenters[i] == transform.position + Vector3.forward * 2)
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
            if (casillaCenters[i] == transform.position + Vector3.forward || casillaCenters[i] == transform.position + Vector3.forward * 2)
            {
                Vector3 inicioRayo = casillaCenters[i] + Vector3.up * alturaInicialRayoCasilla; // Ajuste de la altura inicial
                RaycastHit hit;
                if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, casillaLayer))
                {
                    Renderer renderer = hit.collider.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderersAfectados.Add(renderer); // Agregar a la lista de renderers afectados
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
        transform.GetChild(0).position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        transform.position += velEnterr * Time.deltaTime * Vector3.down;
        transform.GetChild(0).transform.localScale += velEnterr * Time.deltaTime * (Vector3.right + Vector3.forward);
        if (transform.position.y <= 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            transform.GetChild(0).transform.localScale = new Vector3(1.5f, 0.1f, 1.5f);
            enterrado = true;
        }
    }

    void Desenterrar()
    {
        transform.GetChild(0).position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        transform.position += velEnterr * Time.deltaTime * Vector3.up;
        transform.GetChild(0).transform.localScale -= velEnterr * Time.deltaTime * (Vector3.right + Vector3.forward);
        if (transform.position.y >= 0.5f)
        {
            transform.GetChild(0).position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            transform.GetChild(0).transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
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
            renderer.material.color = new Color32(0, 159, 8, 255); // Restaurar el color original
        }
        renderersAfectados.Clear(); // Limpiar la lista
    }

    Vector3[] ObtenerCentrosCasillas()
    {
        Vector3[] casillaCenters = new Vector3[20];

        Vector3 posicionTopo = transform.position;

        // Posiciones casillas de ataque
        casillaCenters[0] = posicionTopo + Vector3.forward;
        casillaCenters[1] = posicionTopo + Vector3.forward * 2;

        // Posiciones casillas detección
        casillaCenters[2] = posicionTopo + Vector3.right;
        casillaCenters[3] = posicionTopo + Vector3.right * 2;
        casillaCenters[4] = posicionTopo + Vector3.forward * -1;
        casillaCenters[5] = posicionTopo + Vector3.forward * -2;
        casillaCenters[6] = posicionTopo + Vector3.right * -1;
        casillaCenters[7] = posicionTopo + Vector3.right * -2;
        casillaCenters[8] = posicionTopo + Vector3.forward + Vector3.right;
        casillaCenters[9] = posicionTopo + Vector3.forward * 2 + Vector3.right;
        casillaCenters[10] = posicionTopo + Vector3.forward + Vector3.right * 2;
        casillaCenters[11] = posicionTopo + Vector3.forward * -1 + Vector3.right;
        casillaCenters[12] = posicionTopo + Vector3.forward * -2 + Vector3.right;
        casillaCenters[13] = posicionTopo + Vector3.forward * -1 + Vector3.right * 2;
        casillaCenters[14] = posicionTopo + Vector3.forward * -1 + Vector3.right * -1;
        casillaCenters[15] = posicionTopo + Vector3.forward * -2 + Vector3.right * -1;
        casillaCenters[16] = posicionTopo + Vector3.forward * -1 + Vector3.right * -2;
        casillaCenters[17] = posicionTopo + Vector3.forward + Vector3.right * -1;
        casillaCenters[18] = posicionTopo + Vector3.forward * 2 + Vector3.right * -1;
        casillaCenters[19] = posicionTopo + Vector3.forward + Vector3.right * -2;

        return casillaCenters;
    }
}
