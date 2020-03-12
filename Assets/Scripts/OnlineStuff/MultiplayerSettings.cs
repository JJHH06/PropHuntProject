using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour
{
    public static MultiplayerSettings settings;
    public byte maxPlayers = 2;
    public string Nickname = "Usuario";

    public int multiplayerScene = 1;

    //Singlentones
    void Awake()
    {
        if (MultiplayerSettings.settings == null)
            MultiplayerSettings.settings = this;
        else if (MultiplayerSettings.settings != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

}
