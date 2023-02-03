using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBulletControl : MonoBehaviour
{
    public float MaxSuper;
    public float NowSuper;
    public GameObject SuperBulletBar;

    // Update is called once per frame
    void Update()
    {
        if (MaxSuper != 0)
        {
            SuperBulletBar.transform.localPosition = new Vector3(-800 + ((NowSuper / MaxSuper) * 800), 10, 0);
        }
    }
}
