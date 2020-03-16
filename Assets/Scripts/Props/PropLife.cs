using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PropLife : MonoBehaviourPunCallbacks
{
    RaycastHit hit;
    public int life = 5;
    bool isHunter;
    private int maxHits;
    private int counterOfHits = 0;
    public GameObject hunterWin;
    public GameObject propLoose;

    public GameObject camara;
    public GameObject thirdController;
    public GameObject Hunter;


    private void Start()
    {
        maxHits = life;
        Cursor.lockState = CursorLockMode.Locked;
        Screen.lockCursor = true;
        isHunter = (int)PhotonNetwork.LocalPlayer.GetTeam() == (int)PunTeams.Team.red;
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
                    hit.collider.GetComponent<LP>().getHit();
                    counterOfHits++;
                    if (counterOfHits >= maxHits)
                    {
                        //Aqui va lo que pasa cuando gana el hunter gana
                        hunterWin.SetActive(true);
                        (Hunter.GetComponent("MouseLookScript") as MonoBehaviour).enabled = false;
                        (Hunter.GetComponent("PlayerMovementScript") as MonoBehaviour).enabled = false;
                        Screen.lockCursor = false;

                    }

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
            if (life <= 0)
            {
                //Aqui va lo que pasa cuando pierde el prop
                propLoose.SetActive(true);
                (camara.GetComponent("FreeLookCam") as MonoBehaviour).enabled = false;
                (thirdController.GetComponent("vThirdPersonInput") as MonoBehaviour).enabled = false;
                Screen.lockCursor = false;



                // Destroy(gameObject);
            }
        }

    }
}
