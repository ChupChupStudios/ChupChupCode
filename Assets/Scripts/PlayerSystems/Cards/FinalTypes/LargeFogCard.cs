using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeFogCard : FogCard
{

    bool checkFog = false;


    public override void ShowEffectArea()
    {
        GameObject tile;
        /*|o x o|
         *|x x x|
         *|o j o|*/
        //Debug.Log("Large fog card selected");

        if (deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward, out tile))
        {
            affectedBlocks.Add(tile);
            tile.GetComponent<Block>().ChangeColor(baseColor);
        }

        if (deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward * 2, out tile))
        {
            affectedBlocks.Add(tile);
            tile.GetComponent<Block>().ChangeColor(baseColor);
        }

        if (deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward + deckManager.ownerTransform.right, out tile))
        {
            affectedBlocks.Add(tile);
            tile.GetComponent<Block>().ChangeColor(baseColor);
        }

        if (deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward - deckManager.ownerTransform.right, out tile))
        {
            affectedBlocks.Add(tile);
            tile.GetComponent<Block>().ChangeColor(baseColor);
        }
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

            if (!tile.CheckFogType() && !checkNeighbours())
            {
                deckManager.Deselect();
                return;
            }

        // Extraer la niebla afectada por la carta:

        checkFog = false;


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

    public bool checkNeighbours()
    {
        foreach (GameObject block in affectedBlocks)
        {
            if (block.GetComponent<Block>().CheckFogType())
            {
                checkFog = true;
            }

        }

        return checkFog;
    }
}
