using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimationControl : MonoBehaviour
{
    public Animator PlayerAnimator;
    public PhotonView _pv;
    public bool IsAddingBullet;

    [Header("¨¤¦â¥»¨­")]
    public GameObject[] PlayerMeshRender;

    // Update is called once per frame
    void Update()
    {
        if (_pv.IsMine)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                PlayerAnimator.SetBool("IsWalk", true);
            }
            else
            {
                PlayerAnimator.SetBool("IsWalk", false);
            }

            if (Input.GetMouseButton(0))
            {
                PlayerAnimator.SetBool("IsShoot", true);
            }
            else
            {
                PlayerAnimator.SetBool("IsShoot", false);
            }

            if(GameObject.Find("Game Control").GetComponent<LifeControl>().NowLife <= 0)
            {
                _pv.gameObject.GetComponent<Rigidbody>().useGravity = false;
                _pv.gameObject.GetComponent<BoxCollider>().enabled = false;
                _pv.gameObject.transform.Find("Body").transform.Find("Player Gun").transform.localEulerAngles = new Vector3(0, 0, 0);
                foreach (GameObject m in PlayerMeshRender)
                {
                    if (m.GetComponent<MeshRenderer>() != null)
                    {
                        m.GetComponent<MeshRenderer>().enabled = true;
                    }
                }

                _pv.gameObject.transform.position = new Vector3(_pv.gameObject.transform.position.x, 1.73f, _pv.gameObject.transform.position.z);
                _pv.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                PlayerAnimator.SetBool("IsDead", true);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!IsAddingBullet)
                {
                    IsAddingBullet = true;
                    PlayerAnimator.SetBool("IsAddingBullet", true);
                    Invoke("FineshedAddBullet", 2);
                }
            }
        }
    }

    void FineshedAddBullet()
    {
        PlayerAnimator.SetBool("IsAddingBullet", false);
        IsAddingBullet = false;
    }
}
