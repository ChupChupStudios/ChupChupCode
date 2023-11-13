using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    [SerializeField] ACard cardPrefab;
    public Tutorial tutorial;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (DeckManager.cards.Count == 7) return;
            DeckManager.Instance.CreateCard(cardPrefab);
            tutorial.TutorialEnemigo();
            Destroy(gameObject);
        }
    }
}
