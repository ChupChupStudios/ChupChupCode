using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtackCard : ACard
{
    GameObject enemy;
    public LayerMask EnemyLayer;
    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        RaycastHit hit;
        if (Physics.Raycast(tile.gameObject.transform.position + Vector3.down, Vector3.up, out hit, Mathf.Infinity, EnemyLayer))
        {
            enemy = hit.collider.gameObject;
            enemy.GetComponent<EnemyVariablesManager>().GetDamage();
        }

        deckManager.Deselect();
        deckManager.cards.Remove(gameObject);
        Destroy(gameObject);
        deckManager.UpdateCardsPositions();
    }
}