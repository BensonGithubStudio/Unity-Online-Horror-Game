using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitSmokeControl : MonoBehaviour
{
    public PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyHitSmoke", 2);
    }

    void DestroyHitSmoke()
    {
        if (_pv.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
