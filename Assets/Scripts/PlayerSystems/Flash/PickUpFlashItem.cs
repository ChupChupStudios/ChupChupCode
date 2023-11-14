using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFlashItem : MonoBehaviour
{
    public ACard[] cardPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DeckManager.cards.Count == 7) return;
            DeckManager.Instance.CreateCard(cardPrefab[0]);
            Destroy(gameObject);
        }
    }
}