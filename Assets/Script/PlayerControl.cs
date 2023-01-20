using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    [Header("诀把计")]
    public GameObject playerController;
    public GameObject PlayerCamera;
    public float CameraRotation;
    public float MouseSentitive;

    [Header("à︹把计")]
    public PhotonView _pv;
    public Rigidbody PlayerRB;
    public float MoveSpeed;
    public float JumpSpeed;
    public bool CanJump;
    public GameObject PlayerGun;

    [Header("à︹篈")]
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

    // Update is called once per frame
    void Update()
    {
        if (_pv.IsMine)
        {
            if (GameObject.Find("Game Control").GetComponent<LifeControl>().NowLife > 0)
            {
                PlayerMove();
                PlayerJump();
                CameraControl();
                GunControl();
            }
            else
            {
                PlayerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

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

    void GunControl()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        playerController.transform.Rotate(0, h * MouseSentitive * Time.deltaTime, 0);
        CameraRotation += v * MouseSentitive * Time.deltaTime;
        CameraRotation = Mathf.Clamp(CameraRotation, -60f, 60f);
        PlayerGun.transform.localEulerAngles = new Vector3(-CameraRotation, PlayerGun.transform.localEulerAngles.y, 0);
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
