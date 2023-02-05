using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class UIControl : MonoBehaviourPunCallbacks
{
    [Header("���a���A")]
    public bool PlayerIsDead;
    public int FpsCount;
    public bool CanRecord;
    public bool CanSpeak;

    [Header("�C������")]
    public GameObject ExitGameButton;
    public GameObject PlayerCountText;
    public GameObject ShootCountText;
    public GameObject HitCountText;
    public GameObject FpsText;
    public GameObject MicrophoneButton;
    public GameObject TrumpetButton;

    // Start is called before the first frame update
    void Start()
    {
        ExitGameButton.SetActive(false);
        InvokeRepeating("Fps", 1, 1);

        CanRecord = false;
        CanSpeak = false;
    }

    // Update is called once per frame
    void Update()
    {
        FpsCount += 1;

        if (this.gameObject.GetComponent<LifeControl>().NowLife <= 0)
        {
            PlayerIsDead = true;
            ExitGameButton.SetActive(true);
        }

        if (PhotonNetwork.CurrentRoom != null)
        {
            PlayerCountText.GetComponent<Text>().text = "�ثe�H�ơG" + PhotonNetwork.CurrentRoom.PlayerCount + "�H";
        }
        ShootCountText.GetComponent<Text>().text = "�g�����ơG" + this.gameObject.GetComponent<ShootControl>().ShootTimes + "��";
        HitCountText.GetComponent<Text>().text = "�������ơG" + this.gameObject.GetComponent<ShootControl>().KillTimes + "��";
    }

    void Fps()
    {
        FpsText.GetComponent<Text>().text = "FPS�G" + FpsCount;
        FpsCount = 0;
    }

    public void OnClickExitGame()
    {
        ExitGameButton.GetComponentInChildren<Button>().interactable = false;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void OnClickMicrophone()
    {
        if (CanRecord)
        {
            CanRecord = false;
            MicrophoneButton.transform.Find("Cancel Image").gameObject.SetActive(true);
        }
        else
        {
            CanRecord = true;
            MicrophoneButton.transform.Find("Cancel Image").gameObject.SetActive(false);
        }
    }

    public void OnClickTrumpet()
    {
        if (CanSpeak)
        {
            CanSpeak = false;
            TrumpetButton.transform.Find("Cancel Image").gameObject.SetActive(true);
        }
        else
        {
            CanSpeak = true;
            TrumpetButton.transform.Find("Cancel Image").gameObject.SetActive(false);
        }
    }
}
