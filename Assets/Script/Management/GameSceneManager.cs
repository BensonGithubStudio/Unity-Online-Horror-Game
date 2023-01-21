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
        this.gameObject.GetComponent<MouseControl>().CanAppear = !PhotonNetwork.IsConnected;

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Start Scene");
        }
        else
        {
            PhotonNetwork.Instantiate(PlayerCharacter[PlayerCharacterKind], StartPosition[PhotonNetwork.CurrentRoom.PlayerCount - 1].transform.position, Quaternion.identity);
            PlayerBulletCountSetUp();
            PlayerShootSpeedSetUp();
            PlayerBulletSpeedSetUp();
            PlayerShootDamageSetUp();
            PlayerLifeSetUp();
        }
    }

    void PlayerBulletCountSetUp()
    {
        if (PlayerCharacterKind == 0)
        {
            this.gameObject.GetComponent<ShootControl>().MaxBulletCount = 50;
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
