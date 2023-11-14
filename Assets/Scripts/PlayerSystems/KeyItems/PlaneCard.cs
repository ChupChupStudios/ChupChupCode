using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCard : ACard
{
    public LayerMask plane;
    private GameObject raycastOutput;

    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        if (Utils.CustomRaycast(tile.gameObject.transform.position + Vector3.down, Vector3.up, out raycastOutput, plane))
        {
            // Cuando la use en el avión cambiar de escena a la pantalla de ganar
            Debug.Log("Plane repaired successfully");
            // Eliminar la carta
            deckManager.Deselect();
            Destroy(gameObject);
        }
        deckManager.Deselect();

    }

    public override void ShowEffectArea()
    {
        GameObject tile;
        if (!deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward, out tile)) return;
        affectedBlocks.Add(tile);
        tile.GetComponent<Block>().ChangeColor(baseColor);
    }
}
