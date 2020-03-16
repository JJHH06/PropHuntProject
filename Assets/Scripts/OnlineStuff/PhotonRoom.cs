using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;
    Photon.Realtime.Player[] photonPlayers;
    private int PlayersLoaded = 0;

    public bool isGameLoaded;
    public int currentScene;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    //Singleton de room
    private void Awake()
    {
        if (PhotonRoom.room = null)
            PhotonRoom.room = this;
        else if (PhotonRoom.room != this)
        {
            Destroy(PhotonRoom.room);
            PhotonRoom.room = this;
        }

        DontDestroyOnLoad(this);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        //Actualizo la lista de jugadores
        photonPlayers = PhotonNetwork.PlayerList;

        //Si ya están todos empiezo
        if (photonPlayers.Length == MultiplayerSettings.settings.maxPlayers)
            if (PhotonNetwork.IsMasterClient)
                StartGame();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        //Actualizo la lista de jugadores
        photonPlayers = PhotonNetwork.PlayerList;

        //Si ya están todos empiezo
        if (photonPlayers.Length == MultiplayerSettings.settings.maxPlayers)
            if (PhotonNetwork.IsMasterClient)
                StartGame();
    }

    void StartGame()
    {
        //Si todavía no he empezado el juego y soy el "dueño" de la partida cambio la escena
        if (!isGameLoaded && PhotonNetwork.IsMasterClient)
        {

            //Nadie más puede entrar al cuarto
            PhotonNetwork.CurrentRoom.IsOpen = false;


            //Defino los equipos para cada jugador
            for (int i = 0; i < photonPlayers.Length; i++)
            {
                if (i % 2 == 0)
                    photonPlayers[i].SetTeam(PunTeams.Team.red);
                else
                    photonPlayers[i].SetTeam(PunTeams.Team.blue);
            }

            //Cargo la escena
            PhotonNetwork.LoadLevel(MultiplayerSettings.settings.multiplayerScene);

            //Digo que la partida ya empezó
            isGameLoaded = true;

        }

    }

    // Cuando termino de cargar la escena: 
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode modo)
    {
        // Guardo la escena por si más adelante quiero confirmar que no estoy en el lobby
        currentScene = scene.buildIndex;
        if (currentScene == MultiplayerSettings.settings.multiplayerScene)
        {
            // Digo que la partida ya empezó
            isGameLoaded = true;
            // Mando un mensaje al "dueño" de la partida
            PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        }
    }

    // Envia un mensaje a todos los miembros de la partida que creen su jugador
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        
        if (++PlayersLoaded == photonPlayers.Length && photonPlayers.Length == PhotonNetwork.PlayerList.Length)
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
    }


    // Crear jugador
    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(System.IO.Path.Combine("Prefabs", "Player"), new Vector3(0, 0, 0), Quaternion.identity);
    }

    [PunRPC]
    private void Hit()
    {

    }
}
