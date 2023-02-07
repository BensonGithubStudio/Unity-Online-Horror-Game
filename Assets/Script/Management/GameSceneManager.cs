using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public AudioSource BackgroundMusicAudioSource;

    public string[] PlayerCharacter;
    public static int PlayerCharacterKind;
    public GameObject[] StartPosition;

    [Header("角色初始值設定")]
    public int[] MaxBulletCount;
    public float[] ShootSpeed;
    public float[] BulletMoveSpeed;
    public int[] BulletDamage;
    public int[] MaxLife;
    public int[] MaxEnergy;
    public int[] MaxSuper;
    public int[] SuperDamage;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusicAudioSource.volume = StartControl.MusicVolume;
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
            PlayerEnergySetUp();
            SuperBulletSetUp();
            SuperBulletDamageSetUp();
        }
    }

    void PlayerBulletCountSetUp()
    {
        this.gameObject.GetComponent<ShootControl>().MaxBulletCount = MaxBulletCount[PlayerCharacterKind];
    }

    void PlayerShootSpeedSetUp()
    {
        this.gameObject.GetComponent<ShootControl>().ShootSpeed = ShootSpeed[PlayerCharacterKind];
    }

    void PlayerBulletSpeedSetUp()
    {
        this.gameObject.GetComponent<ShootControl>().BulletMoveSpeed = BulletMoveSpeed[PlayerCharacterKind];
    }

    void PlayerShootDamageSetUp()
    {
        this.gameObject.GetComponent<ShootControl>().BulletDamage = BulletDamage[PlayerCharacterKind];
    }

    void PlayerLifeSetUp()
    {
        this.gameObject.GetComponent<LifeControl>().MaxLife = MaxLife[PlayerCharacterKind];
        this.gameObject.GetComponent<LifeControl>().NowLife = this.gameObject.GetComponent<LifeControl>().MaxLife;
    }

    void PlayerEnergySetUp()
    {
        this.gameObject.GetComponent<EnergyControl>().MaxEnergy = MaxEnergy[PlayerCharacterKind];
        this.gameObject.GetComponent<EnergyControl>().NowEnergy = this.gameObject.GetComponent<EnergyControl>().MaxEnergy;
    }

    void SuperBulletSetUp()
    {
        this.gameObject.GetComponent<SuperBulletControl>().MaxSuper = MaxSuper[PlayerCharacterKind];
    }

    void SuperBulletDamageSetUp()
    {
        this.gameObject.GetComponent<SuperBulletControl>().SuperDamage = SuperDamage[PlayerCharacterKind];
    }
}
