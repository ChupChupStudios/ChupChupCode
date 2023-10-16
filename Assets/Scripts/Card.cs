using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public Vector3 pos;
    public int cardType;

    bool selected;

    public CardBehaviour cb;
    public PlayerBehaviour pb;
    public SectionBehaviour sb;

    // Start is called before the first frame update
    void Start()
    {
        pb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        sb = GameObject.FindGameObjectWithTag("Floor").GetComponent<SectionBehaviour>();
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
                switch (cardType)
                {
                    case 0:
                        // Si se mira en otra dirección habría que tenerlo en cuenta.
                        Debug.Log("Weak attack card selected");
                        sb.sections[pb.onSection].GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        if (pb.onSection % 7 != 6)
                        {
                            sb.sections[pb.onSection + 1].GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        }
                        break;
                    case 1:
                        Debug.Log("Small fog card selected");
                        // Si se mira en otra dirección habría que tenerlo en cuenta.
                        sb.sections[pb.onSection].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        if (pb.onSection % 7 != 6)
                        {
                            sb.sections[pb.onSection + 1].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        }
                        break;
                    case 2:
                        Debug.Log("Stamina card selected");
                        sb.sections[pb.onSection].GetComponent<Renderer>().material.color = new Color32(75, 255, 75, 255);
                        break;
                    case 3:
                        Debug.Log("Strong attack card selected");
                        sb.sections[pb.onSection].GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                        if (pb.onSection % 7 != 6)
                        {
                            sb.sections[pb.onSection + 1].GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                            if (pb.onSection % 7 != 5)
                            {
                                sb.sections[pb.onSection + 2].GetComponent<Renderer>().material.color = new Color32(255, 75, 75, 255);
                            }
                        }
                        break;
                    case 4:
                        Debug.Log("Large fog card selected");
                        // Si se mira en otra dirección habría que tenerlo en cuenta.
                        sb.sections[pb.onSection].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                        if (pb.onSection % 7 != 6)
                        {
                            sb.sections[pb.onSection + 1].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                            if (pb.onSection % 7 != 5)
                            {
                                sb.sections[pb.onSection + 2].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                            }
                            if (pb.onSection > 6)
                            {
                                sb.sections[pb.onSection - 6].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                            }
                            if (pb.onSection < 42)
                            {
                                sb.sections[pb.onSection + 8].GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
                            }
                        }
                        break;
                }
            }
        }
        else
        {
            selected = false;
            transform.position = pos;
        }
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            foreach (GameObject go in sb.sections)
            {
                go.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);
            }

            selected = true;
            transform.position += new Vector3(0.0f, 0.5f, 0.0f);
            cb.cardSelected = id;
        }
        else
        {
            selected = false;

            foreach (GameObject go in sb.sections)
            {
                go.GetComponent<Renderer>().material.color = new Color32(0, 159, 8, 255);
            }

            cb.cardSelected = -1;
        }
    }
}
