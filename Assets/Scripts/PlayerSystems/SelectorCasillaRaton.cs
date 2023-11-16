using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCasillaRaton : MonoBehaviour
{
    public LayerMask capaBloques;
    public LayerMask capaNiebla;
    Camera camaraPrincipal;
    Movimiento movimientoPersonaje;

    private void Start()
    {
        movimientoPersonaje = GetComponent<Movimiento>();
        camaraPrincipal = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        //Debug.Log("Click izquierdo");

        Vector3 posicionRaton = new(Input.mousePosition.x, Input.mousePosition.y, camaraPrincipal.nearClipPlane);
        Vector3 origen = camaraPrincipal.ScreenToWorldPoint(posicionRaton);
        Vector3 direccion = camaraPrincipal.transform.forward;

        // RAYCAST DESDE LA POSICION DEL RATON HACIA ADELANTE (lookAt de la camara)
        if (!Physics.Raycast(origen, direccion, out RaycastHit hit, Mathf.Infinity, capaBloques)) return;

        GameObject casilla = hit.collider.gameObject;
        {

            if (DeckManager.Instance.SelectedCard == null)
            {
                if (casilla.GetComponent<Block>().type == Block.Type.Fog || !casilla.GetComponent<Block>().enabled) return;

                // DEFINIR NUEVA RUTA DEL PERSONAJE
                movimientoPersonaje.DefinirCamino(casilla.GetComponent<Nodo>());
            }
            else
            {
                // COMPROBAR CASILLA CARTA
                if (DeckManager.Instance.SelectedCard.affectedBlocks.Contains(casilla))
                    DeckManager.Instance.BloquePulsadoCallBack(casilla.GetComponent<Block>());
                else
                    DeckManager.Instance.Deselect();
            }

        }
    }
}
