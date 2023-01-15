using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("�۾��Ѽ�")]
    public GameObject playerController;
    public GameObject PlayerCamera;
    public float CameraRotation;
    public float MouseSentitive;

    [Header("����Ѽ�")]
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        CameraControl();
    }

    void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
        }
    }
    
    void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }
    }

    void CameraControl()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        playerController.transform.Rotate(0, h * MouseSentitive * Time.deltaTime, 0);
        CameraRotation += v * MouseSentitive * Time.deltaTime;
        CameraRotation = Mathf.Clamp(CameraRotation, -60f, 60f);
        PlayerCamera.transform.localEulerAngles = new Vector3(-CameraRotation, PlayerCamera.transform.localEulerAngles.y, 0);
    }
}