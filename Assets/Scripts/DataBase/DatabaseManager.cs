using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    string username;
    string password;
    string uri;
    string contentType = "application/json";

    private void Awake()
    {
        LoadCredentials();
    }
    /*public void UploadData()
    {
        StartCoroutine(SendPostRequest());
    }*/

    //============================================================
    //  CARGAR CREDENCIALES
    //============================================================

    void LoadCredentials()
    {
        string configPath = "Assets/Scripts/DataBase/config.json";

        if (File.Exists(configPath))
        {
            string configJson = File.ReadAllText(configPath);
            var config = JsonUtility.FromJson<Credentials>(configJson);

            username = config.username;
            password = config.password;
            uri = config.uri;
        }
        else
        {
            //Debug.LogError("Config file not found!");
            Debug.Log("Config file not found!");
        }
    }

    [System.Serializable]
    private class Credentials
    {
        public string username;
        public string password;
        public string uri;
    }

    //============================================================
    //  ENVIO DE DATOS
    //============================================================
    string _nombre, _edad, _genero,
        _indiceNivel, _tiempo, _cartasUsadas, _casillasRecorridas;
    public void UploadData(string nombre, string edad, string genero,
        string indiceNivel, string tiempo, string cartasUsadas, string casillasRecorridas)
    {
        _nombre = nombre;
        _edad = edad;
        _genero = genero;
        _indiceNivel = indiceNivel;
        _tiempo = tiempo;
        _cartasUsadas = cartasUsadas;
        _casillasRecorridas = casillasRecorridas;

        StartCoroutine(SendPostRequest());
    }
    IEnumerator SendPostRequest()
    {
        string data = CreateJSON("alumnos", _nombre, _edad, _genero,
            _indiceNivel, _tiempo, _cartasUsadas, _casillasRecorridas);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, data, contentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                print("Error: " + www.error);
            }
            else
            {
                print("Respuesta: " + www.downloadHandler.text);
            }
        }

        // CAMBIAR DE NIVEL AL SIGUIENTE
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual + 1);
    }
    string CreateJSON(string tabla, string nombre, string edad, string genero,
        string indiceNivel, string tiempo, string cartasUsadas, string casillasRecorridas)
    {
        //Construye JSON para la petición REST         
        string json = $@"{{
            ""username"":""{username}"",
            ""password"":""{password}"",
            ""table"":""{tabla}"",
            ""data"": {{
                ""nombre"": ""{nombre}"",
                ""edad"": ""{edad}"",
                ""genero"": ""{genero}"",
                ""indiceNivel"": ""{indiceNivel}"",
                ""tiempo"": ""{tiempo}"",
                ""cartasUsadas"": ""{cartasUsadas}"",
                ""casillasRecorridas"": ""{casillasRecorridas}""
            }}
        }}";

        return json;
    }

    
}
