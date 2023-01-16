using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        print(1);
    }

    public void OnClickStartGame()
    {
        JoinRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print(4);
        int number = Random.Range(1, 1001);
        string name = "Room" + number;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 7;
        PhotonNetwork.CreateRoom(name, roomOptions);

    }

    public override void OnCreatedRoom()
    {
        JoinRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        print(2);
        SceneManager.LoadScene("Game Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
