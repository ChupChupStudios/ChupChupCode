using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedBehaviour : MonoBehaviour
{
    // carta que se otorga al recoger el item
    [SerializeField] ACard cardPrefab;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) PickUpItem();
    }

    public void PickUpItem()
    {
         if (DeckManager.Instance.cards.Count == 7) return;
        DeckManager.Instance.CreateCard(cardPrefab);
        Destroy(gameObject);
    }
}
