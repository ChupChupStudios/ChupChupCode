using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int onSection;

    public CardBehaviour cb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cb.cardSelected == -1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position += new Vector3(1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(0, 0, 1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(0, 0, -1);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Section")
        {
            //Debug.Log("Colision con casilla " + collision.gameObject.GetComponent<Section>().id);
            onSection = collision.gameObject.GetComponent<Section>().id;
        }
    }
}
