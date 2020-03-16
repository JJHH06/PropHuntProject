using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLife : MonoBehaviour
{
    public int VidaProp = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (VidaProp <= 0)
        {
            //Aqui iria lo que pasa cuando gane el hunter
            Destroy(gameObject);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            VidaProp--;
        }

    }

}
