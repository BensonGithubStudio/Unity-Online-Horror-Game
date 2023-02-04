using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public bool CanAppear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
