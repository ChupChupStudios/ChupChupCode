using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
   
    public float velocidad = 2.0f; // Velocidad de movimiento

    void Update()
    {
        // Calcula el desplazamiento en el eje x
        float desplazamientoX = Mathf.Sin(Time.time) * velocidad;

        // Aplica el desplazamiento al objeto en el eje x
        transform.position = new Vector3(desplazamientoX + 110f, transform.position.y, transform.position.z);
    }

}







