using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxControl : MonoBehaviour
{
    public float NowRotation = 0;
    public float RotateSpeed;

    // Update is called once per frame  
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", NowRotation);
        NowRotation += RotateSpeed * Time.deltaTime;
        NowRotation %= 360;
    }
}
