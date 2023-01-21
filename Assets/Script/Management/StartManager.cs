using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviourPunCallbacks
{
    public GameObject StartButton;

    // Start is called before the first frame update
    void Start()
    {
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
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        int number = Random.Range(1, 1001);
        string name = "Room" + number;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 7;
        PhotonNetwork.CreateRoom(name, roomOptions);

    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
