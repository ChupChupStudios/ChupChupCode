using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject firstMoveTile;
    public Button startButton;
    public TextMeshProUGUI text;
    public LayerMask Ground;
    public Slider stamina;
    public Slider exploration;
    public DeckManager deckManager;
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


    public bool explorationPressed = false;
    public int defoged = 0;

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
        text.text = "Clica sobre la casilla marcada";

    }

    public void TutorialSliders()
    {
        tutorialArrow.gameObject.SetActive(false);

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

        stamina.gameObject.SetActive(true);
        exploration.gameObject.SetActive(true);
        text.text = "Camina para rellenar la barra de exploracion. " +
            "Al rellenarla, podrás clicar sobre ella y recibir una recompensa." +
            "Pero cuidado, cuando caminas, también pierdes estamina." +
            "Si agotas la barra amarilla, perderás la partida.";


    }


    public void TutorialEnemigo()
    {
        text.text = "Ha aparecido un enemigo." +
            "Tendras que dirigirta hacia él y usar la carta de ataque para derrotarlo." +
            "Pero cuidado, si entras en su rango de ataque, te atacará y perderás stamina." +
            "Usa las cartas de stamina para no quedarte sin energía en el trayecto y curarte de los ataques.";
        tutorialTileObject.GetComponent<Block>().ChangeColor(new Color32(0, 159, 8, 255));
        tutorialTileEnemy.GetComponent<Block>().ChangeColor(new Color32(243, 129, 0, 255));

        tutorialEnemy.gameObject.SetActive(true);


        tutorialTileObject.GetComponent<Nodo>().enabled = false;
        tutorialTileObject.GetComponent<Block>().enabled = false;
        tutorialTileEnemy.GetComponent<Nodo>().enabled = true;
        tutorialTileEnemy.GetComponent<Block>().enabled = true;

    }


    public void TutorialNiebla()
    {
        text.text = "Usa la carta de niebla para despejar el mapa.";
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


    public void TutorialGoal()
    {
        text.text = "La casilla marcada indica el final del nivel." +
            "Si llegas a ella sin perder la stamina, habrás completado el tutorial.";
        foreach(GameObject o in tutorialGoalItems)
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
        deckManager.CreateCard(cardPrefab);
        exploration.value = 0;
        tutorialSliderButton.gameObject.SetActive(false);
        sliderButton.gameObject.SetActive(true);
        tutorialItem.gameObject.SetActive(true);
        text.text = "Recoge el objeto para recibir una carta.";

        GameObject[] foundObjects = GameObject.FindObjectsOfType<GameObject>();

        // Itera a través de los objetos encontrados
        foreach (GameObject foundObject in foundObjects)
        {
            if (foundObject.name == "Box")
            {
                if (foundObject == tutorialTileObject) continue;
                foundObject.GetComponent<Nodo>().enabled = false;
                foundObject.GetComponent<Block>().enabled = false;
            }

            //text.enabled = true;

        }

        tutorialTileObject.GetComponent<Block>().ChangeColor(new Color32(243, 129, 0, 255));

    }

}
