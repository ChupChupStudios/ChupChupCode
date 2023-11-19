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
            if (DeckManager.Instance.cards.Count == 7) return;
            
            DeckManager.Instance.CreateCard(cardPrefab);
            tutorial.tutorialTileObject.GetComponent<Block>().ChangeColor(new Color32(0, 159, 8, 255));
            tutorial.TutorialGiro();
            Destroy(gameObject);
        }
    }
}
