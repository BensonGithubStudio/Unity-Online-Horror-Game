using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviourPunCallbacks
{
    public AudioSource ButtonAudioSource;
    public AudioClip ClickSound;

    public GameObject StartButton;
    public GameObject SettingUI;
    public Animator SettingAnimator;
    public GameObject AboutUI;

    // Start is called before the first frame update
    void Start()
    {
        SettingUI.SetActive(false);
        AboutUI.SetActive(false);

        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        StartButton.SetActive(true);
    }

    public void OnClickStartGame()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
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
