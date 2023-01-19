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
    public GameObject[] AimStar;

    [Header("射擊管理")]
    public PhotonView _pv;
    public float ShootSpeed;
    public int BulletDamage;
    public bool IsAimEnemy;
    public bool IsShooting;
    public int HitPlayerID;

    [Header("聲音管理")]
    public AudioSource ShootAudioSource;
    public AudioClip ShootSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AimStarAnimation();
        ColorChange();

        if (!IsShooting && Input.GetMouseButton(0))
        {
            IsShooting = true;
            InvokeRepeating("Shoot", 0, ShootSpeed);
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsShooting = false;
            CancelInvoke("Shoot");
        }
    }

    //玩家射擊動作
    void Shoot()
    {
        if (IsAimEnemy)
        {
            HitSomebody(HitPlayerID, BulletDamage);
            ShootAudioSource.PlayOneShot(ShootSound);

            //發射子彈而且發送敵人資訊
        }
        else
        {
            ShootAudioSource.PlayOneShot(ShootSound);

            //單純發射子彈
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
            Ray ray = Camera.main.ScreenPointToRay(AimStarCenter.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, ~(1 << 9)))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
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
            }
        }
    }
}
