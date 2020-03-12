using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{

    //Objetos del UI
    public GameObject title;
    public GameObject PInicio;
    public GameObject PNewGame;
    public GameObject PRoomJoining;
    public GameObject PEnPartida;


    public void toPInicio()
    {
        title.SetActive(true);
        PInicio.SetActive(true);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);

    }

    public void toNewGame()
    {
        title.SetActive(true);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(true);
        PRoomJoining.SetActive(false);
    }

    public void toPRoomJoining()
    {
        title.SetActive(false);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(true);
    }

    public void toPEnPartida()
    {
        title.SetActive(false);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PEnPartida.SetActive(true);
    }

    public void leaveRoom()
    {
        LobbyController.lobby.leaveRoom();
        toNewGame();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void enterRoom()
    {
        LobbyController.lobby.JoinRoom();
    }

    public void createRoom()
    {
        LobbyController.lobby.CreateRoom();
    }


}