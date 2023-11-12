using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider attackRange;
    public List<GameObject> enemiesTouched = new();

    private void Start()
    {
        attackRange = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        //other.GetComponent<EnemyVariablesManager>().GetDamage();
        enemiesTouched.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        enemiesTouched.Remove(other.gameObject);
    }
}
