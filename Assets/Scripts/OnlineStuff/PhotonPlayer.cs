using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject avatar;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            //Si el jugador es un hunter instancio a un hunter
            if ((int)PhotonNetwork.LocalPlayer.GetTeam() == (int)PunTeams.Team.red)
                PhotonNetwork.Instantiate(System.IO.Path.Combine("Prefabs", "HunterPlayer"), TeamsSettings.settings.getSpawnLocation(false, 0), Quaternion.identity);

            //Si el jugador es un Prop, instancio a un Prop
            else if ((int)PhotonNetwork.LocalPlayer.GetTeam() == (int)PunTeams.Team.blue)
                PhotonNetwork.Instantiate(System.IO.Path.Combine("Prefabs", "PropPlayer"), TeamsSettings.settings.getSpawnLocation(true, 0), Quaternion.identity);

            else Debug.Log("WTF?");

        }
    }

}
