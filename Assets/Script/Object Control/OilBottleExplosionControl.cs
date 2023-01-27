using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OilBottleExplosionControl : MonoBehaviour
{
    public PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyExplosion", 5);
    }

    void DestroyExplosion()
    {
        if (_pv.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
