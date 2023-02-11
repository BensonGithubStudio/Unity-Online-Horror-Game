using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroduceControl : MonoBehaviour
{
    [Header("聲音管理")]
    public AudioSource MusicAudioSource;
    public AudioSource ButtonAudioSource;
    public AudioClip ButtonSound;

    [Header("操作說明動畫管理")]
    public Animator IntroduceUIAnimator;
    public GameObject LastStepButton;
    public GameObject NextStepButton;

    void Update()
    {
        if (IntroduceUIAnimator.GetInteger("Step") > 0)
        {
            LastStepButton.SetActive(true);
        }
        else
        {
            LastStepButton.SetActive(false);
        }

        if (IntroduceUIAnimator.GetInteger("Step") < 4)
        {
            NextStepButton.SetActive(true);
        }
        else
        {
            NextStepButton.SetActive(false);
        }
    }

    public void OnClickLeaveIntroduceScene()
    {
        ButtonAudioSource.PlayOneShot(ButtonSound);
        Invoke("ChangeToStartScene", 0.5f);
    }

    void ChangeToStartScene()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void OnClickNextStep()
    {
        ButtonAudioSource.PlayOneShot(ButtonSound);
        if (IntroduceUIAnimator.GetInteger("Step") < 4)
        {
            IntroduceUIAnimator.SetInteger("Step", IntroduceUIAnimator.GetInteger("Step") + 1);
        }
    }

    public void OnClickLastStep()
    {
        ButtonAudioSource.PlayOneShot(ButtonSound);

        if (IntroduceUIAnimator.GetInteger("Step") > 0)
        {
            IntroduceUIAnimator.SetInteger("Step", IntroduceUIAnimator.GetInteger("Step") - 1);
        }
    }
}
