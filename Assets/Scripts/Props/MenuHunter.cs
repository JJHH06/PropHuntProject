using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHunter : MonoBehaviour
{
    public Canvas canvas;
    private bool estado;
    public GameObject camara;
    private GameObject thirdPerson;
    // Start is called before the first frame update
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
            thirdPerson = GameObject.FindGameObjectWithTag("Weapon");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = estado;

            if (camara)
            {
                (camara.GetComponent<GunInventory>()).enabled = !estado;
                (camara.GetComponent<MouseLookScript>()).enabled = !estado;
                (camara.GetComponent<PlayerMovementScript>()).enabled = !estado;
            }
            if (thirdPerson)
            {
                (thirdPerson.GetComponent<GunScript>()).enabled = !estado;
            }



        }
    }
}
