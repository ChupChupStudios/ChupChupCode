using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlane : MonoBehaviour
{
    public Tutorial tutorial;
    // Start is called before the first frame update
    public void Goal()
    {
        tutorial.TutorialGoal();
    }
}
