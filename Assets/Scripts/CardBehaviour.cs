using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public const int INITIAL_CARDS = 5;

    public int cardSelected = -1;
    public int currentId;

    public List<GameObject> tileList = new List<GameObject>();
    public List<GameObject> cards = new List<GameObject>();
    public PlayerVariablesManager playerManager;
    public static CardBehaviour Instance;

    Vector3[] cardPositions = { new Vector3(-5.5f, 0.0f, 0.5f), new Vector3(-4.0f, 0.0f, -1.0f), new Vector3(-2.5f, 0.0f, -2.5f), new Vector3(-1.0f, 0.0f, -4.0f), new Vector3(0.5f, 0.0f, -5.5f), new Vector3(2.0f, 0.0f, -7.0f), new Vector3(7.0f, 0.0f, -2.0f) };

    void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        currentId = 0;
        for (int i = 0; i < INITIAL_CARDS; i++)
        {
            //USE DRAWCARD
            //
            GameObject card = GameObject.CreatePrimitive(PrimitiveType.Plane);
            currentId++;
            //card.name = "Card " + i;
            card.AddComponent<Card>();
            card.GetComponent<Card>().id = i;
            card.GetComponent<Card>().pos = cardPositions[i];
            card.GetComponent<Card>().cardType = i;
            card.GetComponent<Card>().cb = this;
            card.GetComponent<Card>().playerManager = playerManager;
            card.GetComponent<Card>().tag = "Card";

            card.transform.position = cardPositions[i];
            card.transform.localScale = new Vector3(0.2f, 1.0f, 0.3f);
            card.transform.rotation = Quaternion.Euler(new Vector3(-60.0f, 45.0f, 0.0f));

            cards.Add(card);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawCard()
    {
        GameObject card = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //card.name = "Card " + i;
        card.AddComponent<Card>();
        card.GetComponent<Card>().id = currentId;
        card.GetComponent<Card>().pos = cardPositions[currentId];
        card.GetComponent<Card>().cardType = 4;
        card.GetComponent<Card>().cb = this;
        card.GetComponent<Card>().playerManager = playerManager;
        card.GetComponent<Card>().tag = "Card";
        card.transform.position = cardPositions[currentId];
        card.transform.localScale = new Vector3(0.2f, 1.0f, 0.3f);
        card.transform.rotation = Quaternion.Euler(new Vector3(-60.0f, 45.0f, 0.0f));

        cards.Add(card);
    }

    public void Redraw()
    {
        foreach (GameObject go in tileList)
        {
            go.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);
        }
    }
}
