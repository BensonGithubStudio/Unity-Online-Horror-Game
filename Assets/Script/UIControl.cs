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

    [Header("�C������")]
    public GameObject ExitGameButton;
    public GameObject PlayerCountText;
    public GameObject ShootCountText;
    public GameObject HitCountText;

    // Start is called before the first frame update
    void Start()
    {
        ExitGameButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        HitCountText.GetComponent<Text>().text = "�������ơG" + this.gameObject.GetComponent<ShootControl>().HitTimes + "��";
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
