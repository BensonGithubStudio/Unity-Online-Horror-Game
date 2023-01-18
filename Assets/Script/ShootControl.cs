using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootControl : MonoBehaviour
{
    public Animator AimStarAnimator;
    public GameObject AimStarCenter;
    public GameObject[] AimStar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //射擊準心動畫
        if (Input.GetMouseButton(0))
        {
            AimStarAnimator.SetBool("IsShoot", true);
        }
        else
        {
            AimStarAnimator.SetBool("IsShoot", false);
        }

        //準心瞄準敵人時變成紅色
        Ray ray = Camera.main.ScreenPointToRay(AimStarCenter.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, ~(1 << 9)))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            if (hit.transform.gameObject.tag == "Player")
            {
                AimStarCenter.GetComponent<Image>().color = Color.red;
                for (int i = 0; i < 4; i++)
                {
                    AimStar[i].GetComponent<Image>().color = Color.red;
                }
            }
            else
            {
                AimStarCenter.GetComponent<Image>().color = Color.white;
                for (int i = 0; i < 4; i++)
                {
                    AimStar[i].GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
}
