using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyControl : MonoBehaviour
{
    public float MaxEnergy;
    public float NowEnergy;
    public GameObject EnergyBar;
    public bool IsAddingLife;
    
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<LifeControl>().NowLife > 0 && MaxEnergy != 0)
        {
            EnergyBar.transform.localPosition = new Vector3(-800 + ((NowEnergy / MaxEnergy) * 800), 10, 0);
        }

        if (this.gameObject.GetComponent<LifeControl>().NowLife > 0 && this.gameObject.GetComponent<LifeControl>().NowLife < this.gameObject.GetComponent<LifeControl>().MaxLife)
        {
            if (!IsAddingLife)
            {
                InvokeRepeating("AddLife", 1, 1);
                IsAddingLife = true;
            }
        }
        else
        {
            CancelInvoke("AddLife");
            IsAddingLife = false;
        }
    }

    void AddLife()
    {
        NowEnergy -= 2;
        this.gameObject.GetComponent<LifeControl>().NowLife += 2;
    }
}
