using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCollision : MonoBehaviour
{
    public GameObject PropFather;
    PropLife lifeManager = null;
    // Start is called before the first frame update
    void Start()
    {
        lifeManager = PropFather.GetComponent<PropLife>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            lifeManager.VidaProp--;
        }

    }

    void HitByRay()
    {
        lifeManager.VidaProp--;
    }
}
