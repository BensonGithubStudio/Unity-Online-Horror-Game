using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffectControl : MonoBehaviour
{
    MeshRenderer LightMesh;
    GameObject LightObject;

    // Start is called before the first frame update
    void Start()
    {
        LightMesh = this.gameObject.GetComponent<MeshRenderer>();
        LightObject = this.gameObject.transform.GetComponentInChildren<Light>().gameObject;

        Invoke("CloseLight", Random.Range(1, 3));
    }

    void CloseLight()
    {
        LightMesh.enabled = false;
        LightObject.GetComponent<Light>().enabled = false;

        Invoke("OpenLight", Random.Range(1.0f, 3.0f));
    }

    void OpenLight()
    {
        LightMesh.enabled = true;
        LightObject.GetComponent<Light>().enabled = true;

        Invoke("CloseLight", Random.Range(1.0f, 3.0f));
    }
}
