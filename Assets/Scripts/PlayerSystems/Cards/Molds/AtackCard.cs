using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtackCard : ACard
{
    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        Debug.Log("CARTA DE ATAQUE USADA");

        deckManager.Deselect();
        Destroy(gameObject);
    }
}
