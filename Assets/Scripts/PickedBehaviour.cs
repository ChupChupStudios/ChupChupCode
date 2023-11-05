using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedBehaviour : MonoBehaviour
{
    // carta que se otorga al recoger el item
    [SerializeField] ACard cardPrefab;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeckManager.Instance.CreateCard(cardPrefab);
            Destroy(gameObject);
        }
    }
}
