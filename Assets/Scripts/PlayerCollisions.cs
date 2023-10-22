using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public CardBehaviour cardBehaviour;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            cardBehaviour.DrawCard();
            Destroy(gameObject);

        }
    }
    


    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Entra");
        if (collision.gameObject.CompareTag("Player"))
        {
            cardBehaviour.DrawCard();
            Destroy(gameObject);

        }
    }
}
