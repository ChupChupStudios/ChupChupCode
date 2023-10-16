using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public const int INITIAL_CARDS = 5;

    public int cardSelected = -1;

    Vector3[] cardPositions = { new Vector3(-5.5f, 0.0f, 0.5f), new Vector3(-4.0f, 0.0f, -1.0f), new Vector3(-2.5f, 0.0f, -2.5f), new Vector3(-1.0f, 0.0f, -4.0f), new Vector3(0.5f, 0.0f, -5.5f) };

    void Start()
    {
        for (int i = 0; i < INITIAL_CARDS; i++)
        {
            GameObject card = GameObject.CreatePrimitive(PrimitiveType.Plane);
            //card.name = "Card " + i;
            card.AddComponent<Card>();
            card.GetComponent<Card>().id = i;
            card.GetComponent<Card>().pos = cardPositions[i];
            card.GetComponent<Card>().cardType = i;
            card.GetComponent<Card>().cb = this;

            card.transform.position = cardPositions[i];
            card.transform.localScale = new Vector3(0.2f, 1.0f, 0.3f);
            card.transform.rotation = Quaternion.Euler(new Vector3(-65.0f, 45.0f, 0.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
