using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int onSection;

    public LayerMask tileLayer;

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

    public GameObject Raycast(Vector3 offset)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 3 + offset, Vector3.down, out hit, Mathf.Infinity, tileLayer))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log(hit.collider.gameObject);

            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
