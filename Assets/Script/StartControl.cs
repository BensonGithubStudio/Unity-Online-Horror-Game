using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{
    [Header("�n���޲z")]
    public AudioSource MusicAudioSource;
    public AudioSource ButtonAudioSource;
    public AudioClip ClickSound;

    [Header("����޲z")]
    public GameObject StartButton;
    public GameObject SettingUI;
    public Animator SettingAnimator;
    public GameObject AboutUI;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Slider QualitySlider;
    public Slider MouseSentitiveSlider;
    public Slider AimMouseSentitiveSlider;

    [Header("�ѼƳ]�w")]
    public static float MusicVolume = 1;
    public static float SoundVolume = 1;
    public static int QualityNumber = 5;
    public static float MouseSentitive = 200;
    public static float AimMouseSentitive = 20;

    void Start()
    {
        MusicSlider.value = MusicVolume;
        SoundSlider.value = SoundVolume;
        QualitySlider.value = QualityNumber;
        MouseSentitiveSlider.value = MouseSentitive;
        AimMouseSentitiveSlider.value = AimMouseSentitive;
    }

    // Update is called once per frame
    void Update()
    {
        //�n���]�w
        MusicVolume = MusicSlider.value;
        MusicAudioSource.volume = MusicVolume;
        SoundVolume = SoundSlider.value;
        ButtonAudioSource.volume = SoundVolume;

        //�e��]�w
        QualityNumber = (int)QualitySlider.value;
        QualitySettings.SetQualityLevel(QualityNumber);

        //�ƹ��F�ӫ׳]�w
        MouseSentitive = MouseSentitiveSlider.value;
        AimMouseSentitive = AimMouseSentitiveSlider.value;
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
        Invoke("CloseSettingUI", 0.6f);
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

    //�]�w�����s
    public void OnClickAudio()
    {
        SettingAnimator.SetBool("IsAudio", true);
        SettingAnimator.SetBool("IsQuality", false);
        SettingAnimator.SetBool("IsMouse", false);
        SettingAnimator.SetBool("IsOther", false);
    }

    public void OnClickScreenQuality()
    {
        SettingAnimator.SetBool("IsAudio", false);
        SettingAnimator.SetBool("IsQuality", true);
        SettingAnimator.SetBool("IsMouse", false);
        SettingAnimator.SetBool("IsOther", false);
    }

    public void OnClickMouseSentitive()
    {
        SettingAnimator.SetBool("IsAudio", false);
        SettingAnimator.SetBool("IsQuality", false);
        SettingAnimator.SetBool("IsMouse", true);
        SettingAnimator.SetBool("IsOther", false);
    }

    public void OnClickOther()
    {
        SettingAnimator.SetBool("IsAudio", false);
        SettingAnimator.SetBool("IsQuality", false);
        SettingAnimator.SetBool("IsMouse", false);
        SettingAnimator.SetBool("IsOther", true);
    }
}
