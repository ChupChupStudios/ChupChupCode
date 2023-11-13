using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFog : MonoBehaviour
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

    public void OnDestroy()
    {
        tutorial.defoged++;
        if (tutorial.defoged == 4) tutorial.TutorialGoal();
    }
}
