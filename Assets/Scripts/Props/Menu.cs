using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    private bool estado;
    public GameObject camara;
    

    void Start()
    {
        canvas.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            estado = !estado;
            canvas.enabled = estado;
      
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = estado;

            if (camara)
            {
                (camara.GetComponent("FreeLookCam") as MonoBehaviour).enabled = !estado;
            }
            
            


        }
    }
}
