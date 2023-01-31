using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OilBottleEffect : MonoBehaviour
{
    public PhotonView _pv;

    public int OilBottleNumber;
    public int HitNumber;

    // Update is called once per frame
    void Update()
    {
        if (HitNumber == OilBottleNumber)
        {
            if (_pv.IsMine)
            {
                PhotonNetwork.Instantiate("Explosion Red", this.gameObject.transform.position, Quaternion.identity);
                PhotonNetwork.Instantiate("Explosion Blue", this.gameObject.transform.position, Quaternion.identity);
                PhotonNetwork.Instantiate("Bomb Sound", this.gameObject.transform.position, Quaternion.identity);
                PhotonNetwork.Instantiate("Bomb Light", this.gameObject.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }
}
