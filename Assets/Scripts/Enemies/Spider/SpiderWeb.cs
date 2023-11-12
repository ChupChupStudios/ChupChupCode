using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public float factorReduccion = 0.5f; // Factor de reducci�n de velocidad
    public float duracionReduccion = 2.0f; // Duraci�n de la reducci�n en segundos
    public GameObject spider;
    public bool trampaActiva = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Obtener el script de movimiento del jugador
            Movimiento movimiento = other.gameObject.GetComponent<Movimiento>();

            if (movimiento != null && trampaActiva)
            {
                // Iniciar la reducci�n de velocidad temporal
                StartCoroutine(ReduceVelocidadTemporalmente(movimiento));
                trampaActiva = false;
            }
        }
    }

    private IEnumerator ReduceVelocidadTemporalmente(Movimiento movimiento)
    {
        // Llamar a la funci�n para reducir la velocidad
        movimiento.CambiarVelocidad(1, factorReduccion);

        // Esperar unos segundos
        yield return new WaitForSeconds(duracionReduccion);

        // Llamar a la funci�n para aumentar la velocidad
        movimiento.CambiarVelocidad(2, factorReduccion);

        // Desactivar la trampa
        spider.GetComponent<SpiderMovement>().DevolverTelara�a(this.gameObject);
    }
}
