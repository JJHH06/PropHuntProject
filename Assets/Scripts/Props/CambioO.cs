using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioO : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public float distancia = 4f;
    public GameObject chair;
    public GameObject fire;
    public GameObject parkBench;
    public Text text;


    RaycastHit hitInfo;
    void Start()
    {
        chair.SetActive(false);
        fire.SetActive(false);
        parkBench.SetActive(false);


    }

    // Update is called once per frame
    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out hitInfo, distancia))
            {
                if (hitInfo.collider.gameObject.CompareTag("ObjFireExtin") || hitInfo.collider.gameObject.CompareTag("ObjChair") || hitInfo.collider.gameObject.CompareTag("ObjParkBench"))
                {
                    if (hitInfo.collider.gameObject.CompareTag("ObjFireExtin"))
                    {
                        fire.SetActive(true);
                        chair.SetActive(false);
                        parkBench.SetActive(false);

                        text.text = "Objeto: Fire Extinguisher";
                    }
                    else if (hitInfo.collider.gameObject.CompareTag("ObjChair"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(true);
                        parkBench.SetActive(false);

                        text.text = "Objeto: Chair";
                    }
                    else if (hitInfo.collider.gameObject.CompareTag("ObjParkBench"))
                    {
                        fire.SetActive(false);
                        chair.SetActive(false);
                        parkBench.SetActive(true);

                        text.text = "Objeto: Park Bench";
                    }








                }

            }

        }





    }

}
