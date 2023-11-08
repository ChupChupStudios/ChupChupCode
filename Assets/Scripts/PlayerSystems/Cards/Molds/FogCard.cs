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

        Debug.Log("CARTA DE NIEBLA USADA");

        // Extraer la niebla afectada por la carta:
        var affectedFog = affectedBlocks.FindAll(
            item => item.GetComponent<Block>().type == Block.Type.Fog);
        affectedBlocks.RemoveAll(
            item => item.GetComponent<Block>().type == Block.Type.Fog);

        // Eliminar la niebla afectada
        affectedFog.ForEach(fog => Destroy(fog));

        // Eliminar la carta
        deckManager.Deselect();
        deckManager.cards.Remove(gameObject);
        Destroy(gameObject);
        deckManager.UpdateCardsPositions();
    }
}
