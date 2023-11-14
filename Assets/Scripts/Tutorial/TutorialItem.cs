using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    [SerializeField] ACard cardPrefab;
    public Tutorial tutorial;
    public DeckManager deckManager;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (deckManager.cards.Count == 7) return;
            deckManager.CreateCard(cardPrefab);
            tutorial.TutorialEnemigo();
            Destroy(gameObject);
        }
    }
}
