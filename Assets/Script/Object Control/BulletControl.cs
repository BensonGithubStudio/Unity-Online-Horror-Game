using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletControl : MonoBehaviour
{
    public PhotonView _pv;
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 2);
    }

    void DestroyBullet()
    {
        if (_pv.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
    }
}
