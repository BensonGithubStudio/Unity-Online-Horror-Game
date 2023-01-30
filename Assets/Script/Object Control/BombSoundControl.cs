using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombSoundControl : MonoBehaviour
{
    public PhotonView _pv;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().volume = StartControl.SoundVolume;

        Invoke("DestroyBombSound", 5);
    }

    void DestroyBombSound()
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
