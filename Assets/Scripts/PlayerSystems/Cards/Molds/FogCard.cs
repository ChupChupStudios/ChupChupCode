using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FogCard : ACard
{
    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;
        if (!tile.CheckFogType())
        {
            deckManager.Deselect();
            return;
        }

        // Extraer la niebla afectada por la carta:
        var affectedFog = affectedBlocks.FindAll(
            item => item.GetComponent<Block>().type == Block.Type.Fog);
        affectedBlocks.RemoveAll(
            item => item.GetComponent<Block>().type == Block.Type.Fog);

        // Eliminar la niebla afectada
        affectedFog.ForEach(fog => Destroy(fog));

        // Eliminar la carta
        deckManager.Deselect();
        Destroy(gameObject);
    }

    public override void ShowEffectArea()
    {
        /*|o o o|
         *|o x o|
         *|o j o|*/
        //Debug.Log("Small fog card selected");

        GameObject tile;
        if (!deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward, out tile)) return;
        affectedBlocks.Add(tile);
        tile.GetComponent<Block>().ChangeColor(baseColor);
    }
}
