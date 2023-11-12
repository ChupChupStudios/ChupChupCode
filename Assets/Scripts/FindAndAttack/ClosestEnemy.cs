using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemy : MonoBehaviour
{
    public Transform closestEnemyTransform = null;
    float distanceToClosest = Mathf.Infinity;

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        Debug.Log("enemigo detectado");
        float distance = Vector3.Distance(transform.position, other.transform.position);
        if (distance < distanceToClosest)
        {
            closestEnemyTransform = other.transform;
            distanceToClosest = distance;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        if (other.gameObject == closestEnemyTransform.gameObject)
        {
            closestEnemyTransform = null;
            distanceToClosest = Mathf.Infinity;
        }
    }
}
