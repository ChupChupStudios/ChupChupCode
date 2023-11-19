using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryUIManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyButtonsPanel;
    [SerializeField] private GameObject enemyImagesPanel;
    [SerializeField] private GameObject enemyInfoPanel;
    [Space]
    [SerializeField] private Button closeButton;

    private Button[] enemyButtons;
    private Dictionary<Button, GameObject> imagesDictionary = new();
    private Dictionary<Button, GameObject> infoDictionary = new();

    Button currentButton = null;

    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------

    private void Start()
    {
        // BOTONES
        enemyButtons = enemyButtonsPanel.GetComponentsInChildren<Button>();

        // IMAGENES
        var enemyImages = new List<Transform>(enemyImagesPanel.GetComponentsInChildren<Transform>());
        enemyImages.RemoveAt(0);

        // DESCRIPCIONES
        var enemyInfo = new Transform[enemyInfoPanel.transform.childCount];
        for (int i = 0; i < enemyInfo.Length; i++)
            enemyInfo[i] = enemyInfoPanel.transform.GetChild(i);


        for (int i = 0; i < enemyButtons.Length; i++)
        {
            // INICIALIZAR DICCIONARIOS
            imagesDictionary.Add(enemyButtons[i], enemyImages[i].gameObject);
            infoDictionary.Add(enemyButtons[i], enemyInfo[i].gameObject);

            // DESACTIVAR TODOS LOS COMPONENTES
            imagesDictionary[enemyButtons[i]].SetActive(false);
            infoDictionary[enemyButtons[i]].SetActive(false);

            // ASIGNAR LISTENERS (NO FUNCIONA)
            //enemyButtons[i].onClick.AddListener(() =>
            //{
            //    ChangeButtonClicked(enemyButtons[i]);
            //});
        }

        // ASIGNAR LISTENERS
        enemyButtons[0].onClick.AddListener(() => ChangeButtonClicked(enemyButtons[0]));
        enemyButtons[1].onClick.AddListener(() => ChangeButtonClicked(enemyButtons[1]));
        enemyButtons[2].onClick.AddListener(() => ChangeButtonClicked(enemyButtons[2]));
        enemyButtons[3].onClick.AddListener(() => ChangeButtonClicked(enemyButtons[3]));

        closeButton.onClick.AddListener(() => OnClose());

        // MOSTRAR DE BASE EL MURCIELAGO
        currentButton = enemyButtons[0];
        ChangeButtonClicked(currentButton);
    }


    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------

    void OnClose()
    {
        currentButton = enemyButtons[0];
        ChangeButtonClicked(currentButton);

        gameObject.SetActive(false);
    }


    //---------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------

    // CAMBIAR DE ENEMIGO SELECCIONADO
    void ChangeButtonClicked(Button newButton)
    {
        // desactivar antiguo (si habia)
        if (currentButton != null) SetEnableBasedOnButton(currentButton, enable: false);
        // activar nuevo
        SetEnableBasedOnButton(newButton, enable: true);

        currentButton = newButton;
    }

    // ACTIVAR / DESACTIVAR INFORMACION DE ENEMIGO CONCRETO
    void SetEnableBasedOnButton(Button toDisable, bool enable)
    {
        imagesDictionary[toDisable].SetActive(enable);
        infoDictionary[toDisable].SetActive(enable);
    }
}
