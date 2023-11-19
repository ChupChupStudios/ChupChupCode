using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AtackCard : ACard
{
    GameObject enemy;
    public LayerMask EnemyLayer;
    [SerializeField] private AudioClip grassHit;
    [SerializeField] private AudioClip caveHit;
    public override void HideEffectArea()
    {
        affectedBlocks.ForEach(tile => tile.GetComponent<Block>().ResetColor());
        affectedBlocks.Clear();
    }

    public override void CheckAndExecute(Block tile)
    {
        if (!affectedBlocks.Contains(tile.gameObject)) return;

        int actualScene = SceneManager.GetActiveScene().buildIndex;

        if (actualScene == 4 || actualScene == 5 || actualScene == 6)
        {
            SFXManager.Instance.EjecutarSonido(caveHit);
        }
        else
        {
            SFXManager.Instance.EjecutarSonido(grassHit);
        }

        RaycastHit hit;
        foreach (GameObject block in affectedBlocks)
        {
            if (Physics.Raycast(block.transform.position + Vector3.down, Vector3.up, out hit, Mathf.Infinity, EnemyLayer))
            {
                enemy = hit.collider.gameObject;

                if (enemy.tag != "Topo") enemy.GetComponent<EnemyVariablesManager>().GetDamage();

                else
                {
                    if (!enemy.GetComponent<MoleBehaviour>().enterrado)
                    {
                        enemy.GetComponent<EnemyVariablesManager>().GetDamage();
                    }
                }
            }

        }
        deckManager.Deselect();
        Destroy(gameObject);
    }
}

