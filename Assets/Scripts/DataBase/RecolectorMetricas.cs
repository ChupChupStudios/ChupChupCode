using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecolectorMetricas : MonoBehaviour
{
    private static RecolectorMetricas _instance;
    public static RecolectorMetricas Instance => _instance;

    public int cartasUsadas;
    public int casillasRecorridas;

    public UserDataSO userData;

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        // EVENTO CUANDO EL JUGADOR MUEVE UNA CASILLA
        Movimiento movimientoJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimiento>();
        movimientoJugador.CasillaMovida += (sender, staminaUsed) => casillasRecorridas++;
    }

    // GUARDAR DATOS (SE DEBE LLAMAR CUANDO SE ACABA LA PARTIDA)
    public void GuardarDatos()
    {
        // COGER EL INDICE DE LA ESCENA
        string nombreNivel = SceneManager.GetActiveScene().name;
        int indiceNivel = int.Parse(nombreNivel.Substring(nombreNivel.Length - 1));

        // COGER EL TIEMPO DE JUEGO
        float tiempoNivel = Time.timeSinceLevelLoad;

        // DATOS RECOGIDOS
        // indice de nivel: indiceNivel
        // tiempo: tiempoNivel
        // cartas usadas: cartasUsadas
        // casillas recorridas: casillasRecorridas

        Debug.Log($"indice {indiceNivel}, tiempo {tiempoNivel}, cartas {cartasUsadas}, casillas {casillasRecorridas}");
    }
}
