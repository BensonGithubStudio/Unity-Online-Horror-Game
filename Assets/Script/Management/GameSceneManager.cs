using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public string[] PlayerCharacter;
    public int PlayerCharacterKind;
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
            PhotonNetwork.Instantiate(PlayerCharacter[PlayerCharacterKind], StartPosition[PhotonNetwork.CurrentRoom.PlayerCount - 1].transform.position, Quaternion.identity);
            PlayerShootSpeedSetUp();
            PlayerBulletSpeedSetUp();
            PlayerShootDamageSetUp();
            PlayerLifeSetUp();
        }
    }

    void PlayerShootSpeedSetUp()
    {
        if(PlayerCharacterKind == 0)
        {
            this.gameObject.GetComponent<ShootControl>().ShootSpeed = 0.1f;
        }
    }

    void PlayerBulletSpeedSetUp()
    {
        if (PlayerCharacterKind == 0)
        {
            this.gameObject.GetComponent<ShootControl>().BulletMoveSpeed = 100;
        }
    }

    void PlayerShootDamageSetUp()
    {
        if (PlayerCharacterKind == 0)
        {
           this.gameObject.GetComponent<ShootControl>().BulletDamage = 10;
        }
    }

    void PlayerLifeSetUp()
    {
        if (PlayerCharacterKind == 0)
        {
            this.gameObject.GetComponent<LifeControl>().MaxLife = 100;
            this.gameObject.GetComponent<LifeControl>().NowLife = this.gameObject.GetComponent<LifeControl>().MaxLife;
        }
    }
}
