using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAtackCard : AtackCard
{
    public override void ShowEffectArea()
    {
        /*|x x x|
         *|o x o|
         *|o j o|*/
        //Debug.Log("Strong attack card selected");

        GameObject tile;
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

        if (deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward * 2 + deckManager.ownerTransform.right, out tile))
        {
            affectedBlocks.Add(tile);
            tile.GetComponent<Block>().ChangeColor(baseColor);
        }

        if (deckManager.BloqueUsuarioDelMazo(deckManager.ownerTransform.forward * 2 - deckManager.ownerTransform.right, out tile))
        {
            affectedBlocks.Add(tile);
            tile.GetComponent<Block>().ChangeColor(baseColor);
        }
    }
}
