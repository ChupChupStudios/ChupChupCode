using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorCasillaRaton : MonoBehaviour
{
    public LayerMask capaBloques;
    public LayerMask capaNiebla;
    public LayerMask capaJugador;
    public LayerMask capaEnemigo;

    public DeckManager deckManager;
    Camera camaraPrincipal;
    Movimiento movimientoPersonaje;

    public Tutorial tutorial;

    private void Start()
    {
        movimientoPersonaje = GetComponent<Movimiento>();
        camaraPrincipal = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (tutorial != null)
        {
            Debug.Log(tutorial.explaining);
            if (SceneManager.GetActiveScene().name == "Tutorial" && tutorial.explaining) return;
        }

        Vector3 posicionRaton = new(Input.mousePosition.x, Input.mousePosition.y, camaraPrincipal.nearClipPlane);
        Vector3 origen = camaraPrincipal.ScreenToWorldPoint(posicionRaton);
        Vector3 direccion = camaraPrincipal.transform.forward;

        // RAYCAST DESDE LA POSICION DEL RATON HACIA ADELANTE (lookAt de la camara)
        if (Physics.Raycast(origen, direccion, out RaycastHit hitPlayer, Mathf.Infinity, capaJugador))
        {
            if (deckManager.selectedCard == null) return;
            if (deckManager.selectedCard.type==CardType.Estamina)
            {
                deckManager.selectedCard.CheckAndExecute(gameObject.GetComponent<GestorCasillaActual>().currentNode.gameObject.GetComponent<Block>());
            }
        }

        if (Physics.Raycast(origen, direccion, out RaycastHit hitEnemigo, Mathf.Infinity, capaEnemigo))
        {
            if (deckManager.selectedCard == null) return;
            if (deckManager.selectedCard.type == CardType.Ataque)
            {
                deckManager.selectedCard.CheckAndExecute(hitEnemigo.collider.gameObject.GetComponent<EnemyVariablesManager>().currentNode.GetComponent<Block>());
            }
        }

        if (!Physics.Raycast(origen, direccion, out RaycastHit hit, Mathf.Infinity, capaBloques)) return;

        GameObject casilla = hit.collider.gameObject;
        {

            if (DeckManager.Instance.SelectedCard == null)
            {
                //if (casilla.GetComponent<Block>().type == Block.Type.Fog || !casilla.GetComponent<Block>().enabled) return;
                if (casilla.GetComponent<Block>().type != Block.Type.Ground || !casilla.GetComponent<Nodo>().caminable) return;

                // DEFINIR NUEVA RUTA DEL PERSONAJE
                Utils.Log($"casilla caminable: {casilla.GetComponent<Nodo>().caminable}");
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
