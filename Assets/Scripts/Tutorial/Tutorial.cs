using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject messageButton;
    public GameObject firstMoveTile;
    public Button startButton;
    public TextMeshProUGUI text;
    public LayerMask Ground;
    public Slider stamina;
    public Slider exploration;
    public ACard cardPrefab;
    public Button sliderButton;
    public Button tutorialSliderButton;
    public GameObject tutorialEnemy;
    public GameObject tutorialFog;
    public GameObject tutorialGoal;
    public GameObject tutorialItem;
    public GameObject tutorialTileEnemy;
    public GameObject tutorialTileObject;
    public GameObject[] tutorialGoalItems;

    public GameObject tutorialArrow;

    public GameObject[] tilesSlider;

    public GameObject hand;


    public GameObject WalkImage;
    public GameObject SliderImage;
    public GameObject ObjectImage;
    public GameObject EnemyImage;
    public GameObject FogImage;

    public bool explorationPressed = false;
    public int defoged = 0;

    public bool tutorialCard = true;

    public GameObject[] botonesGiro;
    public bool girado = false;

    public ACard cardPrefabAttack;


    public GameObject mascota;
    public GameObject botonMascota;
    public GameObject itemPerky;
    public GameObject tutorialEnemyPerky;


    public GameObject keyButton;
    public GameObject keyText;
    public GameObject keyIcon;
    public GameObject keyItem;
    public GameObject plane;

    public GameObject planoAuxiliar;

    public bool explaining;

    public GameObject[] assets;
    public GameObject[] flowers;
    public GameObject finalTile;

    public Nodo[] planeNodes;
    void Start()
    {
        tutorialFog.gameObject.SetActive(false);
        explaining = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialStart()
    {
         

        GameObject[] foundObjects = GameObject.FindObjectsOfType<GameObject>();

        // Itera a través de los objetos encontrados
        foreach (GameObject foundObject in foundObjects)
        {
            if (foundObject.name == "Box")
            {
                foundObject.GetComponent<Nodo>().enabled = false;
                foundObject.GetComponent<Block>().enabled = false;
            }
            //text.enabled = true;
        }
            firstMoveTile.GetComponent<Block>().ChangeColor(new Color32(243, 129, 0, 255));
        text.text = "Clica sobre la casilla marcada para andar hasta ella.";

        startButton.gameObject.SetActive(false);
        messageButton.SetActive(true);
        planoAuxiliar.SetActive(true);
        //Time.timeScale = 0;
        //WalkImage.gameObject.SetActive(true);

    }

    public void TutorialSliders()
    {
        GameObject[] foundObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject foundObject in foundObjects)
        {
            if (foundObject.name == "Box")
            {
                foundObject.SetActive(false);
            }
            //text.enabled = true;
        }
        foreach (GameObject foundAsset in assets)
        {
                foundAsset.SetActive(false);   
            //text.enabled = true;
        }
        foreach (GameObject foundFlowers in flowers)
        {
            foundFlowers.SetActive(false);
            //text.enabled = true;
        }


        text.text = "Camina para rellenar la barra azul. La llamamos barra de exploración. " +
           "Al rellenarla, deberás clicar sobre ella. Recibirás una recompensa y podrás continuar con el tutorial." +
           " Pero cuidado, cuando caminas, también pierdes stamina." +
           " Si agotas la barra amarilla, perderás la partida." +
           " Para recuperar stamina, selecciona una carta de tu mazo y" +
           " pulsa sobre la casilla que se ilumina.";


        messageButton.SetActive(true);
        planoAuxiliar.SetActive(true);
        explaining = true;
        //Time.timeScale = 0; 


        tutorialArrow.gameObject.SetActive(false);
        //Time.timeScale = 0;
        //hand.gameObject.SetActive(true);

        //GameObject[] foundObjects = GameObject.FindObjectsOfType<GameObject>();

        // Itera a través de los objetos encontrados
        foreach (GameObject foundObject in tilesSlider)
        {
            if (foundObject.name == "Box")
            {
                foundObject.gameObject.SetActive(true);
                foundObject.GetComponent<Nodo>().enabled = true;
                foundObject.GetComponent<Block>().enabled = true;
            }
            if (foundObject.name == "Box Tuto")
            {
                foundObject.GetComponent<TutorialTile>().enabled = false;
            }
            //text.enabled = true;
        }

        stamina.gameObject.SetActive(true);
        exploration.gameObject.SetActive(true);
        
       
        

        //WalkImage.gameObject.SetActive(false);
        //SliderImage.gameObject.SetActive(true);


    }


    public void TutorialGiro()
    {
        tutorialArrow.SetActive(false);
        GameObject[] foundObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        // Itera a través de los objetos encontrados
        foreach (GameObject foundObject in foundObjects)
        {
            if (foundObject.name == "Box")
            {
                foundObject.SetActive(true);
                foundObject.GetComponent<Nodo>().enabled = false;
                foundObject.GetComponent<Block>().enabled = false;
            }
            foreach (GameObject foundAsset in assets)
            {
                foundAsset.SetActive(true);
                //text.enabled = true;
            }

            foreach (GameObject foundFlowers in flowers)
            {
                foundFlowers.SetActive(true);
                //text.enabled = true;
            }
            //text.enabled = true;
        }

        foreach (GameObject b in botonesGiro)
        {
            b.SetActive(true);
        }

        text.text = "Pulsa sobre los botones que han aparecido en la " +
            "esquina inferior derecha para cambiar de dirección.";
        messageButton.SetActive(true);
        //Time.timeScale = 0;
        planoAuxiliar.SetActive(true);
        explaining = true;
    }


    public void TutorialEnemigo()
    {

        text.text = "Ha aparecido un enemigo." +
            " Tendrás que dirigirte hacia él pulsando en la casilla indicada. Usa la carta de ataque para derrotarlo." +
            "Pero cuidado, si entras en su rango de ataque, te atacará y perderás stamina.";
        messageButton.SetActive(true);
        //Time.timeScale = 0;
        planoAuxiliar.SetActive(true);
        explaining = true;


        tutorialTileObject.GetComponent<Nodo>().enabled = false;
        tutorialTileObject.GetComponent<Block>().enabled = false;
        tutorialTileEnemy.GetComponent<Nodo>().enabled = true;
        tutorialTileEnemy.GetComponent<Block>().enabled = true;

        //ObjectImage.gameObject.SetActive(false);
        //EnemyImage.gameObject.SetActive(true);

        tutorialTileObject.GetComponent<Block>().ChangeColor(new Color32(29, 132, 9, 255));
        tutorialTileEnemy.GetComponent<Block>().ChangeColor(new Color32(243, 129, 0, 255));

        tutorialArrow.SetActive(true);
        tutorialArrow.transform.position = new Vector3(tutorialTileEnemy.transform.position.x, 3.75f, tutorialTileEnemy.transform.position.z);

        tutorialEnemy.gameObject.SetActive(true);
    }


    public void TutorialNiebla()
    {
        Debug.Log("tutoFog");
        text.text = "Se ha generado niebla. Cuando aparecen estos bloques," +
            " no puedes ver lo que hay debajo ni caminar sobre ellos. Para eliminarla, busca entre los objetos" +
            " que han aparecido una carta blanca. Con ella podrás despejar la niebla. ";
        messageButton.SetActive(true);
        //Time.timeScale = 0;
        planoAuxiliar.SetActive(true);
        explaining = true;
        //EnemyImage.gameObject.SetActive(false);
        //FogImage.gameObject.SetActive(true);

        tutorialTileEnemy.GetComponent<Block>().ChangeColor(new Color32(29, 132, 9, 255));
        tutorialFog.gameObject.SetActive(true);

        GameObject[] foundObjects = GameObject.FindObjectsOfType<GameObject>();

        // Itera a través de los objetos encontrados
        foreach (GameObject foundObject in foundObjects)
        {
            if (foundObject.name == "Box")
            {
                foundObject.GetComponent<Nodo>().enabled = true;
                foundObject.GetComponent<Block>().enabled = true;
            }

            //text.enabled = true;

        }
    }


    public void TutorialMascota()
    {
        text.text = "Perky se mueve solo y puede atacar enemigos o recoger objetos." +
            " A veces se cansa y tiene que volver a su sitio de descanso." +
            " Si ha cogido algun objeto, podemos pedírselo pulsando el botón que encontramos en la esquina superior derecha.";
        messageButton.SetActive(true);
        //Time.timeScale = 0;
        planoAuxiliar.SetActive(true);
        explaining = true;
        mascota.SetActive(true);
        botonMascota.SetActive(true);
        itemPerky.SetActive(true);
        tutorialEnemyPerky.SetActive(true);
    }

    public void TutorialAvion()
    {
        text.text = "Antes de pasar al siguiente nivel, debes recoger" +
            " la pieza del avión que ha aparecido en el mapa y arreglarlo. Para ello, verás que" +
            " al recoger la carta, al lado de las barras de stamina y exploración, aumentara el contador de piezas." +
            " Cuando tengas todas las piezas, podrás pulsar sobre el indicador de piezas conseguidas para" +
            " recibir la carta con la que arreglarás el avión. Úsala sobre una casilla en la que este el avión. ";
        messageButton.SetActive(true);
        //Time.timeScale = 0;
        planoAuxiliar.SetActive(true);
        explaining = true;

        keyButton.SetActive(true);
        keyText.SetActive(true);
        keyIcon.SetActive(true);
        keyItem.SetActive(true);
        plane.SetActive(true);
        foreach(Nodo n in planeNodes)
        {
            n.caminable = false;
        }


}


    public void TutorialGoal()
    {
        text.text = "La casilla marcada indica el final del nivel." +
            "Si llegas a ella sin perder la stamina, habrás completado el tutorial.";
        messageButton.SetActive(true);
        //Time.timeScale = 0;
        planoAuxiliar.SetActive(true);
        explaining = true;
        finalTile.SetActive(true);
        foreach (GameObject o in tutorialGoalItems)
        {
            //o.gameObject.SetActive(true);
        }
        tutorialGoal.GetComponent<Goal>().enabled = true;

        //text.enabled = true;

    }






    public void OnClickSlider()
    {
        if (explorationPressed) return;
        explorationPressed = true;
        DeckManager.Instance.CreateCard(cardPrefab);
        exploration.value = 0;
        tutorialSliderButton.gameObject.SetActive(false);
        sliderButton.gameObject.SetActive(true);
        tutorialTileObject.gameObject.SetActive(true);
        tutorialItem.gameObject.SetActive(true);
        messageButton.SetActive(true);
        explaining = true;
        text.text = "Ha aparecido un objeto, recógelo para recibir una carta.";
        //SliderImage.gameObject.SetActive(false);
        //ObjectImage.gameObject.SetActive(true);

        GameObject[] foundObjects = GameObject.FindObjectsOfType<GameObject>();

        // Itera a través de los objetos encontrados
        foreach (GameObject foundObject in foundObjects)
        {
            if (foundObject.name == "Box")
            {
                if (foundObject == tutorialTileObject)
                {
                    foundObject.GetComponent<Nodo>().enabled = true;
                    foundObject.GetComponent<Block>().enabled = true;
                }
                else
                {
                    foundObject.GetComponent<Nodo>().enabled = false;
                    foundObject.GetComponent<Block>().enabled = false;
                }
            }

            //text.enabled = true;

        }
        tutorialArrow.SetActive(true);
        tutorialArrow.transform.position = new Vector3(tutorialTileObject.transform.position.x, 3.75f, tutorialTileObject.transform.position.z);
        tutorialTileObject.GetComponent<Block>().ChangeColor(new Color32(243, 129, 0, 255));

    }

    public void OnClickGGiro()
    {
        if (!girado)
        {
            girado = true;
            DeckManager.Instance.CreateCard(cardPrefabAttack);
            TutorialEnemigo();
        }
    }

    public void OnClickPerky()
    {
        TutorialAvion();
    }

    public void OnClickMessage()
    {
        planoAuxiliar.SetActive(false);
        messageButton.SetActive(!messageButton.activeSelf);
        explaining = false;

    }


    /*
    IEnumerator EsperarSegundos()
    {
        // Haz algo antes de esperar
        planoAuxiliar.SetActive(false);
        messageButton.SetActive(!messageButton.activeSelf);
        yield return new WaitForSeconds(1f);
        Debug.Log("ENTRA");
        Time.timeScale = 1;
        // Haz algo después de esperar
    }
    */
}
