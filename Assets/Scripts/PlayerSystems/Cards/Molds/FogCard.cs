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



    public override abstract void CheckAndExecute(Block tile);
}
