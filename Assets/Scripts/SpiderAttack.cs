using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public float alturaInicialRayoPlayer = 1.25f; // Nueva altura inicial del rayo Player
    public float alturaInicialRayoCasilla = 0.5f; // Nueva altura inicial del rayo Casilla

    public LayerMask casillaLayer;
    public LayerMask playerLayer;

    List<Renderer> renderersAfectados = new List<Renderer>();
    private bool jugadorDetectado = false;

    public SliderStamina sliderStamina;

    void Update()
    {
        Vector3[] casillaCenters = ObtenerCentrosCasillas();

        if (DetectarPlayer(casillaCenters))
        {
            if (!jugadorDetectado) // Verificar si el jugador no ha sido detectado previamente
            {
                DetectarCasillas(casillaCenters);
                jugadorDetectado = true; // Establecer que el jugador ha sido detectado
                sliderStamina.ActualizarSlider(this, 2.5f);
            }
        }
        else
        {
            jugadorDetectado = false; // Reiniciar el estado de detección cuando el jugador no es detectado
            RestablecerColorCasillas();
        }
    }

    bool DetectarPlayer(Vector3[] casillaCenters)
    {
        bool aux = false;

        for (int i = 0; i < casillaCenters.Length; i++)
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

        return aux;
    }

    void DetectarCasillas(Vector3[] casillaCenters)
    {
        for (int i = 0; i < casillaCenters.Length; i++)
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
        Vector3[] casillaCenters = new Vector3[8];

        Vector3 posicionAraña = transform.position;

        casillaCenters[0] = posicionAraña + Vector3.forward + Vector3.right;
        casillaCenters[1] = posicionAraña + Vector3.forward - Vector3.right;
        casillaCenters[2] = posicionAraña - Vector3.forward + Vector3.right;
        casillaCenters[3] = posicionAraña - Vector3.forward - Vector3.right;
        casillaCenters[4] = posicionAraña + Vector3.forward;
        casillaCenters[5] = posicionAraña - Vector3.forward;
        casillaCenters[6] = posicionAraña + Vector3.right;
        casillaCenters[7] = posicionAraña - Vector3.right;

        return casillaCenters;
    }
}
