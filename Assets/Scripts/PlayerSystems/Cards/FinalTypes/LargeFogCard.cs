using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeFogCard : FogCard
{
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
}
