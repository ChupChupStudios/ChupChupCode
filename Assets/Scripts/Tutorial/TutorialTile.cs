using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTile : MonoBehaviour
{
    GameObject player;
    public LayerMask Player;
    public Tutorial tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Utils.CustomRaycast(transform.position + Vector3.down, Vector3.up, out player, Player))
        {
            this.GetComponent<Block>().ChangeColor(new Color32(0, 159, 8, 255));
            tutorial.messageButton.SetActive(true);
            tutorial.TutorialSliders();


        }
    }
}
