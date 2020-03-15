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
    public GameObject PSetNickName;
    public GameObject PPropController;
    public GameObject PHunterController;
    public GameObject PInstructions;
    public InputField TfSetNickName;


    public void toPInicio()
    {
        title.SetActive(true);
        PInicio.SetActive(true);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PSetNickName.SetActive(false);
        PPropController.SetActive(false);
        PHunterController.SetActive(false);
        PInstructions.SetActive(false);

    }

    public void toPInstructions()
    {
        title.SetActive(true);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PSetNickName.SetActive(false);
        PPropController.SetActive(false);
        PHunterController.SetActive(false);
        PInstructions.SetActive(true);

    }

    public void toNewGame()
    {
        title.SetActive(true);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(true);
        PRoomJoining.SetActive(false);
        PSetNickName.SetActive(false);
    }

    public void toPRoomJoining()
    {
        title.SetActive(false);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(true);
        PSetNickName.SetActive(false);
    }

    public void toPPropController()
    {
        
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PSetNickName.SetActive(false);
        PPropController.SetActive(true);
        PHunterController.SetActive(false);
        PInstructions.SetActive(false);
    }

    public void toPHunterController()
    {
        
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PSetNickName.SetActive(false);
        PPropController.SetActive(false);
        PHunterController.SetActive(true);
        PInstructions.SetActive(false);
    }

    public void toPEnPartida()
    {
        title.SetActive(false);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PEnPartida.SetActive(true);
        PSetNickName.SetActive(false);
    }

    public void toPSetNickName()
    {
        title.SetActive(false);
        PInicio.SetActive(false);
        PEnPartida.SetActive(false);
        PNewGame.SetActive(false);
        PRoomJoining.SetActive(false);
        PEnPartida.SetActive(false);
        PSetNickName.SetActive(true);
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

    public void setNickName()
    {
        string Nickname = TfSetNickName.text;
        if (Nickname == null || Nickname.Equals(""))
            Nickname = "Player";

        MultiplayerSettings.settings.Nickname = Nickname;
        Debug.Log(Nickname);
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