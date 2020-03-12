using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public static LobbyController lobby;
    public InputField TFroomName;
    public Text datosJugadores;
    public Text nombreRoom;

    /*
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }*/

    //Singleton
    private void Awake()
    {
        lobby = this;
    }

    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectados a: " + PhotonNetwork.CloudRegion);
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public void CreateRoom()
    {
        PhotonNetwork.NickName = MultiplayerSettings.settings.Nickname;
        string nombreDelCuarto = createRadomString(5);
        Debug.Log("Tratando de crear un nuevo cuarto llamado: " + nombreDelCuarto);
        RoomOptions settings = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = MultiplayerSettings.settings.maxPlayers }; 
        PhotonNetwork.CreateRoom(nombreDelCuarto, settings);
    }



    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("No se pudo crear un nuevo room. Debe haber existido ese nombre ya.");
        Debug.Log("Intentando de nuevo...");
        CreateRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.NickName = MultiplayerSettings.settings.Nickname;
        if (TFroomName) {
            string roomName = TFroomName.text;
            Debug.Log("Tratando de unirse al room: " + roomName);
            PhotonNetwork.JoinRoom(roomName);
            UpdateDatos();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No se logró unirse a ese room.");
    }

    public void UpdateDatos()
    {
        if (!PhotonNetwork.InRoom)
        {
            nombreRoom.text = "No se encontró ese cuarto";
            
            return;
        }

        if (nombreRoom)
            nombreRoom.text = PhotonNetwork.CurrentRoom.Name;

        if (datosJugadores)
        {
            datosJugadores.text = "";
            foreach (Photon.Realtime.Player jugador in PhotonNetwork.CurrentRoom.Players.Values)
            {
                datosJugadores.text += jugador.NickName + "\n";
                Debug.Log("HAY UN USUARIO EN EL CUARTO CON NICKNAME: " + jugador.NickName + "\n" + jugador.UserId);
                if (jugador.NickName.Equals(""))
                    Debug.Log("NO HAY NICKNAME");
            }
                
        }

            
    }

    public void leaveRoom()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }


    private string createRadomString(uint lenght)
    {
        char[] acceptableRoomLetters = "ABCDEFXWYZ123456789".ToCharArray();
        char[] nombre = new char[lenght];
        int numeroDeChar = 0;

        for (int i = 0; i < lenght; i++)
        {
            numeroDeChar = Random.Range(0, acceptableRoomLetters.Length);
            nombre[i] = acceptableRoomLetters[numeroDeChar];
        }

        return new string(nombre);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UpdateDatos();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdateDatos();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdateDatos();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        UpdateDatos();
        Debug.Log("Funciona");
    }
}
