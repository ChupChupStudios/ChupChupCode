using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color32(255, 0, 232, 255);
    }
}
