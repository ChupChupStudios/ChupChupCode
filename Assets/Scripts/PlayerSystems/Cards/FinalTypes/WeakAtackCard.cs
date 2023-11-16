using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakAtackCard : AtackCard
{
    public override void ShowEffectArea()
    {
        /*|o x o|
         *|o x o|
         *|o j o|*/

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
    }
}
