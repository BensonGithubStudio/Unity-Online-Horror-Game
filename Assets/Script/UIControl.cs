using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class UIControl : MonoBehaviourPunCallbacks
{
    [Header("ª±®aª¬ºA")]
    public bool PlayerIsDead;

    [Header("¹CÀ¸¤¶­±")]
    public GameObject ExitGameButton;

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
