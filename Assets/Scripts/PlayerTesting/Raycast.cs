using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(myRay, out hit, 40f))
            {
                if (hit.transform.tag == "Props")
                {
                    GetComponent<PhotonView>().RPC("Hit", RpcTarget.Others);
                    Debug.Log("Le dí");

                }
            }
        }
    }
}
