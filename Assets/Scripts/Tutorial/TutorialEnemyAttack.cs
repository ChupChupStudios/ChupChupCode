using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyAttack : MonoBehaviour
{
    public Tutorial tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        tutorial.tutorialArrow.SetActive(false);
        tutorial.TutorialNiebla();
    }
}
