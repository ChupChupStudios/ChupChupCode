using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public float alturaInicialRayoPlayer = 1.25f; // Nueva altura inicial del rayo Player
    public float alturaInicialRayoCasilla = 0.5f; // Nueva altura inicial del rayo Casilla

    public LayerMask casillaLayer;
    public LayerMask playerLayer;

    List<Renderer> renderersAfectados = new();
    private bool jugadorDetectado = false;

    public SliderStamina sliderStamina;

    public Animator animator;

    Color blockColor;

    void Update()
    {
        Vector3[] casillaCenters = ObtenerCentrosCasillas();

        if (DetectarPlayer(casillaCenters))
        {
            if (!jugadorDetectado) // Verificar si el jugador no ha sido detectado previamente
            {
                DetectarCasillas(casillaCenters);
                jugadorDetectado = true; // Establecer que el jugador ha sido detectado
                animator.SetBool("Moviendo", false);
                sliderStamina.ActualizarSlider(this, 5f);
            }
            gameObject.GetComponent<WolfMovement>().nodoObjetivo = null;
            gameObject.GetComponent<WolfMovement>().Corutina();
        }
        else
        {
            jugadorDetectado = false; // Reiniciar el estado de detecci�n cuando el jugador no es detectado
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

                //Debug.Log($"Rayo {i} detect� un jugador");
                //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.white);
            }
            else
            {
                //Debug.Log($"Rayo {i} NO detect� un jugador");
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
                    blockColor = renderer.material.color;
                    Color32 nuevoColor = new Color32(255, 0, 0, 255); // Rojo /RGBA
                    renderer.material.color = nuevoColor;
                }

                //Debug.Log($"Rayo {i} detect� una casilla");
                //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.blue);
            }
            else
            {
                //Debug.Log($"Rayo {i} NO detect� una casilla");
                //Debug.DrawLine(inicioRayo, inicioRayo + Vector3.down * raycastDistance, Color.red);
            }
        }
    }

    void RestablecerColorCasillas()
    {
        foreach (Renderer renderer in renderersAfectados)
        {
            renderer.material.color = blockColor; // Restaurar el color original
        }
        renderersAfectados.Clear(); // Limpiar la lista
    }

    Vector3[] ObtenerCentrosCasillas()
    {
        Vector3[] casillaCenters = new Vector3[1];

        Vector3 posicionLobo = transform.position;

        casillaCenters[0] = posicionLobo + gameObject.GetComponent<WolfMovement>().direccion;

        return casillaCenters;
    }

    public void OnDestroy()
    {
        RestablecerColorCasillas();
    }
}


