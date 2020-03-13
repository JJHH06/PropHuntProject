using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit hitInfo;
    public float distancia = 4f;
    void Start()
    {

    }

    [System.Obsolete]
    void FixedUpdate()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out hitInfo, distancia))
            {
                if (hitInfo.collider.gameObject.CompareTag("ObjFireExtin"))
                {




                }

            }

        }

    }
}
