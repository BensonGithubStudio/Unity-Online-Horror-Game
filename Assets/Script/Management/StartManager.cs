using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviourPunCallbacks
{
    public AudioSource ButtonAudioSource;
    public AudioClip ClickSound;

    public GameObject StartButton;
    public GameObject SettingUI;
    public Animator SettingAnimator;
    public GameObject AboutUI;
    public GameObject NeedToInputNameHint;

    // Start is called before the first frame update
    void Start()
    {
        SettingUI.SetActive(false);
        AboutUI.SetActive(false);
        NeedToInputNameHint.SetActive(false);

        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            StartButton.SetActive(false);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        StartButton.SetActive(true);
    }

    public void OnClickStartGame()
    {
        if (NameControl.PlayerName != null && NameControl.PlayerName != "")
        {
            StartButton.GetComponent<Button>().interactable = false;
            ButtonAudioSource.PlayOneShot(ClickSound);
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            NeedToInputNameHint.SetActive(false);
            NeedToInputNameHint.SetActive(true);
            ButtonAudioSource.PlayOneShot(ClickSound);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (StartControl.GameMode == 0)
        {
            int number = Random.Range(1, 1001);
            string name = "Room" + number;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 7;
            //roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable (){{"CustomProperties", "Seven Players"} };

            PhotonNetwork.CreateRoom(name, roomOptions);
        }
        else if (StartControl.GameMode == 1)
        {
            int number = Random.Range(1, 1001);
            string name = "One" + number;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            //roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "CustomProperties", "Two Players" } };

            PhotonNetwork.CreateRoom(name, roomOptions);
        }
        else if (StartControl.GameMode == 2)
        {
            int number = Random.Range(1, 1001);
            string name = "Two" + number;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            //roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "CustomProperties", "Four Players" } };

            PhotonNetwork.CreateRoom(name, roomOptions);
        }
        else if (StartControl.GameMode == 3)
        {
            int number = Random.Range(1, 1001);
            string name = "Three" + number;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 6;
            //roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "CustomProperties", "Six Players" } };

            PhotonNetwork.CreateRoom(name, roomOptions);
        }
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartControl.IsDisconnected = true;
    }
}
