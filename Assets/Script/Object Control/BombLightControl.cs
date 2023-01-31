using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombLightControl : MonoBehaviour
{
    public PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBombLight", 1);
    }

    void DestroyBombLight()
    {
        if (_pv.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
