using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameControl : MonoBehaviour
{
    public static string PlayerName;
    public GameObject PlayerInputNameImage;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerName != null)
        {
            PlayerInputNameImage.GetComponent<InputField>().text = PlayerName;
        }
    }

    public void OnFinishedEdit()
    {
        PlayerName = PlayerInputNameImage.GetComponent<InputField>().text;
    }
}
