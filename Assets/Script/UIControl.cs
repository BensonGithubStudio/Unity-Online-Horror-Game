using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class UIControl : MonoBehaviourPunCallbacks
{
    [Header("產篈")]
    public bool PlayerIsDead;
    public int FpsCount;
    public bool CanRecord;
    public bool CanSpeak;

    [Header("笴栏ざ")]
    public GameObject ExitGameButton;
    public GameObject PlayerCountText;
    public GameObject ShootCountText;
    public GameObject HitCountText;
    public GameObject FpsText;
    public GameObject MicrophoneButton;
    public GameObject TrumpetButton;
    public PunVoiceClient PhotonVoiceClient;
    public Recorder PhotonRecorder;
    public PhotonVoiceView _pvv;

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

        //Photon Voice北
        PhotonRecorder.RecordingEnabled = CanRecord;
        GameObject[] speakers = GameObject.FindGameObjectsWithTag("Photon Speaker");
        foreach(GameObject speaker in speakers)
        {
            speaker.GetComponent<AudioSource>().mute = !CanSpeak;
        }  

        if (this.gameObject.GetComponent<LifeControl>().NowLife <= 0)
        {
            PlayerIsDead = true;
            ExitGameButton.SetActive(true);
        }

        if (PhotonNetwork.CurrentRoom != null)
        {
            PlayerCountText.GetComponent<Text>().text = "ヘ玡计" + PhotonNetwork.CurrentRoom.PlayerCount + "";
        }
        ShootCountText.GetComponent<Text>().text = "甮阑Ω计" + this.gameObject.GetComponent<ShootControl>().ShootTimes + "Ω";
        HitCountText.GetComponent<Text>().text = "阑炳Ω计" + this.gameObject.GetComponent<ShootControl>().KillTimes + "Ω";
    }

    void Fps()
    {
        FpsText.GetComponent<Text>().text = "FPS" + FpsCount;
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
