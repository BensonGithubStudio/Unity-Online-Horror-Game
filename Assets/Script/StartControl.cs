using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    [Header("聲音管理")]
    public AudioSource MusicAudioSource;
    public AudioSource ButtonAudioSource;
    public AudioClip ClickSound;

    [Header("物件管理")]
    public GameObject StartButton;
    public GameObject SettingUI;
    public GameObject TransitionUI;
    public Animator SettingAnimator;
    public GameObject AboutUI;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider QualitySlider;
    public Slider MouseSentitiveSlider;
    public Slider AimMouseSentitiveSlider;

    [Header("參數設定")]
    public static float MusicVolume = 1;
    public static float SoundVolume = 1;
    public static int QualityNumber = 0;
    public static float MouseSentitive = 200;
    public static float AimMouseSentitive = 20;

    void Start()
    {
        Invoke("PlayMusic", 2.6f);

        MusicSlider.value = MusicVolume;
        SoundSlider.value = SoundVolume;
        QualitySlider.value = QualityNumber;
        MouseSentitiveSlider.value = MouseSentitive;
        AimMouseSentitiveSlider.value = AimMouseSentitive;
    }

    void PlayMusic()
    {
        MusicAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //聲音設定
        MusicVolume = MusicSlider.value;
        MusicAudioSource.volume = MusicVolume;
        SoundVolume = SoundSlider.value;
        ButtonAudioSource.volume = SoundVolume;

        //畫質設定
        QualityNumber = (int)QualitySlider.value;
        QualitySettings.SetQualityLevel(QualityNumber);

        //滑鼠靈敏度設定
        MouseSentitive = MouseSentitiveSlider.value;
        AimMouseSentitive = AimMouseSentitiveSlider.value;
    }

    public void OnClickInputIntroduce()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
    }

    public void OnClickStory()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        TransitionUI.SetActive(false);
        TransitionUI.SetActive(true);
        InvokeRepeating("BackgroundMusicControl", 0, 0.02f);
        Invoke("ChangeToStoryScene", 2);
    }

    void BackgroundMusicControl()
    {
        if (MusicSlider.value > 0)
        {
            MusicSlider.value -= 0.02f;
        }
        else
        {
            CancelInvoke("BackgroundMusicControl");
        }
    }

    void ChangeToStoryScene()
    {
        MusicVolume = 1;
        SceneManager.LoadScene("Story Scene");
    }

    public void OnClickExitGame()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        Invoke("ExitGame", 1);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    public void OnClickSetting()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        SettingUI.SetActive(true);
        SettingAnimator.SetBool("Close", false);
    }

    public void OnClickSettingClose()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        Invoke("CloseSettingUI", 0.6f);
        SettingAnimator.SetBool("Close", true);
    }
    void CloseSettingUI()
    {
        SettingUI.SetActive(false);
    }

    public void OnClickAboutUI()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        SettingUI.SetActive(false);
        AboutUI.SetActive(true);
    }

    public void OnClickAboutUIClose()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        AboutUI.SetActive(false);
        SettingUI.SetActive(true);
    }

    //設定內按鈕
    public void OnClickAudio()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        SettingAnimator.SetBool("IsAudio", true);
        SettingAnimator.SetBool("IsQuality", false);
        SettingAnimator.SetBool("IsMouse", false);
        SettingAnimator.SetBool("IsOther", false);
    }

    public void OnClickScreenQuality()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        SettingAnimator.SetBool("IsAudio", false);
        SettingAnimator.SetBool("IsQuality", true);
        SettingAnimator.SetBool("IsMouse", false);
        SettingAnimator.SetBool("IsOther", false);
    }

    public void OnClickMouseSentitive()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        SettingAnimator.SetBool("IsAudio", false);
        SettingAnimator.SetBool("IsQuality", false);
        SettingAnimator.SetBool("IsMouse", true);
        SettingAnimator.SetBool("IsOther", false);
    }

    public void OnClickOther()
    {
        ButtonAudioSource.PlayOneShot(ClickSound);
        SettingAnimator.SetBool("IsAudio", false);
        SettingAnimator.SetBool("IsQuality", false);
        SettingAnimator.SetBool("IsMouse", false);
        SettingAnimator.SetBool("IsOther", true);
    }
}
