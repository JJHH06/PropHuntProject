﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Raycast : MonoBehaviourPunCallbacks
{
    RaycastHit hit;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Screen.lockCursor = true;
    }

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

    [PunRPC]
    void Hit()
    {
        try
        {
            GetComponent<PropLife>().Hit();
        }
        catch
        {

        }
    }
}
