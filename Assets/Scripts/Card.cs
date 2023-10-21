using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public Vector3 pos;
    public int cardType;
    public bool used = false;

    bool selected;

    public CardBehaviour cb;
    public PlayerRayCastManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRayCastManager>();

        if (cardType % 3 == 0)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
        }
        else if (cardType % 3 == 1)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(75, 255, 75, 255);
        }
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cb.cardSelected == id)
        {
            if (selected)
            {
                GameObject tile;
                switch (cardType)
                {
                    case 0:
                        /*|o x o|
                         *|o x o|
                         *|o j o|*/
                        //Debug.Log("Weak attack card selected");

                        tile = pm.Raycast(pm.transform.forward);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward * 2);
                        if (tile == null) return;
                        cb.tileList.Add(tile);
                        tile.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        break;
                    case 1:
                        /*|o o o|
                         *|o x o|
                         *|o j o|*/
                        //Debug.Log("Small fog card selected");

                        tile = pm.Raycast(pm.transform.forward);
                        if (tile == null) return;
                        cb.tileList.Add(tile);
                        tile.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        break;
                    case 2:
                        /*|o  o  o|
                         *|o xjx o|
                         *|o  o  o|*/
                        //Debug.Log("Stamina card selected");

                        tile = pm.Raycast(Vector3.zero);
                        if (tile == null) return;
                        cb.tileList.Add(tile);
                        tile.GetComponent<Renderer>().material.color = new Color32(75, 255, 75, 255);
                        break;
                    case 3:
                        /*|x x x|
                         *|o x o|
                         *|o j o|*/
                        //Debug.Log("Strong attack card selected");

                        tile = pm.Raycast(pm.transform.forward);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward * 2);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward * 2 + pm.transform.right);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward * 2 + pm.transform.right * -1);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        }
                        break;
                    case 4:
                        /*|o x o|
                         *|x x x|
                         *|o j o|*/
                        //Debug.Log("Large fog card selected");

                        tile = pm.Raycast(pm.transform.forward);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward * 2);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward + pm.transform.right);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        }

                        tile = pm.Raycast(pm.transform.forward + pm.transform.right * -1);
                        if (tile != null)
                        {
                            cb.tileList.Add(tile);
                            tile.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        }
                        break;
                }
                if (used)
                {
                    cb.cardSelected = -1;
                }
            }
        }
        else
        {
            selected = false;
            transform.position = pos;
            if (used)
            {
                foreach (GameObject go in cb.tileList)
                {
                    go.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);
                }
                cb.tileList.Clear();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            foreach (GameObject go in cb.tileList)
            {
                go.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);
            }
            cb.tileList.Clear();
            selected = true;
            transform.position += new Vector3(0.0f, 0.5f, 0.0f);
            cb.cardSelected = id;
        }
        else
        {
            selected = false;
            foreach (GameObject go in cb.tileList)
            {
                go.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);
            }
            cb.tileList.Clear();
            cb.cardSelected = -1;
        }
    }
}
