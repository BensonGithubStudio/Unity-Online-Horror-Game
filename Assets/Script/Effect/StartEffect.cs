using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartEffect : MonoBehaviourPunCallbacks
{
    [Header("初次載入漸變效果")]
    public GameObject AwakeBackground;
    public static bool IsFirstTimePlay = true;

    [Header("遊戲載入背景效果")]
    public GameObject LoadintText;
    public GameObject StartButton;

    // Start is called before the first frame update
    void Start()
    {
        if (IsFirstTimePlay) {
            Invoke("DestroyAwakeBackground", 3);
            IsFirstTimePlay = false;
        }
        else
        {
            DestroyAwakeBackground();
        }

        if (!PhotonNetwork.IsConnected)
        {
            LoadintText.SetActive(true);
            StartButton.SetActive(false);
        }
        else
        {
            LoadintText.SetActive(false);
            StartButton.SetActive(true);
        }
    }

    void DestroyAwakeBackground()
    {
        AwakeBackground.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        LoadintText.SetActive(false);
    }
}
