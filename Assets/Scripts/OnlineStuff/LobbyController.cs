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

    //Singleton
    private void Awake()
    {
        if (lobby != this) Destroy(lobby);
        lobby = this;
    }

    
    //Me conecto al servidor
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //Reviso si me conecté al servidor. Por alguna razón no se llama la función
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectados a: " + PhotonNetwork.CloudRegion);
        PhotonNetwork.AutomaticallySyncScene = true;

    }


    //Creo el cuarto
    public void CreateRoom()
    {
        //Defino mi Nickname en caso de que cambiara
        PhotonNetwork.NickName = MultiplayerSettings.settings.Nickname;
        string nombreDelCuarto = createRadomString(5);
        Debug.Log("Tratando de crear un nuevo cuarto llamado: " + nombreDelCuarto);

        //Detalles del cuarto
        RoomOptions settings = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = MultiplayerSettings.settings.maxPlayers }; 
        PhotonNetwork.CreateRoom(nombreDelCuarto, settings);
    }


    //Si no logró crear un cuarto intento de nuevo
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("No se pudo crear un nuevo room. Debe haber existido ese nombre ya.");
        Debug.Log("Intentando de nuevo...");
        CreateRoom();
    }

    //Trato de unirme a un cuarto
    public void JoinRoom()
    {
        //Actualizo mi Nickname
        PhotonNetwork.NickName = MultiplayerSettings.settings.Nickname;

        //Leo el nombre del cuarto y trato de unirme a él.
        if (TFroomName) {
            string roomName = TFroomName.text;
            Debug.Log("Tratando de unirse al room: " + roomName);
            PhotonNetwork.JoinRoom(roomName);

            // Actualizo los datos de los miembros en la partida
            UpdateDatos();
        }
    }

    // Si no logró unirse al cuarto lo imprimo en consola para confirmar
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No se logró unirse a ese room.");
    }


    //Actualizo el label donde voy a tener todos los datos del equipo
    public void UpdateDatos()
    {
        //Si no entré al cuarto lo informo y salgo
        if (!PhotonNetwork.InRoom)
        {
            nombreRoom.text = "No se encontró ese cuarto";
            
            return;
        }

        //Actualizo el label con el nombre del room
        if (nombreRoom)
            nombreRoom.text = PhotonNetwork.CurrentRoom.Name;

        // Actualizo el label con los nombres de los miembros
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

    // Wrapper para salir del Room. Me evita errores en caso de no estar en un room
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

    // Si entro a un cuarto actualizo los datos
    public override void OnJoinedRoom()
    {
        UpdateDatos();
    }


    // Si alguien entró al cuarto actualizo los datos
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdateDatos();
    }

    //Lo mismo de arriba pero si alguien deja el cuarto
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdateDatos();
    }

    // Cuando se crea el cuerto actualizo los datos
    public override void OnCreatedRoom()
    {
        UpdateDatos();
        Debug.Log("Funciona");
    }
}
