using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeyItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el script del contador
            KeyItemManager keyItemManager = FindObjectOfType<KeyItemManager>();

            // Comprobar si se encontró el script
            if (keyItemManager != null)
            {
                // Incrementar el contador y destruir el objeto recolectable
                keyItemManager.RecogerObjeto();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No se encontró el script del contador.");
            }
        }
    }
}
