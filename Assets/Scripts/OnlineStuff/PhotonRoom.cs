using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

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
        base.OnJoinedRoom();
        Debug.Log("Se logró unir al cuerto");
        //photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();

        if (playersInRoom == MultiplayerSettings.settings.maxPlayers)
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Un jugador se unió al cuarto.");
        //photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;

        Debug.Log(playersInRoom + " / " + MultiplayerSettings.settings.maxPlayers);

        if (playersInRoom == MultiplayerSettings.settings.maxPlayers)
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    void StartGame()
    {
        isGameLoaded = false;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(MultiplayerSettings.settings.multiplayerScene);
        }

    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode modo)
    {
        currentScene = scene.buildIndex;
        if (currentScene == MultiplayerSettings.settings.multiplayerScene)
        {
            isGameLoaded = true;
            PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(System.IO.Path.Combine("Prefabs", "Player"), transform.position, Quaternion.identity);
    }
}
