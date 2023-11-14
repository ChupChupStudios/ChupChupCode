using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFogCard : FogCard
{
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

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        if (!tile.CheckFogType())
        {
            Debug.Log(tile.gameObject);
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
}
