using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    // Devuelve true si choca con un objeto en layermask
    // Si hay colision devuelve el GameObject correspondiente
    public static bool CustomRaycast(Vector3 origin, Vector3 direction, out GameObject result, LayerMask layerMask)
    {
        result = null;
        if (!Physics.Raycast(origin, direction, out RaycastHit output, Mathf.Infinity, layerMask))
            return false;

        result = output.collider.gameObject;
        return true;
    }
}
