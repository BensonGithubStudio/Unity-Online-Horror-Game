using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MouseControl : MonoBehaviourPunCallbacks
{
    public bool CanAppear;

    // Update is called once per frame
    void Update()
    {
        CanAppearCheck();

        Cursor.visible = CanAppear;

        if (!CanAppear)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CanAppear)
            {
                CanAppear = false;
            }
            else
            {
                CanAppear = true;
            }
        }
    }

    void CanAppearCheck()
    {
        if (this.gameObject.GetComponent<LifeControl>().NowLife <= 0)
        {
            CanAppear = true;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        CanAppear = true;
        StartControl.IsDisconnected = true;
    }
}
