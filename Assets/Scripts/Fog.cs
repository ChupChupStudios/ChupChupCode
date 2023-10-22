using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public PlayerVariablesManager playerManager;
    public bool fogSelected;
    GameObject auxRemover;
    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        fogSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        if (fogSelected)
        {
            
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
            foreach (GameObject go in cards)
            {
                if(go.GetComponent<Card>().selected)
                {
                    auxRemover = go;
                    go.GetComponent<Card>().cb.cardSelected = -1;              
                    go.GetComponent<Card>().cb.tileList.Clear();
                    
                   // Destroy(go);
                }
            }
            auxRemover.GetComponent<Card>().cb.cards.Remove(auxRemover);
            Destroy(auxRemover);
            //playerManager.fogRemover--;
            GameObject[] fog = GameObject.FindGameObjectsWithTag("Fog");
            foreach (GameObject go in fog)
            {
                if (go.GetComponent<Fog>().fogSelected)
                {
                    if (Physics.Raycast(go.GetComponent<Fog>().transform.position + Vector3.up * 3, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundMask))
                    {
                        hit.collider.gameObject.GetComponent<Nodo>().caminable = true;

                    }
                    Destroy(go);

                }
            }
            
        }
    }

}
