using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum Type
    {
        None,
        Ground,
        Fog,
        Structure
    }

    public Type type = Type.None;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask obstacleLayer;

    protected Renderer myRenderer;

    Color baseColor = new Color32(255, 50, 255, 255);
    Color currentColor = new Color32(255, 50, 255, 255);
    public Color selectedTileColor = new Color32(255, 50, 255, 255);


    //----------------------------------------------------------------
    //  METODOS DE UNITY
    //----------------------------------------------------------------

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        baseColor = myRenderer.material.color;
        currentColor = baseColor;

    }


    private void OnMouseDown()
    {
        // AVISAR A DECK MANAGER/CARTA DE CASILLA PULSADA
        //DeckManager.Instance.BloquePulsadoCallBack(this);
    }


    // RATON ENCIMA DE CASILLA:
    private void OnMouseEnter()
    {
        if (!enabled) return;
        if (type == Type.Ground && gameObject.GetComponent<Nodo>().caminable)
            myRenderer.material.color = selectedTileColor;
    }
    private void OnMouseExit()
    {
        if (type == Type.Ground)
            myRenderer.material.color = currentColor;
    }


    private void OnDestroy()
    {
        // Si una niebla se elimina, el bloque de suelo de debajo se vuelve caminable:
        if(type == Type.Fog &&
            Utils.CustomRaycast(transform.position + Vector3.up*3, Vector3.down, out GameObject blockGO, groundLayer))
        {
            if (!Utils.CustomRaycast(blockGO.transform.position + Vector3.down * 3, Vector3.up, out GameObject blockGOO, obstacleLayer))
                blockGO.GetComponent<Nodo>().caminable = true;
        }

    }


    //----------------------------------------------------------------
    //  METODOS
    //----------------------------------------------------------------

    public void ChangeColor(Color newColor)
    {
        myRenderer.material.color = newColor;
        currentColor = newColor;
    }

    public void ResetColor()
    {
        myRenderer.material.color = baseColor;
        currentColor = baseColor;
    }
}
