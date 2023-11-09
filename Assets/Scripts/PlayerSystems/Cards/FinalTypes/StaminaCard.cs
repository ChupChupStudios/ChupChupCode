using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCard : ACard
{
    public static event Action<int> SliderEvent;
    public override void ShowEffectArea()
    {
        /*|o  o  o|
         *|o xjx o|
         *|o  o  o|*/
        //Debug.Log("Stamina card selected");

        if (!deckManager.BloqueUsuarioDelMazo(Vector3.zero, out GameObject tile))
            return;

        affectedBlocks.Add(tile);
        tile.GetComponent<Block>().ChangeColor(baseColor);
    }

    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        SliderEvent?.Invoke(25);

        deckManager.Deselect();
        deckManager.cards.Remove(gameObject);
        Destroy(gameObject);
        deckManager.UpdateCardsPositions();
    }
}