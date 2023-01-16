using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public string PlayerCharacter;
    public GameObject[] StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        MouseControl.CanAppear = !PhotonNetwork.IsConnected;

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Start Scene");
        }
        else
        {
            PhotonNetwork.Instantiate(PlayerCharacter, StartPosition[PhotonNetwork.CurrentRoom.PlayerCount - 1].transform.position, Quaternion.identity);
        }
    }
}
