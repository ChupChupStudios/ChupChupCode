using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<GameObject> enemiesTouched = new();

    private void OnTriggerEnter(Collider other)
    {
        Utils.Log($"COLISION Entra: {other.name}");
        if (!other.CompareTag("Enemy")) return;

        enemiesTouched.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Utils.Log($"COLISION Sale: {other.name}");
        if (!other.CompareTag("Enemy")) return;

        enemiesTouched.Remove(other.gameObject);
    }
}
