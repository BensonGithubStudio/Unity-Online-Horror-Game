using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartEffect : MonoBehaviourPunCallbacks
{
    [Header("�즸���J���ܮĪG")]
    public GameObject AwakeBackground;
    public static bool IsFirstTimePlay = true;

    [Header("�C�����J�I���ĪG")]
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
