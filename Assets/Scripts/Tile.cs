using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public CardBehaviour cb;
    GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        cb = GameObject.FindGameObjectWithTag("Deck").GetComponent<CardBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        foreach (GameObject go in cb.tileList)
        {
            if (this.gameObject == go)
            {
                foreach(GameObject c in cb.cards)
                {
                    if(c.GetComponent<Card>().id == cb.cardSelected)
                    {
                        card = c;
                        c.GetComponent<Card>().used = true;
                    }
                }
                cb.cards.Remove(card);
            }
        }
    }

    private void OnMouseEnter()
    {

        gameObject.GetComponent<Renderer>().material.color = new Color32(255, 245, 21, 255);

    }
    private void OnMouseExit()
    {

        gameObject.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);

    }
}
