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



    //public override abstract void CheckAndExecute(Block tile);
    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        // BUSCAR BLOQUES DE NIEBLA
        var affectedFog = affectedBlocks.FindAll(
            item => item.GetComponent<Block>().type == Block.Type.Fog);

        // NO HAY NIEBLA AFECTADA
        if(affectedFog.Count == 0)
        {
            deckManager.Deselect();
            return;
        }

        affectedBlocks.RemoveAll(
            item => item.GetComponent<Block>().type == Block.Type.Fog);

        // Eliminar la niebla afectada
        affectedFog.ForEach(fog => Destroy(fog));

        // Eliminar la carta
        deckManager.Deselect();
        Destroy(gameObject);
    }
}
