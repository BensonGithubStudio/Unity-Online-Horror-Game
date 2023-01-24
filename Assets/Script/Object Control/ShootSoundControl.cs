using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootSoundControl : MonoBehaviour
{
    public PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyShootSound", 1);
    }

    void DestroyShootSound()
    {
        if (_pv.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
