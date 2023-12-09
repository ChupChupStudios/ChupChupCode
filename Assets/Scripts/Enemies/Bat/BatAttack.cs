using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public float alturaInicialRayoPlayer = 1.25f; // Nueva altura inicial del rayo Player
    public float alturaInicialRayoCasilla = 0.5f; // Nueva altura inicial del rayo Casilla

    public LayerMask casillaLayer;
    public LayerMask playerLayer;

    List<Renderer> renderersAfectados = new();
    private bool jugadorDetectado = false;

    public SliderStamina sliderStamina;

    Color blockColor;

    void Update()
    {
        Vector3 posicionMurcielago = transform.position + transform.forward;

        if (DetectarPlayer(posicionMurcielago))
        {
            if (!jugadorDetectado) // Verificar si el jugador no ha sido detectado previamente
            {
                DetectarCasillas(posicionMurcielago);
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

    bool DetectarPlayer(Vector3 casillaCenters)
    {
        bool aux = false;

        Vector3 inicioRayo = casillaCenters + Vector3.up * alturaInicialRayoPlayer; // Ajuste de la altura inicial
        RaycastHit hit;
        if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, playerLayer))
        {
            aux = true;

            //Debug.Log($"Rayo detectó un jugador");
        }
        else
        {
            //Debug.Log($"Rayo NO detectó un jugador");
        }

        return aux;
    }

    void DetectarCasillas(Vector3 casillaCenters)
    {
        Vector3 inicioRayo = casillaCenters + Vector3.up * alturaInicialRayoCasilla; // Ajuste de la altura inicial
        RaycastHit hit;
        if (Physics.Raycast(inicioRayo, Vector3.down, out hit, raycastDistance, casillaLayer))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderersAfectados.Add(renderer); // Agregar a la lista de renderers afectados
                blockColor = renderer.material.color;
                Color32 nuevoColor = new Color32(75, 142, 29, 255); // Rojo /RGBA
                renderer.material.color = nuevoColor;
            }

        }
        else
        {
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
}
