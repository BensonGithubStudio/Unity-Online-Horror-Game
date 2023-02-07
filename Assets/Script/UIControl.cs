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
    [Header("玩家狀態")]
    public bool PlayerIsDead;
    public int FpsCount;
    public bool CanRecord;
    public bool CanSpeak;
    public bool IsPhotonSpeaking;

    [Header("遊戲介面")]
    public GameObject ExitGameButton;
    public GameObject PlayerCountText;
    public GameObject ShootCountText;
    public GameObject HitCountText;
    public GameObject FpsText;
    public GameObject MicrophoneButton;
    public GameObject TrumpetButton;
    public GameObject MicrophoneWorking;
    public GameObject TrumpetWorking;
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
        MicrophoneWorking.SetActive(false);
        TrumpetWorking.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FpsCount += 1;

        //Photon Voice控制
        PhotonRecorder.RecordingEnabled = CanRecord;
        GameObject[] speakers = GameObject.FindGameObjectsWithTag("Photon Speaker");
        foreach(GameObject speaker in speakers)
        {
            speaker.GetComponent<AudioSource>().mute = !CanSpeak;
        }

        //效果控制
        int NotSpeakingCount = 0;
        GameObject[] pvvs = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject pvv in pvvs)
        {
            if (pvv.GetComponent<PhotonView>().IsMine)
            {
                _pvv = pvv.GetComponent<PhotonVoiceView>();
            }
            else
            {
                if (!pvv.GetComponent<PhotonVoiceView>().IsSpeaking)
                {
                    NotSpeakingCount += 1;
                }
            }
        }

        if (NotSpeakingCount == PhotonNetwork.CurrentRoom.PlayerCount - 1)
        {
            IsPhotonSpeaking = false;
        }
        else
        {
            IsPhotonSpeaking = true;
        }

        if (_pvv.IsRecording && CanRecord)
        {
            MicrophoneButton.GetComponent<Image>().color = Color.green;
            MicrophoneWorking.SetActive(true);
        }
        else
        {
            MicrophoneButton.GetComponent<Image>().color = Color.white;
            MicrophoneWorking.SetActive(false);
        }
        if (IsPhotonSpeaking && CanSpeak)
        {
            TrumpetButton.GetComponent<Image>().color = Color.green;
            TrumpetWorking.SetActive(true);
        }
        else
        {
            TrumpetButton.GetComponent<Image>().color = Color.white;
            TrumpetWorking.SetActive(false);
        }
        
        //離開遊戲按鈕顯示
        if (this.gameObject.GetComponent<LifeControl>().NowLife <= 0)
        {
            PlayerIsDead = true;
            ExitGameButton.SetActive(true);
        }

        //遊戲資訊顯示
        if (PhotonNetwork.CurrentRoom != null)
        {
            PlayerCountText.GetComponent<Text>().text = "目前人數：" + PhotonNetwork.CurrentRoom.PlayerCount + "人";
        }
        ShootCountText.GetComponent<Text>().text = "射擊次數：" + this.gameObject.GetComponent<ShootControl>().ShootTimes + "次";
        HitCountText.GetComponent<Text>().text = "擊殺次數：" + this.gameObject.GetComponent<ShootControl>().KillTimes + "次";
    }

    void Fps()
    {
        FpsText.GetComponent<Text>().text = "FPS：" + FpsCount;
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
