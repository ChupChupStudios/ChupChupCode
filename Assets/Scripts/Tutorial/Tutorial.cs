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

    void Start()
    {
        tutorialFog.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialStart()
    {
        startButton.gameObject.SetActive(false);

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
        text.text = "Clica sobre la casilla marcada para andar hasta ella";
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


        text.text = "Camina para rellenar la barra de exploracion. " +
           "Al rellenarla, podrás clicar sobre ella y recibir una recompensa." +
           "Pero cuidado, cuando caminas, también pierdes estamina." +
           "Si agotas la barra amarilla, perderás la partida." +
           "Para recuperar stamina, selecciona una carta de tu mazo y" +
           "pulsa sobre la casilla que se ilumina.";
        //messageButton.SetActive(true);


        tutorialArrow.gameObject.SetActive(false);
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
            //text.enabled = true;
        }

        foreach (GameObject b in botonesGiro)
        {
            b.SetActive(true);
        }

        text.text = "Pulsa sobre los botones que han aparecido para cambiar de dirección";
        messageButton.SetActive(true);
    }


    public void TutorialEnemigo()
    {

        text.text = "Ha aparecido un enemigo." +
            "Tendras que dirigirta hacia él y usar la carta de ataque para derrotarlo." +
            "Pero cuidado, si entras en su rango de ataque, te atacará y perderás stamina.";
        messageButton.SetActive(true);


        tutorialTileObject.GetComponent<Nodo>().enabled = false;
        tutorialTileObject.GetComponent<Block>().enabled = false;
        tutorialTileEnemy.GetComponent<Nodo>().enabled = true;
        tutorialTileEnemy.GetComponent<Block>().enabled = true;

        //ObjectImage.gameObject.SetActive(false);
        //EnemyImage.gameObject.SetActive(true);

        tutorialTileObject.GetComponent<Block>().ChangeColor(new Color32(0, 159, 8, 255));
        tutorialTileEnemy.GetComponent<Block>().ChangeColor(new Color32(243, 129, 0, 255));

        tutorialArrow.SetActive(true);
        tutorialArrow.transform.position = new Vector3(tutorialTileEnemy.transform.position.x, 3.75f, tutorialTileEnemy.transform.position.z);

        tutorialEnemy.gameObject.SetActive(true);
    }


    public void TutorialNiebla()
    {
        Debug.Log("tutoFog");
        text.text = "Busca y usa la carta de niebla para despejar el mapa.";
        messageButton.SetActive(true);
        //EnemyImage.gameObject.SetActive(false);
        //FogImage.gameObject.SetActive(true);

        tutorialTileEnemy.GetComponent<Block>().ChangeColor(new Color32(0, 159, 8, 255));
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
        text.text = "Perky se mueve y puede atacar enemigos o recoger objetos" +
            "A veces se cansa y tiene que volver a su sitio de descanso." +
            "Si ha cogido algun objeto, podemos pedirselo pulsando el botón";
        messageButton.SetActive(true);
        mascota.SetActive(true);
        botonMascota.SetActive(true);
        itemPerky.SetActive(true);
        tutorialEnemyPerky.SetActive(true);
    }

    public void TutorialAvion()
    {
        text.text = "Antes de pasar al siguiente nivel, debes encontrar" +
            "la pieza del avion y arreglarlo.";
        messageButton.SetActive(true);

        keyButton.SetActive(true);
        keyText.SetActive(true);
        keyIcon.SetActive(true);
        keyItem.SetActive(true);
        plane.SetActive(true);


}


    public void TutorialGoal()
    {
        text.text = "La casilla marcada indica el final del nivel." +
            "Si llegas a ella sin perder la stamina, habrás completado el tutorial.";
        messageButton.SetActive(true);
        foreach (GameObject o in tutorialGoalItems)
        {
            o.gameObject.SetActive(true);
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
        text.text = "Recoge el objeto para recibir una carta.";
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
        Debug.Log("DETECTA BOTON");
        messageButton.SetActive(!messageButton.activeSelf);
    }
}
