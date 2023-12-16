using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UserData")]
public class UserDataSO : ScriptableObject
{
    public string nombre;
    public int edad;
    public int genero;

    //public void OnEnable()
    //{
    //    nombre = "";
    //    edad = 0;
    //    genero = 0;
    //}
}
