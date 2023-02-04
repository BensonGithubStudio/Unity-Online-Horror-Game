using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBulletControl : MonoBehaviour
{
    public float MaxSuper;
    public float NowSuper;
    public int SuperDamage;
    public GameObject SuperBulletBar;
    public Animator SuperBulletBarAnimator;

    // Update is called once per frame
    void Update()
    {
        if (MaxSuper != 0)
        {
            SuperBulletBar.transform.localPosition = new Vector3(10, -300 + ((NowSuper / MaxSuper) * 300), 0);
        }

        if (NowSuper >= MaxSuper)
        {
            SuperBulletBarAnimator.SetBool("Full", true);
        }
        else
        {
            SuperBulletBarAnimator.SetBool("Full", false);
        }
    }
}
