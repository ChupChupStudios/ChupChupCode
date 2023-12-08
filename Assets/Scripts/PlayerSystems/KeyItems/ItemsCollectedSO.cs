using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ItemsCollected", menuName = "ScriptableObjects/ItemsCollectedSO", order = 1)]

public class ItemsCollectedSO : ScriptableObject
{
    // PIEZAS CONSEGUIDAS
    public int CollectedItems
    {
        get => collectedItemsIndex.Count;
    }

    // LISTA DE INDICES DE ESCENAS COMPLETADAS (ha conseguido salir con la pieza)
    public List<int> collectedItemsIndex = new ();

    // EXTERNAMENTE SE INDICA QUE EL NIVEL SE HA COMPLETADO (teniendo la pieza)
    public void LevelCompletedCallBack()
    {
        // COMPROBAR INDICE DE LA ESCENA (el nivel)
        int index = SceneManager.GetActiveScene().buildIndex;
        if (collectedItemsIndex.Contains(index)) return;

        // GUARDAR INDICE DE ESCENA
        collectedItemsIndex.Add(index);
    }

    //magia fer? (rarete) si rodro magia

    private void OnEnable()
    {
        collectedItemsIndex = new();
    }
}
