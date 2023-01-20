using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimationControl : MonoBehaviour
{
    public Animator PlayerAnimator;
    public PhotonView _pv;

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
                PlayerAnimator.SetBool("IsDead", true);
            }
        }
    }
}
