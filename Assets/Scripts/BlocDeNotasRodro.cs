//using UnityEngine;

//private void Huir()
//{
//    int pasosDados = 0;
//    bool movidoALaIzquierda = false;
//    bool movidoALaDerecha = false;

//    while (pasosDados < 3)
//    {
//        if (IntentarMover(transform.forward, "forward")) // Intentar ir hacia adelante
//        {
//            movidoALaIzquierda = false;
//            movidoALaDerecha = false;
//        }
//        else if (!movidoALaDerecha && IntentarMover(-transform.right, "left")) // Intentar ir hacia la izquierda
//        {
//            movidoALaIzquierda = true;
//            movidoALaDerecha = false;
//        }
//        else if (!movidoALaIzquierda && IntentarMover(transform.right, "right")) // Intentar ir hacia la derecha
//        {
//            movidoALaIzquierda = false;
//            movidoALaDerecha = true;
//        }
//        else
//        {
//            Debug.Log("ERROR FATAL MUERTE DOLOR Y DESTRUCCIÓN");
//            break;
//        }
//    }

//    bool IntentarMover(Vector3 direction, string debugLog)
//    {
//        Vector3 rayStart = (transform.position + direction) + Vector3.up * 0.5f;
//        RaycastHit hit;
//        Debug.DrawLine(rayStart, rayStart + Vector3.down * 1.0f, Color.cyan);

//        if (Physics.Raycast(rayStart, Vector3.down, out hit, 1.0f, casillaLayer))
//        {
//            Debug.Log("EntraIF 1");
//            Nodo nodoCasilla = hit.collider.GetComponent<Nodo>();
//            if (nodoCasilla != null && nodoCasilla.caminable)
//            {
//                Debug.Log("EntraIF 2");
//                Debug.Log(debugLog);
//                transform.position = nodoCasilla.posicionGlobal;
//                pasosDados++;
//                return true; // Se ha movido exitosamente
//            }
//        }

//        return false; // No se ha movido
//    }

//    // Esperar 3 segundos
//    //yield return new WaitForSeconds(3f);
//    // Recuperar 1 punto de vidaRestante
//    evm.lifePoints++;
//    // Restablecer el comportamiento normal
//    armaduraRota = false;
//}









//private void Huir()
//{
//    int currentState = 0; // Variable para controlar el estado
//    int pasosDados = 0;

//    do
//    {
//        switch (currentState)
//        {
//            case 0:
//                HandleRaycast(Vector3.forward, "0");
//                break;

//            case 1:
//                HandleRaycast(Vector3.right, "1");
//                break;

//            case 2:
//                HandleRaycast(-Vector3.right, "2");
//                break;

//            default:
//                break;
//        }

//    } while (pasosDados != 3);

//    void HandleRaycast(Vector3 direction, string debugLog)
//    {
//        Vector3 rayStart = (transform.position + direction) + Vector3.up * 0.5f;
//        RaycastHit hit;
//        Debug.DrawLine(rayStart, rayStart + Vector3.down * 1.0f, Color.cyan);

//        if (Physics.Raycast(rayStart, Vector3.down, out hit, 1.0f, casillaLayer))
//        {
//            Debug.Log("EntraIF1");
//            Nodo nodoCasilla = hit.collider.GetComponent<Nodo>();
//            if (nodoCasilla != null && nodoCasilla.caminable)
//            {
//                Debug.Log("EntraIF2");
//                Debug.Log(debugLog);
//                transform.position = nodoCasilla.posicionGlobal;
//                pasosDados++;
//            }
//            else
//            {
//                Debug.Log("EntraELSE2");
//                currentState++;
//            }
//        }
//        else
//        {
//            Debug.Log("EntraELSE1");
//            currentState++;
//        }
//    }
//}




















//for (int i = 0; i < 3; i++)
//    {


//        Vector3 inicioRayo1 = (transform.position + Vector3.forward) + Vector3.up * 0.5f;
//        RaycastHit hit1;
//        Debug.DrawLine(inicioRayo1, inicioRayo1 + Vector3.down * 1.0f, Color.cyan);

//        Vector3 inicioRayo2 = (transform.position + Vector3.right) + Vector3.up * 0.5f;
//        RaycastHit hit2;
//        Debug.DrawLine(inicioRayo2, inicioRayo2 + Vector3.down * 1.0f, Color.cyan);

//        Vector3 inicioRayo3 = (transform.position + -Vector3.right) + Vector3.up * 0.5f;
//        RaycastHit hit3;
//        Debug.DrawLine(inicioRayo3, inicioRayo3 + Vector3.down * 1.0f, Color.cyan);

//        if (Physics.Raycast(inicioRayo1, Vector3.down, out hit1, 1.0f, casillaLayer))
//        {
//            Nodo nodoCasilla1 = hit1.collider.GetComponent<Nodo>();
//            if (nodoCasilla1 != null && nodoCasilla1.caminable)
//            {
//                Debug.Log("11");
//                transform.position = nodoCasilla1.posicionGlobal;
//            }
//            else if (Physics.Raycast(inicioRayo2, Vector3.down, out hit2, 1.0f, casillaLayer))
//            {
//                Nodo nodoCasilla2 = hit2.collider.GetComponent<Nodo>();
//                if (nodoCasilla2 != null && nodoCasilla2.caminable)
//                {
//                    Debug.Log("12");
//                    transform.position = nodoCasilla2.posicionGlobal;
//                }
//                else if (Physics.Raycast(inicioRayo3, Vector3.down, out hit3, 1.0f, casillaLayer))
//                {
//                    Nodo nodoCasilla3 = hit3.collider.GetComponent<Nodo>();
//                    if (nodoCasilla3 != null && nodoCasilla3.caminable)
//                    {
//                        Debug.Log("13");
//                        transform.position = nodoCasilla3.posicionGlobal;
//                    }
//                }
//            }
//        }
//        else if (Physics.Raycast(inicioRayo2, Vector3.down, out hit2, 1.0f, casillaLayer))
//        {
//            Nodo nodoCasilla2 = hit2.collider.GetComponent<Nodo>();
//            if (nodoCasilla2 != null && nodoCasilla2.caminable)
//            {
//                Debug.Log("22");
//                transform.position = nodoCasilla2.posicionGlobal;
//            }
//            else if (Physics.Raycast(inicioRayo3, Vector3.down, out hit3, 1.0f, casillaLayer))
//            {
//                Nodo nodoCasilla3 = hit3.collider.GetComponent<Nodo>();
//                if (nodoCasilla3 != null && nodoCasilla3.caminable)
//                {
//                    Debug.Log("23");
//                    transform.position = nodoCasilla3.posicionGlobal;
//                }
//            }
//        }
//        else if (Physics.Raycast(inicioRayo3, Vector3.down, out hit3, 1.0f, casillaLayer))
//        {
//            Nodo nodoCasilla3 = hit3.collider.GetComponent<Nodo>();
//            if (nodoCasilla3 != null && nodoCasilla3.caminable)
//            {
//                Debug.Log("33");
//                transform.position = nodoCasilla3.posicionGlobal;
//            }
//        }



//    }