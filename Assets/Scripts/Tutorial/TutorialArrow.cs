using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{

    Vector3 position;
    Vector3 UndernethPosition;
    Vector3 AbovePosition;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        UndernethPosition = new Vector3(position.x, position.y - 2, position.z);
        AbovePosition = new Vector3(position.x, position.y + 2, position.z);
    }

    // Update is called once per frame
    void Update()
    {
       // ArrowMovement();


        float movimientoY = Mathf.Sin(Time.time * 1) * 1f; // La multiplicación por 2f controla la amplitud del movimiento
        transform.position = new Vector3(transform.position.x, movimientoY + 3, transform.position.z);
    }

    public void ArrowMovement()
    {
        if(Time.deltaTime%3==0) transform.position = Vector3.MoveTowards(transform.position, UndernethPosition, 1 * Time.deltaTime);
        else Vector3.MoveTowards(transform.position, AbovePosition, 1.2f * Time.deltaTime);
    }
}
