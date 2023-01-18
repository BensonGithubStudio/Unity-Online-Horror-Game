using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    [Header("相機參數")]
    public GameObject playerController;
    public GameObject PlayerCamera;
    public float CameraRotation;
    public float MouseSentitive;

    [Header("角色參數")]
    public PhotonView _pv;
    public Rigidbody PlayerRB;
    public float MoveSpeed;
    public float JumpSpeed;
    public bool CanJump;

    [Header("角色狀態")]
    public float MaxLife;
    public float NowLife;

    void OnCollisionEnter(Collision collision)
    {
        if (_pv.IsMine)
        {
            if (collision.gameObject.tag == "Floor")
            {
                CanJump = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_pv.IsMine)
        {
            PlayerMove();
            PlayerJump();
            CameraControl();

            CameraCheck();
            RigidbodyCheck();
        }
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
            if (CanJump)
            {
                PlayerRB.AddForce(0, JumpSpeed, 0);
                CanJump = false;
            }
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

    void CameraCheck()
    {
        GameObject[] Cams = GameObject.FindGameObjectsWithTag("MainCamera");

        foreach (GameObject cam in Cams)
        {
            if (!cam.GetComponent<PhotonView>().IsMine)
            {
                Destroy(cam);
            }
        }
    }

    void RigidbodyCheck()
    {
        GameObject[] PlayerObject = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in PlayerObject)
        {
            if (!playerObject.GetComponent<PhotonView>().IsMine)
            {
                Destroy(playerObject.GetComponent<Rigidbody>());
            }
        }
    }
}
