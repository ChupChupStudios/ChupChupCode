using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCard : ACard
{
    public LayerMask flash;
    private GameObject raycastOutput;
    private GameObject player;

    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        if (Utils.CustomRaycast(tile.gameObject.transform.position + Vector3.down, Vector3.up, out raycastOutput, flash))
        {            
            player = GameObject.FindWithTag("Player");
            player.transform.position = new Vector3(raycastOutput.transform.position.x, player.transform.position.y, raycastOutput.transform.position.z);

            // Eliminar la carta
            deckManager.Deselect();
            Destroy(gameObject);
        }
        deckManager.Deselect();

    }

    public override void ShowEffectArea()
    {
        GameObject tile;
        if (!deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward * 2, out tile)) return;
        affectedBlocks.Add(tile);
        tile.GetComponent<Block>().ChangeColor(baseColor);
    }
}
