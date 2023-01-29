using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{
    [Header("聲音管理")]
    public AudioSource MusicAudioSource;
    public AudioSource ButtonAudioSource;
    public AudioClip ClickSound;

    [Header("物件管理")]
    public GameObject StartButton;
    public GameObject SettingUI;
    public Animator SettingAnimator;
    public GameObject AboutUI;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider QualitySlider;

    [Header("參數設定")]
    public static float MusicValue;
    public static float SoundValue;
    public static int QualityNumber;

    // Update is called once per frame
    void Update()
    {
        MusicAudioSource.volume = MusicSlider.value;
        ButtonAudioSource.volume = SoundSlider.value;

        QualityNumber = (int)QualitySlider.value;
        QualitySettings.SetQualityLevel(QualityNumber);
    }

    public void OnClickStory()
    {

    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void OnClickSetting()
    {
        SettingUI.SetActive(true);
        SettingAnimator.SetBool("Close", false);
        ButtonAudioSource.PlayOneShot(ClickSound);
    }

    public void OnClickSettingClose()
    {
        Invoke("CloseSettingUI", 0.4f);
        SettingAnimator.SetBool("Close", true);
        ButtonAudioSource.PlayOneShot(ClickSound);
    }
    void CloseSettingUI()
    {
        SettingUI.SetActive(false);
    }

    public void OnClickAboutUI()
    {
        SettingUI.SetActive(false);
        AboutUI.SetActive(true);
        ButtonAudioSource.PlayOneShot(ClickSound);
    }

    public void OnClickAboutUIClose()
    {
        AboutUI.SetActive(false);
        SettingUI.SetActive(true);
        ButtonAudioSource.PlayOneShot(ClickSound);
    }
}
