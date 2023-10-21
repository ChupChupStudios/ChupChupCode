using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCastManager : MonoBehaviour
{
    public LayerMask tileLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
