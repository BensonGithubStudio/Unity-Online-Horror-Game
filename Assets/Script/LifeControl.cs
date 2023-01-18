using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    public float MaxLife;
    public float NowLife;
    public GameObject BloodBar;

    // Update is called once per frame
    void Update()
    {
        if (MaxLife != 0)
        {
            BloodBar.transform.localPosition = new Vector3(-800 + ((NowLife / MaxLife) * 800), 10, 0);
        }
    }
}
