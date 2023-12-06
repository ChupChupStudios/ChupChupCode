using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsCollected", menuName = "ScriptableObjects/ItemsCollectedSO", order = 1)]

public class ItemsCollectedSO : ScriptableObject
{
    public int collectedItems = 0;

    //magia fer? (rarete)

    private void Awake()
    {
        collectedItems = 0;
    }
}
