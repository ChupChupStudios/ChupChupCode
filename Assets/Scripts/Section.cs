using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public int id;

    public SectionBehaviour sb;

    // Start is called before the first frame update
    void Start()
    {
        id = sb.sections.IndexOf(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
