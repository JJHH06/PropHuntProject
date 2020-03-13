using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsSettings : MonoBehaviour
{
    public static TeamsSettings settings;
    public GameObject[] PropSpawnPoints;
    public GameObject[] HunterSpawnPoints;


    private void Awake()
    {
        if (TeamsSettings.settings == null)
            TeamsSettings.settings = this;
        else if (TeamsSettings.settings != this)
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    public Vector3 getSpawnLocation(bool isProp, int numero)
    {
        if (isProp) return PropSpawnPoints[numero % PropSpawnPoints.Length].transform.position;

        return HunterSpawnPoints[numero % HunterSpawnPoints.Length].transform.position;
    }
}
