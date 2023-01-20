using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShootControl : MonoBehaviour
{
    [Header("�Ǥ߰ʵe�޲z")]
    public Animator AimStarAnimator;
    public GameObject AimStarCenter;
    public GameObject[] AimStar;

    [Header("�g���޲z")]
    public PhotonView _pv;
    public float ShootSpeed;
    public int BulletDamage;
    public bool IsAimEnemy;
    public bool IsShooting;
    public int HitPlayerID;

    [Header("�l�u����޲z")]
    public GameObject ShootPosition;
    public Vector3 HitPosition;
    public float BulletMoveSpeed;

    [Header("�n���޲z")]
    public AudioSource ShootAudioSource;
    public AudioClip ShootSound;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootPointCheck", 0.5f);
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
    }

    //���a�g���ʧ@
    void Shoot()
    {
        if (IsAimEnemy)
        {
            //�o�g�l�u�ӥB�o�e�ĤH��T

            if (ShootPosition != null)
            {
                GameObject bullet = PhotonNetwork.Instantiate("Player Bullet", ShootPosition.transform.position, Quaternion.identity);
                bullet.transform.LookAt(HitPosition);
                bullet.GetComponent<BulletControl>().MoveSpeed = BulletMoveSpeed;
                HitSomebody(HitPlayerID, BulletDamage);
                ShootAudioSource.PlayOneShot(ShootSound);
            }
        }
        else
        {
            //��µo�g�l�u

            if (ShootPosition != null)
            {
                GameObject bullet = PhotonNetwork.Instantiate("Player Bullet", ShootPosition.transform.position, Quaternion.identity);
                bullet.transform.LookAt(HitPosition);
                bullet.GetComponent<BulletControl>().MoveSpeed = BulletMoveSpeed;
                ShootAudioSource.PlayOneShot(ShootSound);
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

    //�g���Ǥ߰ʵe
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

    //�Ǥߺ˷ǼĤH���ܦ�����
    void ColorChange()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(AimStarCenter.transform.position);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("SceneBorder");
            if (Physics.Raycast(ray, out hit, 10000, ~mask))
            {
                HitPosition = hit.point;

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
