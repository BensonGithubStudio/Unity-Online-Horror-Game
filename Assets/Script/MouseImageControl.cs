using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseImageControl : MonoBehaviour
{
    public GameObject MouseImage;

    void Start()
    {
        MouseImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�ƹ��I�����S��
        if (Input.GetMouseButtonDown(0))
        {
            MouseImage.transform.position = Input.mousePosition;
            MouseImage.SetActive(false);
            MouseImage.SetActive(true);
        }
    }
}
