using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShootControl : MonoBehaviour
{
    [Header("準心動畫管理")]
    public Animator AimStarAnimator;
    public GameObject AimStarCenter;
    public GameObject[] ManyAimStarCenter;
    public GameObject[] AimStar;
    public float ShootDuringTime;

    [Header("倍鏡準心動畫管理")]
    public Animator BigAimStarAnimator;
    public bool IsBigAim;

    [Header("槍支狀態管理")]
    public int MaxBulletCount;
    public int NowBulletCount;
    public GameObject BulletCountText;
    public bool IsAddingBullet;

    [Header("射擊管理")]
    public PhotonView _pv;
    public float ShootSpeed;
    public int BulletDamage;
    public bool IsAimEnemy;
    public bool IsShooting;
    public int HitPlayerID;

    [Header("子彈飛行管理")]
    public GameObject ShootPosition;
    public Vector3 HitPosition;
    public float BulletMoveSpeed;

    [Header("聲音管理")]
    public static float ShootPitch;
    public AudioSource ShootAudioSource;
    public AudioClip AddBulletSound;

    [Header("相機震動管理")]
    public GameObject PlayerCamera;
    public int ShakeTimes;
    public float ShakeWave;
    public float NowShakeWave;

    // Start is called before the first frame update
    void Start()
    {
        NowBulletCount = MaxBulletCount;
        
        Invoke("ShootPointCheck", 0.5f);

        if (ShootPitch == 0)
        {
            ShootPitch = Random.Range(0.6f, 1.3f);
        }
    }

    void ShootPointCheck()
    {
        GameObject shootPosition = GameObject.FindGameObjectWithTag("Shoot Point");
        if (shootPosition.GetComponent<PhotonView>().IsMine)
        {
            ShootPosition = shootPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<LifeControl>().NowLife > 0)
        {
            AimStarAnimation();
            ColorChange();
            BulletCount();

            if (Input.GetMouseButton(0))
            {
                ShootDuringTime += Time.deltaTime;

                if (!IsShooting)
                {
                    IsShooting = true;
                    InvokeRepeating("Shoot", 0, ShootSpeed);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                ShootDuringTime = 0;
                
                IsShooting = false;
                CancelInvoke("Shoot");
            }
            if (Input.GetMouseButtonDown(1))
            {
                AddBullet();
            }
            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                if (IsBigAim)
                {
                    IsBigAim = false;
                    BigAimStarAnimator.SetBool("IsAim", false);
                }
                else
                {
                    IsBigAim = true;
                    BigAimStarAnimator.SetBool("IsAim", true);
                }
            }
        }
    }

    //玩家射擊動作
    void Shoot()
    {
        if (NowBulletCount > 0 && !IsAddingBullet)
        {
            NowBulletCount -= 1;

            if (IsAimEnemy)
            {
                //發射子彈而且發送敵人資訊

                if (ShootPosition != null)
                {
                    GameObject bullet = PhotonNetwork.Instantiate("Player Bullet", ShootPosition.transform.position, Quaternion.identity);
                    GameObject shootSound = PhotonNetwork.Instantiate("Shoot Sound", ShootPosition.transform.position, Quaternion.identity);
                    bullet.transform.LookAt(HitPosition);
                    bullet.GetComponent<BulletControl>().MoveSpeed = BulletMoveSpeed;
                    shootSound.GetComponent<AudioSource>().pitch = ShootPitch;
                    HitSomebody(HitPlayerID, BulletDamage);

                    PhotonNetwork.Instantiate("Hit Smoke", HitPosition, Quaternion.identity);
                }
            }
            else
            {
                //單純發射子彈

                if (ShootPosition != null)
                {
                    GameObject bullet = PhotonNetwork.Instantiate("Player Bullet", ShootPosition.transform.position, Quaternion.identity);
                    GameObject shootSound = PhotonNetwork.Instantiate("Shoot Sound", ShootPosition.transform.position, Quaternion.identity);
                    bullet.transform.LookAt(HitPosition);
                    bullet.GetComponent<BulletControl>().MoveSpeed = BulletMoveSpeed;
                    shootSound.GetComponent<AudioSource>().pitch = ShootPitch;

                    PhotonNetwork.Instantiate("Hit Smoke", HitPosition, Quaternion.identity);
                }
            }
        }
    }

    void HitSomebody(int id,int Damage)
    {
        _pv.RPC("RPCHitSomebody", RpcTarget.All, id, Damage);
    }

    [PunRPC]
    void RPCHitSomebody(int id,int Damage)
    {
        if (PhotonView.Find(id).GetComponent<PhotonView>().IsMine)
        {
            this.gameObject.GetComponent<LifeControl>().NowLife -= Damage;
        }
    }

    void BulletCount()
    {
        BulletCountText.GetComponent<Text>().text = "" + NowBulletCount;
    }

    //射擊準心動畫
    void AimStarAnimation()
    {
        if (Input.GetMouseButton(0))
        {
            AimStarAnimator.SetBool("IsShoot", true);
        }
        else
        {
            AimStarAnimator.SetBool("IsShoot", false);
        }
    }

    //準心瞄準敵人時變成紅色
    void ColorChange()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            Ray ray;
            if (ShootDuringTime < 1)
            {
                ray = Camera.main.ScreenPointToRay(AimStarCenter.transform.position);
            }
            else
            {
                int a = Random.Range(0, ManyAimStarCenter.Length);
                ray = Camera.main.ScreenPointToRay(ManyAimStarCenter[a].transform.position);
            }

            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("SceneBorder");
            if (Physics.Raycast(ray, out hit, 10000, ~mask))
            {
                HitPosition = hit.point;

                //如果碰到玩家
                if (hit.transform.gameObject.tag == "Player")
                {
                    AimStarCenter.GetComponent<Image>().color = Color.red;
                    for (int i = 0; i < 4; i++)
                    {
                        AimStar[i].GetComponent<Image>().color = Color.red;
                    }

                    HitPlayerID = hit.transform.gameObject.GetComponent<PhotonView>().ViewID;
                    IsAimEnemy = true;
                }
                else
                {
                    AimStarCenter.GetComponent<Image>().color = Color.white;
                    for (int i = 0; i < 4; i++)
                    {
                        AimStar[i].GetComponent<Image>().color = Color.white;
                    }

                    IsAimEnemy = false;
                }

                //如果碰到油桶
                if (hit.transform.gameObject.tag == "Oil Bottle")
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (NowBulletCount > 0)
                        {
                            int num = hit.transform.gameObject.GetComponent<OilBottleEffect>().OilBottleNumber;
                            CameraShake();
                            HitOilBottle(num);
                        }
                    }

                    AimStarCenter.GetComponent<Image>().color = Color.red;
                    for (int i = 0; i < 4; i++)
                    {
                        AimStar[i].GetComponent<Image>().color = Color.red;
                    }
                }
            }
            else
            {
                AimStarCenter.GetComponent<Image>().color = Color.white;
                for (int i = 0; i < 4; i++)
                {
                    AimStar[i].GetComponent<Image>().color = Color.white;
                }

                IsAimEnemy = false;
            }
        }
    }

    void HitOilBottle(int number)
    {
        _pv.RPC("RPCHitOilBottle", RpcTarget.All, number);
    }

    [PunRPC]
    void RPCHitOilBottle(int number)
    {
        GameObject[] bottles = GameObject.FindGameObjectsWithTag("Oil Bottle");

        foreach (GameObject bottle in bottles)
        {
            bottle.GetComponent<OilBottleEffect>().HitNumber = number;
        }
    }

    void AddBullet()
    {
        if (!IsAddingBullet)
        {
            IsAddingBullet = true;
            ShootAudioSource.PlayOneShot(AddBulletSound);
            Invoke("FinishedAddBullet", 2);
        }
    }

    void FinishedAddBullet()
    {
        NowBulletCount = MaxBulletCount;
        IsAddingBullet = false;
    }

    void CameraShake()
    {
        GameObject[] cams = GameObject.FindGameObjectsWithTag("MainCamera");

        foreach(GameObject cam in cams)
        {
            if (cam.GetComponent<PhotonView>().IsMine)
            {
                PlayerCamera = cam;
            }
        }

        NowShakeWave = ShakeWave;
        Shake();
    }

    void Shake()
    {
        PlayerCamera.transform.localPosition = new Vector3(0, 0, 0);
        Invoke("Shake2", 0.05f);
    }

    void Shake2()
    {
        PlayerCamera.transform.localPosition = new Vector3(NowShakeWave, NowShakeWave, NowShakeWave);

        if (ShakeTimes < 20)
        {
            NowShakeWave -= NowShakeWave / 3;
            ShakeTimes += 1;
            Invoke("Shake", 0.05f);
        }
        else
        {
            ShakeTimes = 0;
        }
    }
}