using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPunCallbacks
{
    RaycastHit hit;
    public int life;
    bool isHunter;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Screen.lockCursor = true;
        life = 10;
        isHunter = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!isHunter)
            return;

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
        if (!isHunter)
        {
            life--;
            if (life >= 0)
                Destroy(gameObject);
        }

    }
}
