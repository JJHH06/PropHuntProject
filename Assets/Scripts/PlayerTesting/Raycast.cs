using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(myRay, out hit,40f))
        {
            if (hit.transform.tag == "Props")
            {
                hit.transform.SendMessage("HitByRay");
                Debug.Log("Funciona!!!");

            }
        }
    }
}
