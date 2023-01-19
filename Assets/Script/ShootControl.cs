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

    [Header("�n���޲z")]
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

    //���a�g���ʧ@
    void Shoot()
    {
        if (IsAimEnemy)
        {
            HitSomebody(HitPlayerID, BulletDamage);
            ShootAudioSource.PlayOneShot(ShootSound);

            //�o�g�l�u�ӥB�o�e�ĤH��T
        }
        else
        {
            ShootAudioSource.PlayOneShot(ShootSound);

            //��µo�g�l�u
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
