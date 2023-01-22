using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject GameControlObject;
    public GameObject HitImage;
    public float NowLife;
    public float LateLife;

    // Start is called before the first frame update
    void Start()
    {
        HitImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        NowLife = GameControlObject.GetComponent<LifeControl>().NowLife;

        if (NowLife < LateLife)
        {
            HitImage.SetActive(false);
            HitImage.SetActive(true);
            CancelInvoke("DestroyHitImage");
            Invoke("DestroyHitImage", 1);
        }

        LateLife = GameControlObject.GetComponent<LifeControl>().NowLife;
    }

    void DestroyHitImage()
    {
        HitImage.SetActive(false);
    }
}
