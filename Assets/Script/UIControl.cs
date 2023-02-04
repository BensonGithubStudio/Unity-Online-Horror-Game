using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class UIControl : MonoBehaviourPunCallbacks
{
    [Header("玩家狀態")]
    public bool PlayerIsDead;
    public int FpsCount;

    [Header("遊戲介面")]
    public GameObject ExitGameButton;
    public GameObject PlayerCountText;
    public GameObject ShootCountText;
    public GameObject HitCountText;
    public GameObject FpsText;

    // Start is called before the first frame update
    void Start()
    {
        ExitGameButton.SetActive(false);
        InvokeRepeating("Fps", 1, 1);
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
            PlayerCountText.GetComponent<Text>().text = "目前人數：" + PhotonNetwork.CurrentRoom.PlayerCount + "人";
        }
        ShootCountText.GetComponent<Text>().text = "射擊次數：" + this.gameObject.GetComponent<ShootControl>().ShootTimes + "次";
        HitCountText.GetComponent<Text>().text = "擊中次數：" + this.gameObject.GetComponent<ShootControl>().HitTimes + "次";
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
}
